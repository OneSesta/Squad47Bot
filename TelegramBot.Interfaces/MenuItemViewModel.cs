using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TelegramBot.Common
{
    /// <summary>
    /// ViewModel associated with menu item in modular menu
    /// </summary>
    public class MenuItemViewModel : ObservableModelBase
    {
        #region Properties
        /// <summary>
        /// Text of MenuItem
        /// </summary>
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Icon of MenuItem
        /// </summary>
        private Uri _imageSource;
        public Uri ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command to be associated with MenuItem
        /// </summary>
        private ICommand _command;
        public ICommand Command
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Children MenuItems
        /// </summary>
        private ObservableCollection<MenuItemViewModel> _children;
        public ObservableCollection<MenuItemViewModel> Children
        {
            get
            {
                return _children;
            }
            private set
            {
                _children = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public MenuItemViewModel()
        {
            Children = new ObservableCollection<MenuItemViewModel>();
        }

        /// <summary>
        /// Searching item in its children by given predivate.
        /// </summary>
        /// <param name="condition">Item should satisfy it</param>
        /// <param name="searchInSubmenusAlso">Should the method search in submenus also?</param>
        /// <returns>Menu item if found, null if not found</returns>
        public MenuItemViewModel GetItem(Predicate<MenuItemViewModel> predicate, bool searchInSubmenusAlso = false)
        {
            MenuItemViewModel found = null;
            foreach (MenuItemViewModel model in Children)
            {
                if (predicate(model)){
                    found = model;
                }
            }
            if (searchInSubmenusAlso)
            {
                foreach (MenuItemViewModel model in Children)
                {
                    GetItem(predicate, searchInSubmenusAlso);
                }
            }
            return found;
        }
    }
}
