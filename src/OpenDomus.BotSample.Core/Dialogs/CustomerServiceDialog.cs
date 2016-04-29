using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDomus.BotSample.Core.Dialogs
{
    [LuisModel("49d7e5a7-beb4-40c2-9e01-e8a0cdebdc17", "0e50270c574844cb85480394ad946b4d")]
    [Serializable]
    public class CustomerServiceDialog : LuisDialog<object>
    {
        

        [LuisIntent("SayTime")]
        public async Task SayTime(IDialogContext context, LuisResult result)
        {
            countNone = 0;
            string message = $"Sono le {DateTime.Now.ToString("HH:mm")}";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }

        [LuisIntent("SayDate")]
        public async Task SayDate(IDialogContext context, LuisResult result)
        {
            countNone = 0;
            string message = $"Oggi è {DateTime.Today.ToLongDateString()}";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }

        [LuisIntent("CallOperator")]
        public async Task CallOperator(IDialogContext context, LuisResult result)
        {
            countNone = 0;
            string message = "OK, ti metto in contatto con qualcuno più umano di me.";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }


        [LuisIntent("AskInfoAboutPlan")]
        public async Task AskInfoAboutPlan(IDialogContext context, LuisResult result)
        {
            countNone = 0;
            var planName = string.Join(" ",
                result.Entities
                .Where(e => e.Type == "PlanName")
                .Select(e => e.Entity)
                );

            string message = $"Questi sono i dettagli del piano '{planName}'";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }




        int countNone = 0;
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            #region Improvement

            countNone++;
            if (countNone > 2)
            {
                var child = new PromptConfirmWithDefault(
                    "Vuoi che ti passi un operatore più sveglio di me?",
                    "Non ho capito, vuoi parlare con un operatore?",
                    attempts: 2,
                    defaultResult: true
                    );

                context.Call(child, AfterDialog);
                return;
            }

            #endregion

            string message = "Scusa, non ho capito.";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }



        async Task AfterDialog(IDialogContext context, IAwaitable<bool> argument)
        {
            var result = await argument;

            if (result)
            {
                await CallOperator(context, null);
            }
            else
            {
                countNone = 0;
                string message = "OK.";
                await context.PostAsync(message);

                context.Wait(MessageReceived);
            }
        }
    }
}
