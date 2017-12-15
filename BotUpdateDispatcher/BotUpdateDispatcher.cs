using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Common;

namespace BotUpdateDispatcherModule
{
    public class BotUpdateDispatcher : IBotUpdateDispatcher
    {
        private List<IBotUpdateHandler> handlers;
        private IBotProvider _botProvider;

        public BotUpdateDispatcher(IBotProvider botProvider)
        {
            _botProvider = botProvider;
            handlers = new List<IBotUpdateHandler>();
        }

        public void AddHandler(IBotUpdateHandler handler)
        {
            handlers.Add(handler);
        }

        public void RemoveHandler(IBotUpdateHandler handler)
        {
            handlers.Remove(handler);
        }

        public void HandleUpdate(object sender, UpdateEventArgs update)
        {
            foreach (IBotUpdateHandler handler in handlers)
            {
                if (handler.CanHandleUpdate(update.Update))
                {
                    handler.HandleUpdate(update.Update);
                }
            }
        }

    }
}
