using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Common
{
    public interface IMenuItemsService
    {
        ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
    }
}
