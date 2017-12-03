using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Models
{
    internal static class Logger
    {
        public delegate void ForEvent(string log);
        static public event ForEvent NewLogEntry = delegate { };

        static public void Log(Update incomingUpdate = null, Message answerMessage = null, string answerQuery = null)
        {
            if (incomingUpdate == null || answerMessage == null)
                return;

            string entry = $"\r\n\r\n{DateTime.Now.ToLocalTime().ToString()}:\r\n";
            if (incomingUpdate!=null)
                switch (incomingUpdate.Type)
                {
                    case UpdateType.MessageUpdate:
                        {
                            entry += $"Text message received:\r\n" +
                                $"{incomingUpdate.Message.Text}\r\n" +
                                $"From: {incomingUpdate.Message.From.FirstName} {incomingUpdate.Message.From.LastName}\r\n";
                            break;
                        }
                    case UpdateType.CallbackQueryUpdate:
                        {
                            entry += $"Button pressed:\r\n" +
                                $"{incomingUpdate.CallbackQuery.Data}\r\n" +
                                $"From: {incomingUpdate.CallbackQuery.From.FirstName} {incomingUpdate.CallbackQuery.From.LastName}\r\n";
                            break;
                        }
                }
            if (answerMessage!=null)
                switch (answerMessage.Type)
                {
                    case MessageType.TextMessage:
                        {
                            entry += $"Answered with message:\r\n" +
                                answerMessage.Text;
                            break;
                        }
                    case MessageType.DocumentMessage:
                        {
                            entry += $"Answered with document:\r\n" +
                                answerMessage.Document.FileName +
                                "And caption:\r\n" +
                                answerMessage.Caption;
                            break;
                        }
                }
            if (answerQuery != null)
            {
                entry += $"Answered with query:\r\n" +
                    answerQuery;
            }

            NewLogEntry(entry);
        }
    }
}
