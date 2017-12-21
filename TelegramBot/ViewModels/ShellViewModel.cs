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
    //[ModuleDependency("BotProviderModule")]
    //[ModuleDependency("BotUpdateDispatcherModule")]
    public class ShellViewModel : ObservableModelBase
    {
        //private ITelegramBotClient _botClient;
        //private IUnityContainer _unityContainer;
        //private IBotUpdateDispatcher _dispatcher;
        //private IBotLogger _logger;

        //private ObservableCollection<MenuItemViewModel> _menuItems;
        //public ObservableCollection<MenuItemViewModel> MenuItems
        //{
        //    get
        //    {
        //        return _menuItems;
        //    }
        //    set
        //    {
        //        _menuItems = value;
        //        OnPropertyChanged();
        //    }
        //}

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        //public bool IsActive
        //{
        //    get
        //    {
        //        return _botClient.IsReceiving;
        //    }
        //}

        #region Commands
        //public ICommand ActivateCommand
        //{
        //    get;
        //    private set;
        //}

        //public ICommand DeactivateCommand
        //{
        //    get;
        //    private set;
        //}

        /*public ICommand OpenFilesFolderCommand
        {
            get;
            private set;
        }*/

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

        public ShellViewModel(/*IUnityContainer unityContainer, ITelegramBotClient botClient, IBotUpdateDispatcher dispatcher, [OptionalDependency] IBotLogger logger*/ IBot bot, IMenuItemsService menuItemsService)
        {
            //_logger = logger;
            //_unityContainer = unityContainer;
            //_dispatcher = dispatcher;
            //_botClient = botClient;

            _bot = bot;
            _menuItemsService = menuItemsService;

            OpenLocalFilesCommand = new RelayCommand<object>(o => Process.Start(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)), o => true);
            _menuItemsService.MenuItems = new ObservableCollection<MenuItemViewModel>()
            {
                new MenuItemViewModel()
                {
                    Text="File",
                    Children = new ObservableCollection<MenuItemViewModel>
                    {
                        new MenuItemViewModel()
                        {
                            Text="Open local files",
                            Command=OpenLocalFilesCommand
                        }
                    }
                }
            };
            MenuItems = _menuItemsService.MenuItems;

            //_logger?.LogAction("Initializing...");
            //// initializing ViewModel UI commands
            //ActivateCommand = new RelayCommand<object>(o => this.Activate(), o => !IsActive);
            //DeactivateCommand = new RelayCommand<object>(o => this.Deactivate(), o => IsActive);
            //ExitApplicationCommand = new RelayCommand<object>(o =>
            //{
            //    if (IsActive)
            //        Deactivate();
            //    App.Current.Shutdown();
            //}, (o) => true);
            OpenAboutCommand = new OpenAboutWindowCommand();
            //ActivateCommand.Execute(null);
        }


        ///// <summary>
        ///// Activates the bot (hooks MessageUpdate handler and starts receiving)
        ///// </summary>
        //public void Activate()
        //{
        //    _botClient.OnUpdate += _dispatcher.HandleUpdate;
        //    _botClient.StartReceiving(new UpdateType[] { UpdateType.CallbackQueryUpdate, UpdateType.MessageUpdate });
        //    _logger.LogAction("Bot activated");
        //}
        ///// <summary>
        ///// Deactivates the bot (unhooks MessageUpdate handler and stops receiving)
        ///// </summary>
        //public void Deactivate()
        //{
        //    _botClient.StopReceiving();
        //    _botClient.OnUpdate -= _dispatcher.HandleUpdate;
        //    _logger.LogAction("Bot deactivated");
        //}

    }
}
