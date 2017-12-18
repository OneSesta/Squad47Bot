using Newtonsoft.Json;
using PersonsInfoModule.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Common;

namespace PersonsInfoModule
{
    public class PersonsInfo : ObservableModelBase, IBotUpdateHandler
    {
        private List<Person> _persons;
        private IBotUpdateDispatcher _dispatcher;
        private IBotLogger _logger;
        string file = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Locati‌​on)).Parent.EnumerateDirectories()
                .Single(d => d.Name.Contains("файлы"))
                .EnumerateFiles()
                .Single(f => f.Name == "Info.json")
                .FullName;

        public List<Person> Persons
        {
            get
            {
                return _persons;
            }
            set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        public PersonsInfo(IBotUpdateDispatcher dispatcher, IBotLogger logger)
        {
            _dispatcher = dispatcher;
            _logger = logger;
            dispatcher.AddHandler(this);
            _persons = new List<Person>();
        }

        public async void HandleUpdate(Update update, ITelegramBotClient client)
        {
            string request = Utils.PrettifyCommand(update.Message.Text);
            string answer = "";
            Message message;

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

            message = await client.SendTextMessageAsync(update.Message.Chat.Id, answer, ParseMode.Default, false, false, update.Message.MessageId);
            _logger?.LogUpdate(update, message);
        }

        public void LoadInfo()
        {
            string converted = System.IO.File.ReadAllText(file);
            Persons = JsonConvert.DeserializeObject<List<Person>>(converted);
        }

        public void SaveInfo()
        {
            System.IO.File.WriteAllText(file, JsonConvert.SerializeObject(_persons));
        }

        public bool CanHandleUpdate(Update update, ITelegramBotClient client = null)
        {
            if (update.Type != UpdateType.MessageUpdate || update.Message.Type != MessageType.TextMessage)
                return false;

            string request = Utils.PrettifyCommand(update.Message.Text);

            return request.StartsWith("/info ");
        }
        //            numberCommandsHandler.PropertyChanged += (o, e) =>
        //                {
        //                    if (e.PropertyName == "Persons")
        //                    {
        //                        Info = DecodeInfo((o as PersonsInfoCommands).Persons);
        //                    }
        //                };
        //            Info = DecodeInfo(numberCommandsHandler.Persons);
        //_dispatcher.AddHandler(numberCommandsHandler);
    }
}
