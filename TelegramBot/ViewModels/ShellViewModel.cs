namespace TelegramBot.ViewModels
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Windows.Input;
    using Telegram.Bot;
    using Telegram.Bot.Args;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using TelegramBot.UICommands;
    using TelegramBot.Views;
    using TelegramBot.Common;
    using Unity;
    using Prism.Modularity;
    using Unity.Attributes;


    /// <summary>
    /// This class is responsible for the main window (aka Shell)
    /// </summary>
    public class ShellViewModel : ObservableModelBase
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        #region Commands

        public ICommand OpenLocalFilesCommand
        {
            get;
            private set;
        }

        public ICommand OpenAboutCommand
        {
            get;
            private set;
        }

        #endregion

        private IBot _bot;
        private IMenuItemsService _menuItemsService;

        public ShellViewModel(IBot bot, IMenuItemsService menuItemsService)
        {
            _bot = bot;
            _menuItemsService = menuItemsService;

            OpenLocalFilesCommand = new RelayCommand<object>(o => Process.Start(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)), o => true);
            OpenAboutCommand = new OpenAboutWindowCommand();
            _menuItemsService.MenuItems.Insert(0, new MenuItemViewModel()
            {
                Text = "File",
                Children = new ObservableCollection<MenuItemViewModel>
                    {
                        new MenuItemViewModel()
                        {
                            Text="Open local files",
                            Command=OpenLocalFilesCommand
                        }
                    }
            });
            _menuItemsService.MenuItems.Add(new MenuItemViewModel()
            {
                Text = "Help",
                Children = new ObservableCollection<MenuItemViewModel>
                    {
                        new MenuItemViewModel()
                        {
                            Text="Sorry bro, we can't help you"
                        },
                        new MenuItemViewModel()
                        {
                            Text="About",
                            Command=OpenAboutCommand
                        }
                    }
            });
            MenuItems = _menuItemsService.MenuItems;
        }

    }
}
