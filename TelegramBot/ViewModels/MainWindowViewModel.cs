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
    using TelegramBot.Commands;
    using TelegramBot.Models;
    using TelegramBot.UICommands;
    using TelegramBot.Views;


    /// <summary>
    /// This class is responsible for the main window.
    /// </summary>
    internal class MainWindowViewModel : ObservableModelBase
    {
        private ITelegramBotClient Bot;
        private UpdateDispatcher dispatcher = new UpdateDispatcher();

        public ITelegramBotClient BotClient
        {
            get { return Bot; }
        }

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

        public MainWindowViewModel()
        {
#if DEBUG
            Bot = new TelegramBotClient("447136859:AAGMz8BN0p21JLO7i9Ob4ridbKTUDpCAD1E");
#else
            Bot = new TelegramBotClient("473027046:AAEWIMqj41Oc33sFIc_yrDcM07XbRDw-Omg");
#endif

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
            dispatcher.AddHandler(new RockPaperScissorsGame(BotClient));
            dispatcher.AddHandler(new RandomBasedCommands(BotClient));
            dispatcher.AddHandler(new FilesAccessor(BotClient));
            dispatcher.AddHandler(new BaseCommands(BotClient));

            var numberCommandsHandler = new PersonsInfoCommands(BotClient);

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
            dispatcher.AddHandler(numberCommandsHandler);
        }


        /// <summary>
        /// Activates the bot (hooks MessageUpdate handler and starts receiving)
        /// </summary>
        public void Activate()
        {
            Bot.OnUpdate += dispatcher.HandleUpdate;
            Bot.StartReceiving(new UpdateType[] { UpdateType.CallbackQueryUpdate, UpdateType.MessageUpdate });
        }
        /// <summary>
        /// Deactivates the bot (unhooks MessageUpdate handler and stops receiving)
        /// </summary>
        public void Deactivate()
        {
            Bot.StopReceiving();
            Bot.OnUpdate -= dispatcher.HandleUpdate;
        }
        #region Old Stuff
        /// <summary>
        /// Processes the inbound message
        /// </summary>
        //private void BotOnUpdateReceived(object sender, UpdateEventArgs e)
        //{
        //var Rnd = new Random();
        //Message msg = e.Message;
        //if (msg == null || msg.Type != MessageType.TextMessage)
        //    return;

        //#region Answer logic
        //String Answer = "";

        //if (msg.Text.StartsWith("/start") || msg.Text.StartsWith("/help"))
        //{
        //    Answer = "Список доступных команд:\r\n" +
        //        "/flip - Подбросить монетку\r\n" +
        //        "/rand - Случайное число от 1 до 47\r\n" +
        //        "/scorebylitvinov - Твоя оценка по Литвинову\r\n" +
        //        "/para - Проверь, нужно ли тебе идти на следующую пару\r\n" +
        //        "/rockpaperscissors - Камень, ножницы, бумага\r\n" +
        //        "/help - Список всех команд";
        //}
        //else if (msg.Text.StartsWith("/flip"))
        //{
        //    // Орел и решка
        //    // для хранения ответа, орел решка
        //    String flipAnswer = "";
        //    int Random = Rnd.Next(0, 2);
        //    if (Random == 0)
        //    {
        //        flipAnswer = "Орел";
        //    }
        //    else
        //    {
        //        flipAnswer = "Решка";
        //    }
        //    Answer = flipAnswer;
        //}
        //else if(msg.Text.StartsWith("/rand"))
        //{
        //    // Случайное число
        //    int Rand = Rnd.Next(0, 48);
        //    Answer = Rand.ToString();
        //}
        //else if(msg.Text.StartsWith("/scorebylitvinov"))
        //{
        //    // Узнай свою оценку по Литвинову
        //    int RandLit = Rnd.Next(1, 11);
        //    if (RandLit < 9)
        //    {
        //        RandLit = Rnd.Next(1, 61);
        //    }
        //    else { RandLit = Rnd.Next(61, 101); }

        //    if (RandLit < 40) { Answer = "По Литвинову ты идешь на пересдачу"; }
        //    else if (RandLit < 60) { Answer = "Твоя оценка по Литвинову: " + RandLit.ToString(); }
        //    else { Answer = "Твоя оценка по Литвинову: " + RandLit.ToString() + ", гнида ебучая"; }

        //}
        //else if(msg.Text.StartsWith("/para"))
        //{
        //    // Проверка на то, нужно ли идти на пару
        //    int Random = Rnd.Next(0, 2);
        //    String paraAnswer = ""; //для хранения ответа, нужно ли идти на пару
        //    if (Random == 0)//Используем из Орла и решки, так как похожий принцип
        //    {
        //        paraAnswer = "лучше сходить";
        //    }
        //    else
        //    {
        //        paraAnswer = "можно не идти";
        //    }

        //    Answer = "На следующую пару " + paraAnswer;
        //}
        //else if (msg.Text.StartsWith("/rockpaperscissors"))
        //{
        //    Answer = "Камень, ножницы, бумага:\r\n" +
        //        "/scissors - Выбрать ножницы (\U0000270C)\r\n" +
        //        "/paper - Выбрать бумагу (\U0000270B)\r\n" +
        //        "/rock - Выбрать камень (\U0000270A)\r\n";
        //}
        //else if (msg.Text.StartsWith("/scissors"))
        //{
        //    //\U0000270A - Камень
        //    //\U0000270B - Бумага
        //    //\U0000270C - Ножницы
        //    int Random = Rnd.Next(0, 3);
        //    switch (Random)
        //    {
        //        case 0:
        //            Answer = "\U0000270C vs \U0000270A\r\n" + "Вы проиграли";
        //            break;
        //        case 1:
        //            Answer = "\U0000270C vs \U0000270C\r\n" + "Ничья";
        //            break;
        //        case 2:
        //            Answer = "\U0000270C vs \U0000270B\r\n" + "Вы победили";
        //            break;
        //    }
        //}
        //else if (msg.Text.StartsWith("/paper"))
        //{
        //    int Random = Rnd.Next(0, 3);
        //    switch (Random)
        //    {
        //        case 0:
        //            Answer = "\U0000270B vs \U0000270A\r\n" + "Вы выиграли";
        //            break;
        //        case 1:
        //            Answer = "\U0000270B vs \U0000270C\r\n" + "Вы проиграли";
        //            break;
        //        case 2:
        //            Answer = "\U0000270B vs \U0000270B\r\n" + "Ничья";
        //            break;
        //    }
        //}
        //else if (msg.Text.StartsWith("/rock"))
        //{
        //    int Random = Rnd.Next(0, 3);
        //    switch (Random)
        //    {
        //        case 0:
        //            Answer = "\U0000270A vs \U0000270A\r\n" + "Ничья";
        //            break;
        //        case 1:
        //            Answer = "\U0000270A vs \U0000270C\r\n" + "Вы победили";
        //            break;
        //        case 2:
        //            Answer = "\U0000270A vs \U0000270B\r\n" + "Вы проиграли";
        //            break;
        //    }
        //}
        //else if (msg.Text.Contains("лаб"))
        //{
        //    try
        //    {
        //        using (FileStream stream = FilesAccessor.GetFileByCommand(msg.Text))
        //        {
        //            await Bot.SendDocumentAsync(msg.Chat.Id, new FileToSend(stream.Name, stream), "Лови", false, msg.MessageId);
        //            Log += $"\r\n\r\n{DateTime.Now.ToLocalTime().ToString()}:\r\nCommand received:\r\n{msg.Text}\r\nFrom: {e.Message.From.FirstName} {e.Message.From.LastName}\r\nAnswered with file: {stream.Name}";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return;
        //    }
        //}
        //#endregion

        //if (Answer!="")
        //{
        //    await Bot.SendTextMessageAsync(msg.Chat.Id, Answer, ParseMode.Default, false, false, msg.MessageId);
        //    Log += $"\r\n\r\n{DateTime.Now.ToLocalTime().ToString()}:\r\nCommand received:\r\n{e.Message.Text}\r\nFrom: {e.Message.From.FirstName} {e.Message.From.LastName}\r\nAnswered with: {Answer}";

        //}
        //}
        #endregion

    }
}
