using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBot.Common;
using Unity;

namespace BotModule
{
    public class Bot : IBot
    {

        public ICommand ActivateCommand
        {
            get;
            private set;
        }

        public ICommand DeactivateCommand
        {
            get;
            private set;
        }

        private ITelegramBotClient _botClient;
        private IBotUpdateDispatcher _dispatcher;
        private IBotLogger _logger;
        private IMenuItemsService _menuItems;

        public Bot(ITelegramBotClient botClient, IBotUpdateDispatcher dispatcher, IBotLogger logger, IMenuItemsService menuItems)
        {
            _logger = logger;
            _dispatcher = dispatcher;
            _botClient = botClient;
            _menuItems = menuItems;

            _logger?.LogAction("Initializing...");
            // initializing ViewModel UI commands
            ActivateCommand = new RelayCommand<object>(o => this.Activate(), o => !IsActive);
            DeactivateCommand = new RelayCommand<object>(o => this.Deactivate(), o => IsActive);

            menuItems.MenuItems.Add(new MenuItemViewModel()
            {
                Text = "Start",
                ImageSource = new Uri(@"/BotModule;component/Start.png", UriKind.RelativeOrAbsolute),
                Command = ActivateCommand
            });
            menuItems.MenuItems.Add(new MenuItemViewModel()
            {
                Text = "Stop",
                ImageSource = new Uri(@"/BotModule;component/Stop.png", UriKind.RelativeOrAbsolute),
                Command = DeactivateCommand
            });

            ActivateCommand.Execute(null);
            Application.Current.Exit += (o, e) =>
            {
                if (IsActive)
                    Deactivate();
            };
        }

        /// <summary>
        /// Activates the bot (hooks MessageUpdate handler and starts receiving)
        /// </summary>
        public void Activate()
        {
            _botClient.OnUpdate += _dispatcher.HandleUpdate;
            _botClient.StartReceiving(new UpdateType[] { UpdateType.CallbackQueryUpdate, UpdateType.MessageUpdate });
            _logger?.LogAction("Bot activated");
        }
        /// <summary>
        /// Deactivates the bot (unhooks MessageUpdate handler and stops receiving)
        /// </summary>
        public void Deactivate()
        {
            _botClient.StopReceiving();
            _botClient.OnUpdate -= _dispatcher.HandleUpdate;
            _logger?.LogAction("Bot deactivated");
        }

        public bool IsActive
        {
            get
            {
                return _botClient.IsReceiving;
            }
        }
    }
}
