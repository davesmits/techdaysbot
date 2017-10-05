using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace TechdaysBot.Dialogs
{
    [Serializable]
    [LuisModel("5e7d3ace-3dfe-4458-bb48-7c38287b7be5", "fcb4de503df04f81aa53284ec789c4d5")]
    public class RootDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Ik begrijp je niet");
        }

        [LuisIntent("NikkiPublic.Contact")]
        public async Task Contact(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Wil je een van ons spreken? Bel gerust met 0204420620 en maak een afspraak. Of vul ons contactformulier in en geef aan dat je een afspraak wilt maken.");
        }

        [LuisIntent("NikkiPublic.Jobs")]
        public async Task Jobs(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Benieuwd naar onze vacatures? Check hier onze vacatures.");
        }

        [LuisIntent("NikkiPublic.Missie")]
        public async Task MissieAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Wij versnellen elke organisatie met digitale transformatie. Met Microsoft-technologie op kop, en de inspiratie en strategie om slimme keuzes te maken. Met de beste mensen en de beste oplossingen die er zijn. Innovatie die zorgt voor blijvend resultaat. Meer weten over InSpark? Lees meer over ons ");
        }

        [LuisIntent("NikkiPublic.References")]
        public async Task ReferencesAsync(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Wij zijn erg trots op onze successen bij onze klanten. Lees hier onze klantverhalen ");
        }

        [LuisIntent("NikkiPublic.Search")]
        public async Task Search(IDialogContext context, LuisResult result)
        {
            if (result.TryFindEntity("Tag", out EntityRecommendation tagEntity))
            {
                await context.PostAsync($"Je hebt gezocht op {tagEntity.Entity}");
            }
        }



    }
}