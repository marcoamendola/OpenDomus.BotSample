using OpenDomus.BotSample.Core.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDomus.BotSample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new BotRunner(
                m =>
                {
                    System.Console.WriteLine(m.Text);
                    System.Console.Write("> ");
                },
                System.Console.ReadLine
            );

            //runner.Run(() => new EchoDialog());
            runner.Run(() => new CustomerServiceDialog());   
        }
    }
}
