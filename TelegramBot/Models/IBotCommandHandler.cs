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
        /// <summary>
        /// Handles given bot update (sends answers or buttons feedback)
        /// </summary>
        /// <param name="update">Update to handle</param>
        void HandleUpdate(Update update);

        /// <summary>
        /// Checks if bot update can be handled by the handler
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        bool CanHandleUpdate(Update update);
    }
}
