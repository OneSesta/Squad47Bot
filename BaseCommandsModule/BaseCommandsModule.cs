using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Common;
using Unity;
using Unity.Attributes;

namespace BaseCommandsModule
{
    [Module(ModuleName = "BaseCommandsModule")]
    [ModuleDependency("BotUpdateDispatcherModule")]

    public class BaseCommandsModule : IModule
    {
        IUnityContainer _unityContainer;
        IBotUpdateDispatcher _updateDispatcher;
        IBotLogger _logger;

        public BaseCommandsModule(IUnityContainer unityContainer, IBotUpdateDispatcher updateDispatcher, [OptionalDependency] IBotLogger logger)
        {
            _unityContainer = unityContainer;
            _updateDispatcher = updateDispatcher;
            _logger = logger;
        }

        public void Initialize()
        {
            _unityContainer.RegisterInstance<IBotUpdateHandler>("BaseCommandsModule", new BaseCommands(_updateDispatcher, _logger));
        }
    }
}
