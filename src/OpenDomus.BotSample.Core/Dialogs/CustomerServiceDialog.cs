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

        public CustomerServiceDialog()
        {

        }


        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = "Scusa, non ho capito.";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }

        [LuisIntent("SayTime")]
        public async Task SayTime(IDialogContext context, LuisResult result)
        {
            string message = $"Sono le {DateTime.Now.ToString("HH:mm")}";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }

        [LuisIntent("SayDate")]
        public async Task SayDate(IDialogContext context, LuisResult result)
        {
            string message = $"Oggi è {DateTime.Today.ToLongDateString()}";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }

        [LuisIntent("CallOperator")]
        public async Task CallOperator(IDialogContext context, LuisResult result)
        {
            string message = "OK, ti metto in contatto con qualcuno più umano di me.";
            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }
    }
}
