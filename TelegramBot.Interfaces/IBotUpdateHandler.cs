using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Common
{
    /// <summary>
    /// Should perform handling of incoming Update,
    /// such as answering message with requested info
    /// </summary>
    public interface IBotUpdateHandler
    {
        /// <summary>
        /// Handles given bot update (sends answers or buttons feedback)
        /// </summary>
        /// <param name="update">Update to handle</param>
        void HandleUpdate(Update update, ITelegramBotClient botClient);

        /// <summary>
        /// Checks if bot update can be handled by the handler
        /// </summary>
        /// <param name="update"></param>
        /// <returns>True if handler can handle bot update, false otherwise</returns>
        bool CanHandleUpdate(Update update, ITelegramBotClient botClient);
    }
}
