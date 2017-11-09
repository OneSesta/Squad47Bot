using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;


namespace TelegramBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Telegram.Bot.TelegramBotClient BOT;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BOT = new Telegram.Bot.TelegramBotClient("488598835:AAFJAn6w1rifdR6z-8wDsaSxvwXXDVusSgU");
            BOT.OnMessage += BotOnMessageReceived;
            BOT.StartReceiving(new UpdateType[] { UpdateType.MessageUpdate });
            button1.IsEnabled = false;
        }
        static Random Rnd = new Random();
        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            Telegram.Bot.Types.Message msg = messageEventArgs.Message;
            if (msg == null || msg.Type != MessageType.TextMessage) return;

            String Answer = "";
            String flipAnswer = "";
            String paraAnswer = "";

            int Random = Rnd.Next(0, 2);
            int Rand = Rnd.Next(0, 48);
            if (Random == 0)
            {
                flipAnswer = "Орел";
            }
            else
            {
                flipAnswer = "Решка";
            }

            if (Random == 0)
            {
                paraAnswer = "лучше сходить";
            }
            else
            {
                paraAnswer = "можно не идти";
            }


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
            await BOT.SendTextMessageAsync(msg.Chat.Id, Answer);

        }
    }
}
