using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace TechdaysBot.Dialogs
{
    [Serializable]
    [LuisModel("df2316dd-a2e5-4b39-9a0f-25df97c475dd", "776058c82bd54f3cbbbb93ab0f2bd3c1")]
    public class RootDialog : LuisDialog<object>
    {


        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var card = new HeroCard()
            {
                Title = "Hero card",
                Text = "HoloLens",
                Subtitle = "Subtitle",
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, "Roll Again", value: "HoloLens"),
                }
            };


            var message = context.MakeMessage();
            message.Attachments = new List<Attachment>()
            {
                card.ToAttachment()
            };


            message.Speak = "Hello";



            // Send card and bots reaction to user.
            message.InputHint = InputHints.AcceptingInput;
            await context.PostAsync(message);


            context.Wait(MessageReceived);
        }

        [LuisIntent("Session Topic")]
        public async Task SessionTopic(IDialogContext context, LuisResult result)
        {
            string topic = null;
            if (result.TryFindEntity("Topic", out EntityRecommendation entity))
            {
                topic = entity.Entity;
            }


            if (string.IsNullOrEmpty(topic) || !SessionDatabase.Sessions.Any(x => x.Topics.Contains(topic)))
            {
                var topics = SessionDatabase.Sessions.SelectMany(x => x.Topics).Distinct().ToList();
                PromptDialog.Choice<string>(context, AfterTopicPicked, topics, "Which topic do you mean?");
            }
            else
            {
                await ShowRelevantSessions(context, topic);
            }
        }

        [LuisIntent("Where Is Techdays")]
        public async Task WhereIsTechdays(IDialogContext context, LuisResult result)
        {
            var message = context.MakeMessage() as IMessageActivity;
            message.ChannelData = JObject.FromObject(new
            {
                action = new { type = "LaunchUri", uri = "bingmaps:?where=amsterdam" }
            });
            await context.PostAsync(message);
        }

        private async Task AfterTopicPicked(IDialogContext context, IAwaitable<string> result)
        {
            string topic = await result;
            await ShowRelevantSessions(context, topic);
        }

        private static async Task ShowRelevantSessions(IDialogContext context, string topic)
        {
            var card = new HeroCard()
            {
                Title = "Relevant sessions",
                Text = $"Sessions at techdays for {topic}",
                Subtitle = "",
                Images = SessionDatabase.Sessions.Where(x => x.Topics.Contains(topic)).Select(x => new CardImage { Url = x.Image }).ToList()
            };
            var message = context.MakeMessage();
            message.Attachments = new List<Attachment>()
                {
                    card.ToAttachment()
                };
            message.Speak = "Sessions";

            // Send card and bots reaction to user.
            message.InputHint = InputHints.AcceptingInput;
            await context.PostAsync(message);
        }

        

        /// <summary>
        /// Need to override the LuisDialog.MessageReceived method so that we can detect when the user invokes the skill without
        /// specifying a phrase, for example: "Open Hotel Finder", or "Ask Hotel Finder". In these cases, the message received will be an empty string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override async Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            // Check for empty query
            var message = await item;
            if (string.IsNullOrEmpty(message.Text))
            {
                // Return the Help/Welcome
                await Help(context, null);
            }
            else
            {
                await base.MessageReceived(context, item);
            }
        }

        private async Task Help(IDialogContext context, object p)
        {
            const string ssmltext = @"<speak version=""1.0""
                                         xmlns=""http://www.w3.org/2001/10/synthesis""
                                         xml:lang=""en-US"">
                                            <p>
                                                I am the Techdays Bot. <break /> I can help you with information about techdays and the sessions
                                            </p>
                                            <p>
                                                You can ask me <break />
                                                Find sessions for specific topics <break />
                                                Find information about speakers <break />
                                            </p>
                                        </speak>";

            const string text = "I am the Techdays Bot. I can help you with information about techdays and the sessions. You can ask me Find sessions for specific topics. Find information about speakers";

            await context.SayAsync("I'm the techdays bot, here to help", ssmltext);

            context.Wait(MessageReceived);
        }
    }
}