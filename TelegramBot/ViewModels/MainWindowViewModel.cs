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
    using TelegramBot.Models;
    using TelegramBot.Views;
    using TelegramBot.Common;
    using Unity;
    using Prism.Modularity;


    /// <summary>
    /// This class is responsible for the main window.
    /// </summary>
    /*[Module]
    [ModuleDependency("BotProviderModule")]
    [ModuleDependency("LoggerModule")]
    [ModuleDependency("BotUpdateDispatcherModule")]*/
    public class MainWindowViewModel : ObservableModelBase, IModule
    {
        private ITelegramBotClient Bot;
        private IUnityContainer _unityContainer;
        private IBotUpdateDispatcher _dispatcher;

        public void Initialize()
        {
        }

        private IBotLogger _logger;
        
        public bool IsActive
        {
            get
            {
                return Bot.IsReceiving;
            }
        }

        #region Commands
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

        public ICommand OpenFilesFolderCommand
        {
            get;
            private set;
        }

        public ICommand ExitApplicationCommand
        {
            get;
            private set;
        }

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
        public ICommand SaveInfoCommand
        {
            get;
            private set;
        }

        #endregion

        #region Student Info
        private string _info = "";
        public string Info
        {
            get
            {
                return _info;
            }
            private set
            {
                _info = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Decodes list of persons to string to show in TextBox
        /// </summary>
        /// <param name="encodeFrom">List of persons</param>
        /// <returns></returns>
        public string DecodeInfo(List<Person> encodeFrom)
        {
            string result = "";
            foreach (Person p in encodeFrom)
            {
                result += $"{p.LastName} {p.FirstName} {p.Patronymic} {p.PhoneNumber}\r\n";
            }
            return result;
        }

        /// <summary>
        /// Encodes string from TextBox into List of Persons
        /// </summary>
        /// <param name="encodeFrom">String from TextBox</param>
        /// <returns></returns>
        public List<Person> EncodeInfo(string encodeFrom)
        {
            List<Person> persons = new List<Person>();
            string[] personsLines = encodeFrom.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in personsLines)
            {
                string[] splitted = line.Split(' ');
                persons.Add(new Person(splitted[0], splitted[1], splitted[2], splitted.Length>=4?splitted[3]:""));
            }
            return persons;
        }
        #endregion


        public MainWindowViewModel(IUnityContainer unityContainer, IBotProvider botProvider, IBotUpdateDispatcher dispatcher, IBotLogger logger)
        {
            _logger = logger;
            _unityContainer = unityContainer;
            _dispatcher = dispatcher;

            Bot = botProvider.GetBotClient();

            _logger.LogAction("Initializing...");
            // initializing ViewModel UI commands
            ActivateCommand = new RelayCommand<object>(o => this.Activate(), o => !IsActive);
            DeactivateCommand = new RelayCommand<object>(o => this.Deactivate(), o => IsActive);
            ExitApplicationCommand = new RelayCommand<object>(o =>
            {
                if (IsActive)
                    Deactivate();
                App.Current.Shutdown();
            }, (o) => true);
            OpenFilesFolderCommand = new RelayCommand<object>(o => Process.Start(@"файлы\"), o => true);
            OpenAboutCommand = new OpenAboutWindowCommand();
            OpenLocalFilesCommand = new RelayCommand<object>(o => Process.Start(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)), o => true);

            // create files directory and open it, if it doesn't exist
            if (!Directory.Exists(@"файлы\"))
            {
                Directory.CreateDirectory(@"файлы\");
                OpenFilesFolderCommand.Execute(null);
            }

            // auto-activate on startup
            ActivateCommand.Execute(null);

            // to the end: adding bot command handlers
            _dispatcher.AddHandler(new RockPaperScissorsGame(Bot));
            _dispatcher.AddHandler(new RandomBasedCommands(Bot));
            _dispatcher.AddHandler(new FilesAccessor(Bot));
            //dispatcher.AddHandler(new BaseCommands(Bot));

            var numberCommandsHandler = new PersonsInfoCommands(Bot);

            // bind Save command to handler
            SaveInfoCommand = new RelayCommand<object>(o =>
            {
                numberCommandsHandler.Persons = EncodeInfo(o as string);
                numberCommandsHandler.SaveInfo();
            }, o =>
            {
                string info = DecodeInfo(numberCommandsHandler.Persons);
                return (o as string)?.Trim() != info.Trim();
            });

            numberCommandsHandler.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Persons")
                    {
                        Info = DecodeInfo((o as PersonsInfoCommands).Persons);
                    }
                };
            Info = DecodeInfo(numberCommandsHandler.Persons);
            _dispatcher.AddHandler(numberCommandsHandler);
        }


        /// <summary>
        /// Activates the bot (hooks MessageUpdate handler and starts receiving)
        /// </summary>
        public void Activate()
        {
            Bot.OnUpdate += _dispatcher.HandleUpdate;
            Bot.StartReceiving(new UpdateType[] { UpdateType.CallbackQueryUpdate, UpdateType.MessageUpdate });
            _logger.LogAction("Bot activated");
        }
        /// <summary>
        /// Deactivates the bot (unhooks MessageUpdate handler and stops receiving)
        /// </summary>
        public void Deactivate()
        {
            Bot.StopReceiving();
            Bot.OnUpdate -= _dispatcher.HandleUpdate;
            _logger.LogAction("Bot deactivated");
        }

    }
}
