namespace TelegramBot.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Telegram.Bot.Args;
    using Telegram.Bot.Types;

    internal class UpdateDispatcher
    {
        private List<IBotUpdateHandler> handlers;

        public UpdateDispatcher()
        {
            handlers = new List<IBotUpdateHandler>();
        }

        public void AddHandler(IBotUpdateHandler handler)
        {
            handlers.Add(handler);
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
