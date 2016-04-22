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


        
    }
}
