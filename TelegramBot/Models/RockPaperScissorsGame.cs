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
    using TelegramBot.ViewModels;

    class RockPaperScissorsGame : IBotCommandHandler
    {
        private TelegramBotClient _client;
        private MainWindowViewModel model;

        //class to contain game message and info about players
        private class Game
        {
            public Message gameMessage = null;
            public User player1 = null;
            public int player1Answer;
            public User player2 = null;
            public int player2Answer;
        }

        private List<Game> currentGames;

        public RockPaperScissorsGame(MainWindowViewModel client)
        {
            model = client;
            _client = client.BotClient;
            currentGames = new List<Game>(10);
        }

        public async void HandleUpdate(Update update)
        {
            string answer;

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
                Logger.Log(update, game.gameMessage);
                //adding new game to list of current games
                currentGames.Add(game);
                return;
            }

            //if someone presses the button
            if (update.Type == UpdateType.CallbackQueryUpdate)
            {
                //check if this game exist
                if (currentGames.FindAll(
                    g =>
                    (g.gameMessage.Chat.Id == update.CallbackQuery.Message.Chat.Id
                    && g.gameMessage.MessageId == update.CallbackQuery.Message.MessageId))
                .Count != 1)
                {
                    //if it doesn't, GTFO
                    var popup = await _client.AnswerCallbackQueryAsync(update.CallbackQuery.Id, "Низзя!");
                    Logger.Log(update, answerQuery: "Низзя!");
                    return;
                }

                //if yes then obtaining its index
                int gameIndex = currentGames.FindIndex(g =>
                    (g.gameMessage.Chat.Id == update.CallbackQuery.Message.Chat.Id
                    && g.gameMessage.MessageId == update.CallbackQuery.Message.MessageId));

                Game game = currentGames[gameIndex];

                //if there's no one playing yet, then someone who pressed is the first player
                if (game.player1 == null)
                {
                    game.player1 = update.CallbackQuery.From;
                    game.player1Answer = Array.IndexOf(presentations, update.CallbackQuery.Data);

                    //edit game message
                    answer = "Камень, ножницы, бумага:\r\n" +
                        $"{game.player1.FirstName} {game.player1.LastName}\r\n" +
                        $"vs\r\n" +
                        $"Пока никого... Сыграй!";
                    var edit = await _client.EditMessageTextAsync(game.gameMessage.Chat, game.gameMessage.MessageId, answer, replyMarkup: keyboard);
                    Logger.Log(update, edit);
                }

                //else he is the second player
                else if (game.player2 == null)
                {
                    //if player1 and player2 are same user
                    if (game.player1.Id == update.CallbackQuery.From.Id)
                    {
                        var popup = await _client.AnswerCallbackQueryAsync(update.CallbackQuery.Id, "Низзя!");
                        Logger.Log(update, answerQuery: "Низзя!");
                        return;
                    }

                    game.player2 = update.CallbackQuery.From;
                    game.player2Answer = Array.IndexOf(presentations, update.CallbackQuery.Data);

                    //determining R-P-S outcome
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

                    //editing game message with results
                    answer = $"{game.player1.FirstName} {game.player1.LastName}: {presentations[game.player1Answer]}\r\n" +
                        $"vs\r\n" +
                        $"{game.player2.FirstName} {game.player2.LastName}: {presentations[game.player2Answer]}\r\n" +
                        $"Результат: {outcome}";

                    var edit = await _client.EditMessageTextAsync(game.gameMessage.Chat, game.gameMessage.MessageId, answer);
                    Logger.Log(update, edit);

                    //removing game from list of current games
                    currentGames.RemoveAt(gameIndex);
                }
            }
        }

        public bool CanHandleUpdate(Update update)
        {
            //check if it's callback from buttons on already going game
            if ((update.Type == UpdateType.CallbackQueryUpdate)
                && currentGames.FindAll(
                    g =>
                    (g.gameMessage.Chat.Id == update.CallbackQuery.Message.Chat.Id
                    && g.gameMessage.MessageId == update.CallbackQuery.Message.MessageId))
                .Count == 1)
            {
                return true;
            }

            //or if it's someone wants to start a game
            if (update.Type == UpdateType.MessageUpdate && update.Message.Type == MessageType.TextMessage)
            {
                string request = Utils.PrettifyCommand(update.Message.Text);
                return request == "/rockpaperscissors";
            }
            return false;
        }


    }
}
