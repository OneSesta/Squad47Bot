namespace TelegramBot
{
    using Prism.Ioc;
    using Prism.Unity;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using TelegramBot.Views;

    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return new MainWindow();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            return;
        }
    }
}