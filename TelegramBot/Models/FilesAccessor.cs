namespace TelegramBot.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;

    /// <summary>
    /// This class is responsible for gaining access to documents that people want to acquire by typing commands
    /// </summary>
    class FilesAccessor : IBotCommandHandler
    {
        private TelegramBotClient _client;

        public FilesAccessor(TelegramBotClient client)
        {
            _client = client;
        }

        public void HandleUpdate(Update update)
        {
            FileStream stream =  GetFileByCommand(update.Message.Text.ToLower());
            _client.SendDocumentAsync(update.Message.Chat.Id, new FileToSend(stream.Name, stream), "Лови", false, update.Message.MessageId);
        }

        /// <summary>
        /// Gets file by requested string of text (user command)
        /// </summary>
        /// <param name="command"></param>
        /// <example>
        /// <see cref="GetFileByCommand"/> call example:
        /// <code>
        /// GetFileByCommand("лаба 1 лавренюк");
        /// </code>
        /// </example>
        /// <returns>FileStream associated with file described in the "command" paramether</returns>
        public static FileStream GetFileByCommand(string request)
        {
            string path = "файлы/";

            if (request.Contains("лавренюк"))
            {
                path += "лабы/лавренюк/";
            }
            else if (request.Contains("алгоритм"))
            {
                path += "лабы/алгоритмы/";
            }

            MatchCollection matches = Regex.Matches(request, @"\d+");
            if (matches.Count != 1)
                return null;
            string[] files = Directory.GetFiles(path, "*"+matches[0].Value+"*").Where(f => f.Contains(matches[0].Value)).ToArray();
            Console.WriteLine();
            if (files.Length != 1)
                return null;

            var file = new FileStream(files[0], FileMode.Open);

            return file;
        }

        public bool CanHandleUpdate(Update update)
        {
            if (update.Type != UpdateType.MessageUpdate)
                return false;

            try
            {
                if (GetFileByCommand(update.Message.Text.ToLower()) == null)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
