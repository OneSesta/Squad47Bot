using LoggerViewerModule.ViewModels;
using LoggerViewerModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Common;

namespace LoggerViewerModule
{
    [Module]
    [ModuleDependency("LoggerModule")]
    public class LoggerViewerModule : IModule
    {
        private IRegionManager _regionManager;
        private IBotLogger _logger;
        private LoggerView _view;

        public LoggerViewerModule(IRegionManager regionManager, IBotLogger logger)
        {
            _regionManager = regionManager;
            _logger = logger;
            _view = new LoggerView(new LoggerViewModel(_logger));
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ModulesSettingsRegion", () => _view);
        }
    }
}
