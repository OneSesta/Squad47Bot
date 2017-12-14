using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Common
{
    public interface IBotLogger
    { 
        event Action<string> NewLogEntry;
        void LogUpdate(Update incomingUpdate = null, Message answerMessage = null, string answerQuery = null);
        void LogAction(string action);
    }
}
