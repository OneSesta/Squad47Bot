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

    /// <summary>
    /// This command is responsible for opening folder with documents.
    /// </summary>
    class SaveInfoCommand : ICommand
    {
        private MainWindowViewModel _viewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewModel">Target ViewModel</param>
        public SaveInfoCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        #region ICommand implementation
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

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //System.IO.File.WriteAllText(@"файлы/Info.json", InfoText.Text);
        }
        #endregion
    }
}
