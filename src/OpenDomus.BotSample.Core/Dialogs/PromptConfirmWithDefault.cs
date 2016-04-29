using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Bot.Builder.Dialogs.PromptDialog;

namespace OpenDomus.BotSample.Core.Dialogs
{
    
    /// <summary>   Prompt for a confirmation with a default. </summary>
    /// <remarks>   Normally used through <see cref="PromptDialog.Confirm(IDialogContext, ResumeAfter{bool}, string, string, int)"/>.</remarks>
    [Serializable]
    public sealed class PromptConfirmWithDefault : Prompt<bool>
    {
        bool? defaultResult;

        /// <summary>   Constructor for a prompt confirmation dialog. </summary>
        /// <param name="prompt">   The prompt. </param>
        /// <param name="retry">    What to display on retry. </param>
        /// <param name="attempts"> Maximum number of attempts. </param>
        /// <param name="defaultResult"> Default result. </param>
        public PromptConfirmWithDefault(string prompt, string retry, int attempts, bool? defaultResult)
            : base(prompt, retry, attempts)
        {
            this.defaultResult = defaultResult;
        }


        readonly string[] MatchYesList = "s,si,sì,certo,ok,va bene,sicuro,sicuramente".Split(',');
        readonly string[] MatchNoList = "n,no,giammai".Split(',');
        protected override bool TryParse(Message message, out bool result)
        {
            var found = false;
            result = false;
            if (message.Text != null)
            {
                var term = message.Text.Trim().ToLower();
                if ((from r in MatchYesList select r.ToLower()).Contains(term))
                {
                    result = true;
                    found = true;
                }
                else if ((from r in MatchNoList select r.ToLower()).Contains(term))
                {
                    result = false;
                    found = true;
                }
            }

            if (!found && attempts<=1 && defaultResult.HasValue) {
                found = true;
                result = defaultResult.Value;
            }
            return found;
        }

        protected override string DefaultRetry
        {
            get
            {
                return "Riprova\n" + this.prompt;
            }
        }
    }




}
