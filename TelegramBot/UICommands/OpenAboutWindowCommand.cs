namespace TelegramBot.Commands
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using TelegramBot.ViewModels;
    using TelegramBot.Views;

    /// <summary>
    /// This command is responsible for opening folder with documents.
    /// </summary>
    class OpenAboutWindowCommand : ICommand
    {

        private AboutWindow _schedule;
        public void OpenAbout()
        {
            if (_schedule == null || _schedule.IsClosed)
            {
                _schedule = new AboutWindow();
                _schedule.Show();
            }
            else
            {
                _schedule.Show();
                _schedule.WindowState = WindowState.Normal;
                _schedule.Activate();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public OpenAboutWindowCommand()
        {
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
            OpenAbout();
        }
        #endregion
    }
}
