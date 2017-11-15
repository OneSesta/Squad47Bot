namespace TelegramBot.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// This class is responsible for gaining access to documents that people want to acquire by typing commands
    /// </summary>
    class FilesAccessor
    {
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
        public static FileStream GetFileByCommand(string command)
        {
            command = command.ToLower();

            string path="../файлы/лабы/";

            if (command.Contains("лавренюк"))
            {
                path += "лавренюк/";
            }
            else if (command.Contains("алгоритм"))
            {
                path += "алгоритмы/";
            }

            MatchCollection matches = Regex.Matches(command, @"\d+");
            if (matches.Count != 1)
                throw new InvalidOperationException();
            string[] files = Directory.GetFiles(path, "*"+matches[0].Value+"*").Where(f => f.Contains(matches[0].Value)).ToArray();
            Console.WriteLine();
            if (files.Length != 1)
                throw new InvalidOperationException();

            var file = new FileStream(files[0], FileMode.Open);
            return file;
        }
    }
}
