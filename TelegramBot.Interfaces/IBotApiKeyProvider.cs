using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Common
{
    /// <summary>
    /// Performs work of retrieving Telegram Bot API key.
    /// TelegramBotClient should then be constructed using this key.
    /// </summary>
    public interface IBotApiKeyProvider
    {
        /// <summary>
        /// Should provide Telegram Bot API key
        /// </summary>
        /// <returns>API key</returns>
        string GetApiKey();
    }
}
