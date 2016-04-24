using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDomus.BotSample.Core.Dialogs
{
    public static class CustomPromptDialog
    {
        public static void Confirm(IDialogContext context, ResumeAfter<bool> resume, string prompt, string retry = null, int attempts = 3, bool? defaultResult = null)
        {
            var child = new PromptConfirmWithDefault(prompt, retry, attempts, defaultResult);
            context.Call<bool>(child, resume);
        }
         
    }
}
