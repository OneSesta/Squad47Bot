namespace TelegramBot.Commands
{
    using System;
    using System.Windows.Input;
    using TelegramBot.ViewModels;

    /// <summary>
    /// This command is responsible for bot activation
    /// </summary>
    class ActivateBotCommand : ICommand
    {
        private MainWindowViewModel _viewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewModel">Target ViewModel</param>
        public ActivateBotCommand(MainWindowViewModel viewModel)
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
            return !_viewModel.IsActive;
        }

        public void Execute(object parameter)
        {
            _viewModel.Activate();
        }
        #endregion
    }
}
