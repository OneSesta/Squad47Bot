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

namespace BotUpdateDispatcherModule
{
    [Module(ModuleName = "BotUpdateDispatcherModule")]
    [ModuleDependency("BotProviderModule")]
    public class BotUpdateDispatcherModule : IModule
    {
        private IBotProvider _botProvider;
        private IUnityContainer _container;

        public BotUpdateDispatcherModule(IBotProvider botProvider, IUnityContainer container)
        {
            _botProvider = botProvider;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterInstance<IBotUpdateDispatcher>(new BotUpdateDispatcher(_botProvider));
        }
    }
}
