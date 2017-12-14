namespace TelegramBot.Common
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ObservableModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
