using LoggerModule.Views;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace LoggerModule
{
    public class LoggerModule : IModule
    {
        IUnityContainer _container;
        IEventAggregator _aggregator;
        IRegionManager _regionManager;

        public LoggerModule(IUnityContainer container, IEventAggregator aggregator, IRegionManager regionManager)
        {
            _container = container;
            _aggregator = aggregator;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterInstance<IBotUpdateLogger>(new BotUpdateLoggerService());
            _regionManager.RegisterViewWithRegion("Log", typeof(LoggerView));
        }
    }
}
