using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    internal static class Utils
    {
#if DEBUG
        static public string BotPostfix = "Squad47Testbot";
        static public string BotTitle = "Squad 47 Bot: DEBUG";
#else
        static public string BotPostfix = "Squad47bot";
        static public string BotTitle = "Squad 47 Bot: RELEASE";
#endif
        /// <summary>
        /// Command trim, to lower, and without bot prefix.
        /// </summary>
        /// <param name="command">Command to prettify</param>
        /// <returns>Pretty command :3</returns>
        static public string PrettifyCommand(string command)
        {
            string request = command.Trim();

            //removing @47bot-like suffix
            if (request.EndsWith("@" + Utils.BotPostfix))
                request = request.Remove(request.Length - (Utils.BotPostfix.Length + 1));

            return request.ToLower();
        }
    }
}
