using System;
using System.Windows.Input;

namespace prototype.UserEritor.Desktop
{
    /// <summary>
    ///     Базовая модель представления
    /// </summary>
    public class BaseViewModel : PropertyChangedNotifier
    {
        private bool _processFlag;
       
        /// <summary>
        ///     Если true, то выполняется какое то действие. Можно отобразить спиннер и тд... 
        /// </summary>
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

        /// <summary>
        ///     Создает команду. Можно испоьзовать фабрику, что бы иметь возможность изменять реализацию
        /// </summary>
        public virtual ICommand CreateCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            return new RelayCommand(execute, canExecute);
        }      
    }
}
