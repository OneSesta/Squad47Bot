using TelegramBot.Common;

namespace TelegramBot.Models
{
    internal class SchedulePeriod : ObservableModelBase
    {
        private int _startHour;
        public int StartHour
        {
            get { return _startHour; }
            set { _startHour = value; OnPropertyChanged(); }
        }

        private int _startMinute;
        public int StartMinute
        {
            get { return _startMinute; }
            set { _startMinute = value; OnPropertyChanged(); }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; OnPropertyChanged(); }
        }

        private string _teacher;
        public string Teacher
        {
            get { return _teacher; }
            set { _teacher = value; OnPropertyChanged(); }
        }

        private string _cabinet;
        public string Cabinet
        {
            get { return _cabinet; }
            set { _cabinet = value; OnPropertyChanged(); }
        }
    }
}
