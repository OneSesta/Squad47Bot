namespace TelegramBot.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    class FilesAccessor
    {
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

            FileStream file = new FileStream(files[0], FileMode.Open);
            return file;
        }
    }
}
