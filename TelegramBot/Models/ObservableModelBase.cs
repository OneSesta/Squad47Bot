namespace TelegramBot.Models
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal abstract class ObservableModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
