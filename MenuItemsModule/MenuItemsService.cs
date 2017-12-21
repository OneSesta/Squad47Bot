using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Common;

namespace MenuItemsModule
{
    public class MenuItemsService : ObservableModelBase, IMenuItemsService
    {
        private ObservableCollection<MenuItemViewModel> _menuItems;
        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }
    }
}
