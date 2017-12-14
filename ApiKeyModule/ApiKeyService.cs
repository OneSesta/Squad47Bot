using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Interfaces;

namespace ApiKeyModule
{
    public class ApiKeyService : IBotApiKeyService
    {
        public string GetApiKey()
        {
            string filename= @"./ApiKeys/";
#if DEBUG
            filename += "KeyDebug.txt";
#else
            filename += "KeyRelease.txt";
#endif
            if (File.Exists(filename))
                return File.ReadAllText(filename);
            else
                throw new FileNotFoundException("Api key not found");
        }
    }
}
