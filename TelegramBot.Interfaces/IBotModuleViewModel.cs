using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Common
{
    public interface IBotModuleViewModel
    {
        /// <summary>
        /// Title of module tab in main window
        /// </summary>
        string Title { get; }
    }
}
