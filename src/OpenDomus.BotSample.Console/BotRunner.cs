using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDomus.BotSample.Console
{
    public class BotRunner
    {

        Action<Message> displayBotMessage;
        Func<string> getUserMessage;
        public BotRunner(Action<Message> displayBotMessage, Func<string> getUserMessage)
        {
            this.displayBotMessage = displayBotMessage;
            this.getUserMessage = getUserMessage;
        }

        public void Run<R>(Func<IDialog<R>> rootDialogFactory)
        {
            var message = new Message()
            {
                ConversationId = Guid.NewGuid().ToString(),
                Text = ""
            };

            while (true)
            {
                message = Conversation
                    .SendAsync(message, rootDialogFactory)
                    .Result;

                if (message.Text == null) return;

                displayBotMessage(message);

                message.Text = getUserMessage();
            }
        }
    }
}
