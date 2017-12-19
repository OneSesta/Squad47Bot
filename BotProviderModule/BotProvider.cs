using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.Common;

namespace BotProviderModule
{
    class BotProvider : IBotProvider
    {
        IBotApiKeyProvider _keyProvider;
        public BotProvider(IBotApiKeyProvider keyProvider)
        {
            _keyProvider = keyProvider;
        }
        public ITelegramBotClient GetBotClient()
        {
            return new TelegramBotClient(_keyProvider.GetApiKey());
        }
    }
}
