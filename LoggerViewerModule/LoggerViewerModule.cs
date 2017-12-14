using LoggerViewerModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerViewerModule
{
    public class LoggerViewerModule : IModule
    {
        private IRegionManager _regionManager;

        public LoggerViewerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ModulesSettingsRegion", typeof(LoggerView));
        }
    }
}
