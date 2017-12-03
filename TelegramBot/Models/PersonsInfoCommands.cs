namespace TelegramBot.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using TelegramBot.ViewModels;
    using System.IO;
    using Newtonsoft.Json;
    using System.ComponentModel;
    using System.Collections.ObjectModel;

    internal class PersonsInfoCommands : IBotCommandHandler, INotifyPropertyChanged
    {

        private ITelegramBotClient _client;
        private List<Person> _persons;

        public List<Person> Persons
        {
            get
            {
                return _persons;
            }
            set
            {
                _persons = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Persons"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public PersonsInfoCommands(ITelegramBotClient client)
        {
            _client = client;
            _persons = new List<Person>();
            LoadInfo();
        }

        public async void HandleUpdate(Update update)
        {
            string request = Utils.PrettifyCommand(update.Message.Text);
            string answer = "";
            Message message;
            //string[] Arr1 = new string[1];
            //string[] Arr = System.IO.File.ReadAllLines(@"C:\Users\deses\source\repos\Squad47Bot\TelegramBot\bin\Debug\Numbers.txt", System.Text.Encoding.Default);
            //string Arr = System.IO.File.ReadAllText(@"C:\Users\deses\source\repos\Squad47Bot\TelegramBot\bin\Debug\Numbers.txt", System.Text.Encoding.Default);
            //Arr1[0] = Arr[0];
            //switch (request)
            //{
            //    case "/number Авилова":
            //        answer = $"Авилова Валерия: 	+380502312269";
            //        break;
            //}
            //
            //foreach (string name in Arr)
            //{
            //    answer = Arr[3];


            //}
            //switch (request)
            //{

            //}
            //answer = Arr;
            request = request.Remove(0, "/info ".Length);

            foreach (Person p in _persons)
            {
                if (p.FirstName.ToLower() == request
                    || p.LastName.ToLower() == request
                    || p.Patronymic.ToLower() == request)
                {
                    answer += $"{p.LastName} {p.FirstName} {p.Patronymic} {p.PhoneNumber}\r\n";
                }
            }

            if (answer == "")
            {
                answer = "Хз";
            }

            message = await _client.SendTextMessageAsync(update.Message.Chat.Id, answer, ParseMode.Default, false, false, update.Message.MessageId);
            Logger.Log(update, message);
        }

        public void LoadInfo()
        {
            if (System.IO.File.Exists("файлы/Info.json"))
            {
                string converted = System.IO.File.ReadAllText("файлы/Info.json");
                _persons = JsonConvert.DeserializeObject<List<Person>>(converted);
            }
        }

        public void SaveInfo()
        {
            System.IO.File.WriteAllText("файлы/Info.json", JsonConvert.SerializeObject(_persons));
        }

        public bool CanHandleUpdate(Update update)
        {
            if (update.Type != UpdateType.MessageUpdate || update.Message.Type != MessageType.TextMessage)
                return false;

            string request = Utils.PrettifyCommand(update.Message.Text);

            return request.StartsWith("/info ");
        }
    }
}
