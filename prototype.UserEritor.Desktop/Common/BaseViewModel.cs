using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace prototype.UserEritor.Desktop
{
    /// <summary>
    ///     Базовая модель представления
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _processFlag;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool ProcessFlag
        {
            get { return _processFlag; }
            set
            {
                if (_processFlag != value)
                {
                    _processFlag = value;
                    OnPropertyChanged();
                }
            }
        }
        public virtual ICommand CreateCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            return new RelayCommand(execute, canExecute);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
