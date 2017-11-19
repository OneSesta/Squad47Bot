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

        private class Game
        {
            public Message gameMessage = null;
            public User player1 = null;
            public int player1Answer;
            public User player2 = null;
            public int player2Answer;
        }

        private List<Game> currentGames;

        public RockPaperScissorsGame(TelegramBotClient client)
        {
            _client = client;
            currentGames = new List<Game>(10);
        }

        public async void HandleUpdate(Update update)
        {
            string answer;

            //string[] variants = new string[] { "/scissors", "/paper", "/rock" };

            //\U0000270A - Камень
            //\U0000270B - Бумага
            //\U0000270C - Ножницы
            string[] presentations = new string[] { "\U0000270A", "\U0000270B", "\U0000270C" };
            var keyboard = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    new InlineKeyboardCallbackButton(presentations[0], presentations[0]),
                    new InlineKeyboardCallbackButton(presentations[1], presentations[1]),
                    new InlineKeyboardCallbackButton(presentations[2], presentations[2])
                }
            });

            //if someone just wants to start a game
            if (update.Type == UpdateType.MessageUpdate
               && Utils.PrettifyCommand(update.Message.Text) == "/rockpaperscissors")
            {
                answer = "Камень, ножницы, бумага:";
                Game game = new Game
                {
                    gameMessage = await _client.SendTextMessageAsync(update.Message.Chat.Id, answer, replyToMessageId: update.Message.MessageId, replyMarkup: keyboard)
                };
                currentGames.Add(game);
                return;
            }

            if (update.Type == UpdateType.CallbackQueryUpdate)
            {
                if (currentGames.FindAll(
                    g =>
                    (g.gameMessage.Chat.Id == update.CallbackQuery.Message.Chat.Id
                    && g.gameMessage.MessageId == update.CallbackQuery.Message.MessageId))
                .Count != 1)
                {
                    var popup = _client.AnswerCallbackQueryAsync(update.CallbackQuery.Id, "Низзя!");
                    return;
                }

                int gameIndex = currentGames.FindIndex(g =>
                    (g.gameMessage.Chat.Id == update.CallbackQuery.Message.Chat.Id
                    && g.gameMessage.MessageId == update.CallbackQuery.Message.MessageId));

                Game game = currentGames[gameIndex];

                if (game.player1 == null)
                {
                    game.player1 = update.CallbackQuery.From;
                    game.player1Answer = Array.IndexOf(presentations, update.CallbackQuery.Data);

                    answer = "Камень, ножницы, бумага:\r\n" +
                        $"{game.player1.FirstName} {game.player1.LastName}\r\n" +
                        $"vs\r\n" +
                        $"Пока никого... Сыграй!";
                    var edit = _client.EditMessageTextAsync(game.gameMessage.Chat, game.gameMessage.MessageId, answer, replyMarkup: keyboard);
                }
                else if (game.player2 == null)
                {
                    if (game.player1.Id == update.CallbackQuery.From.Id)
                    {
                        var popup = _client.AnswerCallbackQueryAsync(update.CallbackQuery.Id, "Низзя!");
                        return;
                    }

                    game.player2 = update.CallbackQuery.From;
                    game.player2Answer = Array.IndexOf(presentations, update.CallbackQuery.Data);

                    string outcome;
                    if (game.player1Answer == game.player2Answer)
                    {
                        outcome = "Ганьба, это ничья.";
                    }
                    else if ((game.player1Answer == game.player2Answer + 1) || (game.player1Answer == 0 && game.player2Answer == 2))
                    {
                        outcome = $"{game.player1.FirstName} {game.player1.LastName}, це перемога!";
                    }
                    else
                    {
                        outcome = $"{game.player2.FirstName} {game.player2.LastName}, це перемога!";
                    }

                    answer = $"{game.player1.FirstName} {game.player1.LastName}: {presentations[game.player1Answer]}\r\n" +
                        $"vs\r\n" +
                        $"{game.player2.FirstName} {game.player2.LastName}: {presentations[game.player2Answer]}\r\n" +
                        $"Результат: {outcome}";

                    var edit = _client.EditMessageTextAsync(game.gameMessage.Chat, game.gameMessage.MessageId, answer);
                }
            }
        }

        public bool CanHandleUpdate(Update update)
        {
            //var type = update.Type == UpdateType.CallbackQueryUpdate;
            //var games = currentGames.FindAll(
            //        g =>
            //        (g.gameMessage.Chat.Id == update.CallbackQuery.Message.Chat.Id
            //        && g.gameMessage.MessageId == update.CallbackQuery.Message.MessageId));

            if ((update.Type == UpdateType.CallbackQueryUpdate)
                && currentGames.FindAll(
                    g =>
                    (g.gameMessage.Chat.Id == update.CallbackQuery.Message.Chat.Id
                    && g.gameMessage.MessageId == update.CallbackQuery.Message.MessageId))
                .Count == 1)
            {
                return true;
            }

            if (update.Type == UpdateType.MessageUpdate)
            {
                string request = Utils.PrettifyCommand(update.Message.Text);

                return request == "/rockpaperscissors";
            }
            return false;
        }


    }
}
