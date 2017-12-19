namespace TelegramBot.Common
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Fancy implementation if INotifyPropertyChanged interface,
    /// so it's easier to use.
    /// </summary>
    public abstract class ObservableModelBase : INotifyPropertyChanged
    {
        //no null-checking in deriving classes
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Fancy method raising a PropertyChanged event without
        /// need to specify the changed property name if raised from
        /// calling property setter.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
