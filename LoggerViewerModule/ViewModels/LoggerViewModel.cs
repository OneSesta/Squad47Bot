using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TelegramBot.Common;

namespace LoggerViewerModule.ViewModels
{
    public class LoggerViewModel : ObservableModelBase, IBotModuleViewModel
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

        private IBotLogger _logger;

        public ICommand ClearLogCommand
        {
            get;
            private set;
        }

        public string Title => "Log";

        public LoggerViewModel(IBotLogger logger)
        {
            _logger = logger;
            _logger.NewLogEntry += AddLog;
            ClearLogCommand = new RelayCommand<object>(o => Log = "", o => Log != "");
        }
    }
}
