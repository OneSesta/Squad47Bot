using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TelegramBot.Common
{
    public class MenuItemViewModel : ObservableModelBase
    {
        /// <summary>
        /// Text of MenuItem
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Icon of MenuItem
        /// </summary>
        public Uri ImageSource { get; set; }

        /// <summary>
        /// Command to be associated with MenuItem
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// Children MenuItems
        /// </summary>
        public ObservableCollection<MenuItemViewModel> Children { get; set; }
    }
}
