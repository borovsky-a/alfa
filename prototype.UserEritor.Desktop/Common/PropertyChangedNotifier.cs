using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace prototype.UserEritor.Desktop
{
    /// <summary>
    ///  Реализация <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public abstract  class PropertyChangedNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
