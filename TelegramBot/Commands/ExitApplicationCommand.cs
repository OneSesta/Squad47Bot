namespace TelegramBot.Commands
{
    using System;
    using System.Windows.Input;
    using TelegramBot.ViewModels;

    class ExitApplicationCommand : ICommand
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
        public ExitApplicationCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_viewModel.IsActive)
                _viewModel.Deactivate();
            App.Current.Shutdown();
        }
    }
}
