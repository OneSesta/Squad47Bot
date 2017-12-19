using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Common
{
    /// <summary>
    /// Should perform logging tasks and notify about this.
    /// </summary>
    public interface IBotLogger
    { 
        /// <summary>
        /// Should be raised when new log entry is incoming
        /// </summary>
        event Action<string> NewLogEntry;

        /// <summary>
        /// Performs all actions of logging
        /// </summary>
        /// <param name="incomingUpdate">Incoming Update (for example, message, button callback query...</param>
        /// <param name="answerMessage">Message sent as answer on incoming Update</param>
        /// <param name="answerQuery">Or/and popup sent</param>
        void LogUpdate(Update incomingUpdate = null, Message answerMessage = null, string answerQuery = null);

        /// <summary>
        /// Logs some string.
        /// </summary>
        /// <example>LogAction("Initializing bot...")</example>
        /// <param name="action"></param>
        void LogAction(string action);
    }
}
