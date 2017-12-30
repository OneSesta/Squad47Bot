namespace TelegramBot
{
    using ApiKeyModule;
    using CommonServiceLocator;
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Unity;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using TelegramBot.Common;
    using TelegramBot.ViewModels;
    using TelegramBot.Views;
    using Unity;
    using Unity.Extension;
    using Unity.ServiceLocation;

    public partial class App : PrismApplication
    {
        /// <summary>
        /// Create Shell
        /// </summary>
        protected override Window CreateShell()
        {
            InitializeModules();
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
            var shell = Container.Resolve<Shell>();
            this.MainWindow = shell;
            return shell;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        /// <summary>
        /// Creating catalog of Modules from .dlls in "Modules" folder
        /// </summary>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new DirectoryModuleCatalog() { ModulePath = @"./Modules" };
            catalog.Initialize();
            return catalog;
        }
    }
}