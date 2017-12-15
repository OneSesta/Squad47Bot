using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TelegramBot.Common
{
    public interface IBotUpdateDispatcher
    {
        void AddHandler(IBotUpdateHandler handler);
        void RemoveHandler(IBotUpdateHandler handler);
        void HandleUpdate(object sender, UpdateEventArgs args);
    }
}
