namespace TelegramBot.Commands
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using TelegramBot.ViewModels;
    using TelegramBot.Views;

    /// <summary>
    /// This command is responsible for bot activation
    /// </summary>
    class OpenScheduleCommand : ICommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewModel">Target ViewModel</param>
        public OpenScheduleCommand()
        {
        }

        private ScheduleWindow _schedule;
        public void OpenSchedule()
        {
            if (_schedule == null || _schedule.IsClosed)
            {
                _schedule = new ScheduleWindow();
                _schedule.Show();
            }
            else
            {
                _schedule.Show();
                _schedule.WindowState = WindowState.Normal;
                _schedule.Activate();
            }
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
            OpenSchedule();
        }
        #endregion
    }
}
