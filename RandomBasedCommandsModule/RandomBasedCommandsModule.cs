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

namespace RandomBasedCommandsModule
{
    [Module(ModuleName = "RandomBasedCommandsModule")]
    [ModuleDependency("BotUpdateDispatcherModule")]

    public class RandomBasedCommandsModule : IModule
    {
        IUnityContainer _unityContainer;
        IBotUpdateDispatcher _updateDispatcher;
        IBotLogger _logger;

        public RandomBasedCommandsModule(IUnityContainer unityContainer, IBotUpdateDispatcher updateDispatcher, [OptionalDependency] IBotLogger logger)
        {
            _unityContainer = unityContainer;
            _updateDispatcher = updateDispatcher;
            _logger = logger;
        }

        public void Initialize()
        {
            _unityContainer.RegisterInstance<IBotUpdateHandler>("RandomBasedCommandsModule", new RandomBasedCommands(_updateDispatcher, _logger));
        }
    }
}
