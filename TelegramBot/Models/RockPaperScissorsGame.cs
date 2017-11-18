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

    class RockPaperScissorsGame : IBotCommandHandler
    {
        private TelegramBotClient _client;

        public RockPaperScissorsGame(TelegramBotClient client)
        {
            _client = client;
        }

        public void HandleUpdate(Update update)
        {

            string request = Utils.PrettifyCommand(update.Message.Text);
            string answer;

            //if someone just wants to start a game
            if (request == "/rockpaperscissors")
            {
                answer = "Камень, ножницы, бумага:\r\n" +
                    "/scissors - Выбрать ножницы (\U0000270C)\r\n" +
                    "/paper - Выбрать бумагу (\U0000270B)\r\n" +
                    "/rock - Выбрать камень (\U0000270A)\r\n";
                _client.SendTextMessageAsync(update.Message.Chat.Id, answer, ParseMode.Default, false, false, update.Message.MessageId);
                return;
            }

            string[] variants = new string[] { "/scissors", "/paper", "/rock" };

            //\U0000270A - Камень
            //\U0000270B - Бумага
            //\U0000270C - Ножницы
            string[] presentations = new string[] { "\U0000270C", "\U0000270B", "\U0000270A" };

            int playerAnswer = Array.IndexOf(variants, request);
            int botAnswer = (new Random()).Next(3);

            string outcome;
            if (playerAnswer == botAnswer)
            {
                outcome = "Ганьба.";
            }
            else if ((playerAnswer == botAnswer + 1) || (playerAnswer == 0 && botAnswer == 2))
            {
                outcome = "Поразка";
            }
            else
            {
                outcome = "Перемога!";
            }

            answer = $"{presentations[playerAnswer]} vs {presentations[botAnswer]}\r\n{outcome}";

            _client.SendTextMessageAsync(update.Message.Chat.Id, answer, ParseMode.Default, false, false, update.Message.MessageId);
        }

        public bool CanHandleUpdate(Update update)
        {
            if (update.Type != UpdateType.MessageUpdate)
                return false;

            string request = Utils.PrettifyCommand(update.Message.Text);

            return request == "/rock"
                || request == "/scissors"
                || request == "/paper"
                || request == "/rockpaperscissors";
        }
        

    }
}
