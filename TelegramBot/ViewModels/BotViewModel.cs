namespace TelegramBot.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Telegram.Bot;
    using Telegram.Bot.Args;
    using Telegram.Bot.Types.Enums;
    using TelegramBot.Commands;

    class BotViewModel : INotifyPropertyChanged
    {
        private static TelegramBotClient Bot;

        public TelegramBotClient BotClient
        {
            get { return Bot; }
        }

        public bool IsActive{
            get
            {
                return Bot.IsReceiving;
            }
        }

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

        public ICommand ExitApplicationCommand
        {
            get;
            private set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string _log;
        public string Log
        {
            get
            {
                return _log;
            }
            set
            {
                _log = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Log"));
            }
        }

        public BotViewModel()
        {
#if DEBUG
            Bot = new TelegramBotClient("447136859:AAGMz8BN0p21JLO7i9Ob4ridbKTUDpCAD1E");
#else
            Bot = new TelegramBotClient("488598835:AAFJAn6w1rifdR6z-8wDsaSxvwXXDVusSgU");
#endif
            ActivateCommand = new ActivateBotCommand(this);
            DeactivateCommand = new DeactivateBotCommand(this);
            ExitApplicationCommand = new ExitApplicationCommand(this);
        }

        public void Activate()
        {
            Bot.OnMessage += BotOnMessageReceived;
            Bot.StartReceiving(new UpdateType[] { UpdateType.MessageUpdate });
        }
        public void Deactivate()
        {
            Bot.StopReceiving();
            Bot.OnMessage -= BotOnMessageReceived;
        }
        private async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            Random Rnd = new Random();
            Telegram.Bot.Types.Message msg = e.Message;
            if (msg == null || msg.Type != MessageType.TextMessage) return;

            String Answer = "";






            //Орел и решка
            String flipAnswer = ""; //для хранения ответа, орел решка
            int Random = Rnd.Next(0, 2);
            if (Random == 0)
            {
                flipAnswer = "Орел";
            }
            else
            {
                flipAnswer = "Решка";
            }



            //Случайное число
            int Rand = Rnd.Next(0, 48);





            //Узнай свою оценку по Литвинову
            int RandLit = Rnd.Next(1, 11);
            switch (RandLit)
            {
                case 1:
                    RandLit = Rnd.Next(1, 44);
                    break;
                case 2:
                    RandLit = Rnd.Next(10, 44);
                    break;
                case 3:
                    RandLit = Rnd.Next(10, 51);
                    break;
                case 4:
                    RandLit = Rnd.Next(15, 61);
                    break;
                case 5:
                    RandLit = Rnd.Next(15, 61);
                    break;
                case 6:
                    RandLit = Rnd.Next(15, 61);
                    break;
                case 7:
                    RandLit = Rnd.Next(15, 66);
                    break;
                case 8:
                    RandLit = Rnd.Next(20, 75);
                    break;
                case 9:
                    RandLit = Rnd.Next(50, 75);
                    break;
                case 10:
                    RandLit = Rnd.Next(65, 100);
                    break;
            }


            //Проверка на то, нужно ли идти на пару
            String paraAnswer = ""; //для хранения ответа, нужно ли идти на пару
            if (Random == 0)//Используем из Орла и решки, так как похожий принцип
            {
                paraAnswer = "лучше сходить";
            }
            else
            {
                paraAnswer = "можно не идти";
            }



            switch (msg.Text)
            {
                case "/start": Answer = "/flip - подбросить монетку\r\n/rand - случайное число от 1 до 47\r\n/scorebylitvinov - твоя оценка по Литвинову"; break;
                case "/flip": Answer = flipAnswer; break;
                case "/flip@Squad47bot": Answer = flipAnswer; break;

                case "/rand": Answer = Rand.ToString(); break;
                case "/rand@Squad47bot": Answer = Rand.ToString(); break;

                case "/scorebylitvinov": Answer = "Твоя оценка по Литвинову: " + RandLit.ToString(); break;
                case "/scorebylitvinov@Squad47bot": Answer = "Твоя оценка по Литвинову: " + RandLit.ToString(); break;

                case "/para": Answer = "На следующую пару " + paraAnswer; break;
                case "/para@Squad47bot": Answer = "На следующую пару " + paraAnswer; break;


                default: Answer = "Такой команды нет, так как Русик работает в Мейзу"; break;
            }

            /*await Dispatcher.BeginInvoke(new ThreadStart(delegate {
                LogTextBlock.Text += "\r\n" + "Bot received command: " + msg.Text;
                LogTextBlock.Focus();
                LogTextBlock.CaretIndex = LogTextBlock.Text.Length;
                LogTextBlock.ScrollToEnd();
            }));*/

            await Bot.SendTextMessageAsync(msg.Chat.Id, Answer);

            Log += $"\r\n{DateTime.Now.ToLocalTime().ToString()}:\r\nCommand received:\r\n{e.Message.Text}\r\nFrom: {e.Message.From.FirstName} {e.Message.From.LastName}\r\nAnswered with: {Answer}";

        }
    }
}
