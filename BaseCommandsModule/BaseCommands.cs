namespace BaseCommandsModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using TelegramBot.Common;

    public class BaseCommands : IBotUpdateHandler
    {
        private IBotUpdateDispatcher _updateDispatcher;
        private IBotLogger _logger;

        public BaseCommands(IBotUpdateDispatcher updateDispatcher, IBotLogger logger)
        {
            _updateDispatcher = updateDispatcher;
            _updateDispatcher.AddHandler(this);
            _logger = logger;
        }

        public bool CanHandleUpdate(Update update, ITelegramBotClient botClient = null)
        {
            if (update.Type != UpdateType.MessageUpdate || update.Message.Type != MessageType.TextMessage)
                return false;

            string request = Utils.PrettifyCommand(update.Message.Text);

            return request == "/start"
                || request == "/help";
        }

        public async void HandleUpdate(Update update, ITelegramBotClient botClient)
        {
            string answer = "Список доступных команд:\r\n" +
                "/flip - Подбросить монетку\r\n" +
                "/rand - Случайное число от 1 до 47\r\n" +
                "/scorebylitvinov - Твоя оценка по Литвинову\r\n" +
                "/para - Проверь, нужно ли тебе идти на следующую пару\r\n" +
                "/rockpaperscissors - Камень, ножницы, бумага\r\n" +
                "/numbers - Мобильные номера группы\r\n" +
                "/help - Список всех команд";
            var message = await botClient.SendTextMessageAsync(update.Message.Chat.Id, answer, ParseMode.Default, false, false, update.Message.MessageId);
            _logger?.LogUpdate(update, message);
        }
    }
}
