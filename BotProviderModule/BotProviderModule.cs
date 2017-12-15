using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.Common;
using Unity;

namespace BotProviderModule
{
    [Module(ModuleName = "BotProviderModule")]
    [ModuleDependency("ApiKeyProviderModule")]
    public class BotProviderModule : IModule
    {
        private IBotApiKeyProvider _keyProvider;
        private IUnityContainer _container;

        public BotProviderModule(IBotApiKeyProvider keyProvider, IUnityContainer container)
        {
            _container = container;
            _keyProvider = keyProvider;
        }

        public void Initialize()
        {
            _container.RegisterInstance(new BotProvider(_keyProvider).GetBotClient());
        }
    }
}
