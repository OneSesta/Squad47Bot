namespace FilesAccessorModule
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Telegram.Bot;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using TelegramBot.Common;

    /// <summary>
    /// This class is responsible for gaining access to documents that people want to acquire by typing commands
    /// </summary>
    public class FilesAccessor : IBotUpdateHandler
    {
        private IBotUpdateDispatcher _updateDispatcher;
        private IBotLogger _logger;

        public FilesAccessor(IBotUpdateDispatcher updateDispatcher, IBotLogger logger)
        {
            _updateDispatcher = updateDispatcher;
            _updateDispatcher.AddHandler(this);
            _logger = logger;
        }

        public async void HandleUpdate(Update update, ITelegramBotClient client)
        {
            using (FileStream stream = GetFileByCommand(update.Message.Text.ToLower()))
            {
                var message = await client.SendDocumentAsync(update.Message.Chat.Id, new FileToSend(stream.Name, stream), caption: "Лови", replyToMessageId: update.Message.MessageId);
                stream.Close();
                _logger?.LogUpdate(update, message);
            }
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
            DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Locati‌​on))
                .Parent
                .EnumerateDirectories()
                .Single(d => d.Name.Contains("файлы"));

            if (!request.Contains("лаб"))
                return null;
            else
                directory = directory.EnumerateDirectories()
                    .Single(d => d.Name.Contains("лабы"));

            if (request.Contains("лавренюк"))
            {
                directory = directory.EnumerateDirectories()
                    .Single(d => d.Name.Contains("лавренюк"));
            }
            else if (request.Contains("алгоритм"))
            {
                directory = directory.EnumerateDirectories()
                    .Single(d => d.Name.Contains("алгоритм"));
            }

            MatchCollection matches = Regex.Matches(request, @"\d+");
            if (matches.Count != 1)
                return null;
            try
            {
                FileInfo file = directory.GetFiles("*" + matches[0].Value + "*").Single(f => f.Name.Contains(matches[0].Value));
                return file.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch
            {
                return null;
            }
        }

        public bool CanHandleUpdate(Update update, ITelegramBotClient client)
        {
            if (update.Type != UpdateType.MessageUpdate || update.Message.Type != MessageType.TextMessage)
                return false;

            using (FileStream file = GetFileByCommand(update.Message.Text))
            {
                return file != null;
            }
        }
    }
}
