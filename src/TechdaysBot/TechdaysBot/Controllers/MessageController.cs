﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using TechdaysBot.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TechdaysBot
{
    [BotAuthentication]
    public class MessageController : ApiController
    {
        [BotAuthentication]
        public virtual async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new RootDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        private Activity HandleSystemMessage(Activity message)
        {
            switch (message.Type)
            {
                case ActivityTypes.Ping:
                    {
                        Activity reply = message.CreateReply();
                        reply.Type = ActivityTypes.Ping;
                        return reply;
                    }
                default:
                    return null;
            }
        }
    }
}