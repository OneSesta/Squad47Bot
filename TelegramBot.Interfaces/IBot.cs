using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Common
{
    public interface IBot
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
}
