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

    class NumberCommands : IBotCommandHandler
    {

        private ITelegramBotClient _client;
        private Person[] _persons;



        public NumberCommands(ITelegramBotClient client)
        {
            _client = client;
        }

        public async void HandleUpdate(Update update)
        {
            string request = Utils.PrettifyCommand(update.Message.Text);
            string answer = "";
            //string[] Arr1 = new string[1];
            //string[] Arr = System.IO.File.ReadAllLines(@"C:\Users\deses\source\repos\Squad47Bot\TelegramBot\bin\Debug\Numbers.txt", System.Text.Encoding.Default);
            string Arr = System.IO.File.ReadAllText(@"C:\Users\deses\source\repos\Squad47Bot\TelegramBot\bin\Debug\Numbers.txt", System.Text.Encoding.Default);
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
            answer = Arr;
            Message message;
            if (answer != "")
            {
                message = await _client.SendTextMessageAsync(update.Message.Chat.Id, answer, ParseMode.Default, false, false, update.Message.MessageId);
                Logger.Log(update, message);
            }
        }

        public void LoadInfo()
        {
            if (System.IO.File.Exists("файлы/Info.json"))
            {
                string converted = System.IO.File.ReadAllText("файлы/Info.json");
                _persons = JsonConvert.DeserializeObject<Person[]>(converted);
            }
        }

        public bool CanHandleUpdate(Update update)
        {
            if (update.Type != UpdateType.MessageUpdate || update.Message.Type != MessageType.TextMessage)
                return false;

            string request = Utils.PrettifyCommand(update.Message.Text);

            return request == "/numbers";
        }
    }
}
