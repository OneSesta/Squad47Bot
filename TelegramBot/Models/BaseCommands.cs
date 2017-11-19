﻿namespace TelegramBot.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;

    class BaseCommands : IBotCommandHandler
    {
        private TelegramBotClient _client;

        public BaseCommands(TelegramBotClient client)
        {
            _client = client;
        }

        public bool CanHandleUpdate(Update update)
        {
            if (update.Type != UpdateType.MessageUpdate || update.Message.Type != MessageType.TextMessage)
                return false;

            string request = Utils.PrettifyCommand(update.Message.Text);

            return request == "/start"
                || request == "/help";
        }

        public void HandleUpdate(Update update)
        {
            string answer = "Список доступных команд:\r\n" +
                "/flip - Подбросить монетку\r\n" +
                "/rand - Случайное число от 1 до 47\r\n" +
                "/scorebylitvinov - Твоя оценка по Литвинову\r\n" +
                "/para - Проверь, нужно ли тебе идти на следующую пару\r\n" +
                "/rockpaperscissors - Камень, ножницы, бумага\r\n" +
                "/help - Список всех команд";
            _client.SendTextMessageAsync(update.Message.Chat.Id, answer, ParseMode.Default, false, false, update.Message.MessageId);
        }
    }
}
