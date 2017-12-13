using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace LoggerModule
{
    public interface IBotUpdateLogger
    { 
        event Action<string> NewLogEntry;
        void Log(Update incomingUpdate = null, Message answerMessage = null, string answerQuery = null);
    }
}
