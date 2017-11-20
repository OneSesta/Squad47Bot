namespace TelegramBot.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Telegram.Bot.Types;

    interface IBotCommandHandler
    {
        void HandleUpdate(Update update);
        bool CanHandleUpdate(Update update);
    }
}
