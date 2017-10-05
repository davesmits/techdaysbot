using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace TechdaysBot.Dialogs
{
    [Serializable]
    [LuisModel("df2316dd-a2e5-4b39-9a0f-25df97c475dd", "fcb4de503df04f81aa53284ec789c4d5")]
    public class RootDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.SayAsync("hello", "hello");
        }

        



    }
}