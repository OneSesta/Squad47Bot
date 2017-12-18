using PersonsInfoModule.ViewModels;
using PersonsInfoModule.Views;
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

namespace PersonsInfoModule
{
    [Module(ModuleName = "PersonsInfoModule")]
    [ModuleDependency("BotUpdateDispatcherModule")]

    public class PersonsInfoModule : IModule
    {
        IUnityContainer _unityContainer;
        IRegionManager _regionManager;
        IBotUpdateDispatcher _updateDispatcher;
        IBotLogger _logger;
        PersonsInfoView view;

        public PersonsInfoModule(IUnityContainer unityContainer, IRegionManager regionManager, IBotUpdateDispatcher updateDispatcher, [OptionalDependency] IBotLogger logger)
        {
            _unityContainer = unityContainer;
            _regionManager = regionManager;
            _updateDispatcher = updateDispatcher;
            _logger = logger;
        }

        public void Initialize()
        {
            var personsInfo = new PersonsInfo(_updateDispatcher, _logger);
            _unityContainer.RegisterInstance<IBotUpdateHandler>("PersonsInfoModule", personsInfo);
            view = new PersonsInfoView(new PersonsInfoViewModel(personsInfo));
            _regionManager.RegisterViewWithRegion("ModulesSettingsRegion", () => view);
        }
    }
}
