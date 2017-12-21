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
using Unity.Attributes;

namespace BotModule
{
    [Module(ModuleName = "BotModule")]
    [ModuleDependency("BotProviderModule")]
    [ModuleDependency("BotUpdateDispatcherModule")]
    public class BotModule : IModule
    {
        private ITelegramBotClient _botClient;
        private IUnityContainer _unityContainer;
        private IBotUpdateDispatcher _dispatcher;
        private IBotLogger _logger;

        public BotModule(IUnityContainer unityContainer, ITelegramBotClient botClient, IBotUpdateDispatcher dispatcher, [OptionalDependency] IBotLogger logger)
        {
            _logger = logger;
            _unityContainer = unityContainer;
            _dispatcher = dispatcher;
            _botClient = botClient;
        }

        public void Initialize()
        {
            _unityContainer.RegisterInstance<IBot>(new Bot(_botClient, _dispatcher, _logger));
        }
    }
}
