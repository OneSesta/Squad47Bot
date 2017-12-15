using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot.Common
{
    public interface IBotProvider
    {
        ITelegramBotClient GetBotClient();
    }
}
