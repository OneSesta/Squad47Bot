using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Common;
using Unity;

namespace ApiKeyModule
{
    [Module(ModuleName = "ApiKeyProviderModule")]
    public class ApiKeyModule : IModule
    {
        IUnityContainer _container;

        public ApiKeyModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterInstance(typeof(IBotApiKeyProvider), new ApiKeyService());
        }
    }
}
