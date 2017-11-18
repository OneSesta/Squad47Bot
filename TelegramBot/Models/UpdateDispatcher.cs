namespace TelegramBot.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Telegram.Bot.Args;
    using Telegram.Bot.Types;

    class UpdateDispatcher
    {
        private List<IBotCommandHandler> handlers;

        public UpdateDispatcher()
        {
            handlers = new List<IBotCommandHandler>();
        }

        public void AddHandler(IBotCommandHandler handler)
        {
            handlers.Add(handler);
        }

        public void HandleUpdate(object sender, UpdateEventArgs update)
        {
            foreach (IBotCommandHandler handler in handlers)
            {
                if (handler.CanHandleUpdate(update.Update))
                {
                    handler.HandleUpdate(update.Update);
                }
            }
        }
    }
}
