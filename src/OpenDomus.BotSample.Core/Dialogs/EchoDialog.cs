using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDomus.BotSample.Core.Dialogs
{
    [Serializable]
    public class EchoDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        
        async Task MessageReceivedAsync(IDialogContext context, IAwaitable<Message> argument)
        {
            var message = await argument;
            
            var reply = string.Empty;
            if (!string.IsNullOrWhiteSpace(message.Text))
            {
                if (message.Text.Equals("addio", StringComparison.InvariantCultureIgnoreCase))
                    reply = null;
                else
                    reply = "Hai appena detto: " + message.Text;
            }

            await context.PostAsync(reply);
            context.Wait(MessageReceivedAsync);
        }


    }
}
