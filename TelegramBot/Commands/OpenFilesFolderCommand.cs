using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;
    using TelegramBot.ViewModels;
    class OpenFilesFolderCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        private MainViewModel _viewModel;
        public OpenFilesFolderCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Process.Start(@"..\файлы\");
        }
    }
}
