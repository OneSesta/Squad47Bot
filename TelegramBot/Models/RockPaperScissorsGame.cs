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
    using Telegram.Bot.Types.InlineKeyboardButtons;
    using Telegram.Bot.Types.ReplyMarkups;

    class RockPaperScissorsGame : IBotCommandHandler
    {
        private TelegramBotClient _client;

        //private class Game
        //{
        //    public Message gameMessage;
        //    public User player1;
        //    public User player2;
        //}

        //private List<Game> currentGames;

        public RockPaperScissorsGame(TelegramBotClient client)
        {
            _client = client;
            //currentGames = new List<Game>(10);
        }

        public void HandleUpdate(Update update)
        {

            string request = Utils.PrettifyCommand(update.Message.Text);
            string answer;

            string[] variants = new string[] { "/scissors", "/paper", "/rock" };

            //\U0000270A - Камень
            //\U0000270B - Бумага
            //\U0000270C - Ножницы
            string[] presentations = new string[] { "\U0000270C", "\U0000270B", "\U0000270A" };


            //if someone just wants to start a game
            if (request == "/rockpaperscissors")
            {
                //var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
                //{
                //    new InlineKeyboardButton[]
                //    {
                //        new InlineKeyboardCallbackButton(presentations[0], variants[0]),
                //        new InlineKeyboardCallbackButton(presentations[1], variants[1]),
                //        new InlineKeyboardCallbackButton(presentations[2], variants[2])
                //    }
                //});
                answer = "Камень, ножницы, бумага:\r\n" +
                    "/scissors - Выбрать ножницы (\U0000270C)\r\n" +
                    "/paper - Выбрать бумагу (\U0000270B)\r\n" +
                    "/rock - Выбрать камень (\U0000270A)\r\n";
                _client.SendTextMessageAsync(update.Message.Chat.Id, answer, ParseMode.Default, false, false, update.Message.MessageId/*, keyboard*/);
                return;
            }

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

            //if ((update.Type == UpdateType.CallbackQueryUpdate)
            //    && currentGames.FindAll(
            //        g => 
            //        (g.gameMessage.Chat.Id == update.Message.Chat.Id
            //        && g.gameMessage.MessageId == update.Message.MessageId))
            //    .Count == 1)
            //{
            //    return true;
            //}

            if (update.Type == UpdateType.MessageUpdate)
            {
                string request = Utils.PrettifyCommand(update.Message.Text);

                return request == "/rock"
                    || request == "/scissors"
                    || request == "/paper"
                    || request == "/rockpaperscissors";
            }
            return false;
        }


    }
}
