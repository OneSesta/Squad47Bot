using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Common;
using Unity;

namespace MenuItemsModule
{
    public class MenuItemsModule : IModule
    {
        private IUnityContainer _container;

        public MenuItemsModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterInstance<IMenuItemsService>(new MenuItemsService());
        }
    }
}
