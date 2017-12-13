using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TelegramBot.Interfaces;

namespace LoggerModule.ViewModels
{
    public class LoggerViewModel : ObservableModelBase
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

        public LoggerViewModel()
        {
            //Logger.NewLogEntry += AddLog;
            //ClearLogCommand = new RelayCommand<object>(o => Log = "", o => Log != "");
        }
    }
}
