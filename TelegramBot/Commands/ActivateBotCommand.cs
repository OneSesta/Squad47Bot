namespace TelegramBot.Commands
{
    using System;
    using System.Windows.Input;
    using TelegramBot.ViewModels;
    class ActivateBotCommand : ICommand
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

        private BotViewModel _viewModel;
        public ActivateBotCommand(BotViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return !_viewModel.IsActive;
        }

        public void Execute(object parameter)
        {
            _viewModel.Activate();
        }
    }
}
