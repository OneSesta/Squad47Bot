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

namespace FilesAccessorModule
{
    [Module(ModuleName = "FilesAccessorModule")]
    [ModuleDependency("BotUpdateDispatcherModule")]

    public class FilesAccessorModule : IModule
    {
        IUnityContainer _unityContainer;
        IBotUpdateDispatcher _updateDispatcher;
        IBotLogger _logger;

        public FilesAccessorModule(IUnityContainer unityContainer, IBotUpdateDispatcher updateDispatcher, [OptionalDependency] IBotLogger logger)
        {
            _unityContainer = unityContainer;
            _updateDispatcher = updateDispatcher;
            _logger = logger;
        }

        public void Initialize()
        {
            _unityContainer.RegisterInstance<IBotUpdateHandler>("FilesAccessorModule", new FilesAccessor(_updateDispatcher, _logger));
        }
    }
}
