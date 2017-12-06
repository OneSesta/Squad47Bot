namespace TelegramBot.ViewModels
{
    using System.Windows.Input;
    using TelegramBot.Models;
    using TelegramBot.UICommands;

    class LogViewModel : ObservableModelBase
    {
        private string _log = "";
        public string Log
        {
            get
            {
                return _log;
            }
            private set
            {
                _log = value;
                OnPropertyChanged();
            }
        }
        public void ClearLog()
        {
            Log = "";
        }
        public void AddLog(string logEntry)
        {
            Log += logEntry;
        }

        public ICommand ClearLogCommand
        {
            get;
            private set;
        }

        public LogViewModel()
        {
            Logger.NewLogEntry += AddLog;
            ClearLogCommand = new RelayCommand<object>(o => Log = "", o => Log != "");
        }
    }
}
