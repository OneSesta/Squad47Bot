using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Common;
using Unity;
using Unity.Attributes;

namespace RockPaperScissorsGameModule
{
    [Module(ModuleName = "RockPaperScissorsGameModule")]
    [ModuleDependency("BotUpdateDispatcherModule")]

    public class RockPaperScissorsGameModule : IModule
    {
        IUnityContainer _unityContainer;
        IBotUpdateDispatcher _updateDispatcher;
        IBotLogger _logger;

        public RockPaperScissorsGameModule(IUnityContainer unityContainer, IBotUpdateDispatcher updateDispatcher, [OptionalDependency] IBotLogger logger)
        {
            _unityContainer = unityContainer;
            _updateDispatcher = updateDispatcher;
            _logger = logger;
        }

        public void Initialize()
        {
            _unityContainer.RegisterInstance<IBotUpdateHandler>("RandomBasedCommandsModule", new RockPaperScissorsGame(_updateDispatcher, _logger));
        }
    }
}
