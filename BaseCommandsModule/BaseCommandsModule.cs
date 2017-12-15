using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Common;
using Unity;

namespace BaseCommandsModule
{
    class BaseCommandsModule : IModule
    {
        IUnityContainer _unityContainer;

        public BaseCommandsModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterInstance<IBotUpdateHandler>("BaseCommandsModule", new BaseCommands());
        }
    }
}
