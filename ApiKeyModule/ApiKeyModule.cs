using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Interfaces;
using Unity;

namespace ApiKeyModule
{
    public class ApiKeyModule : IModule
    {
        IUnityContainer _container;

        public ApiKeyModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterInstance(typeof(IBotApiKeyService), new ApiKeyService());
        }
    }
}
