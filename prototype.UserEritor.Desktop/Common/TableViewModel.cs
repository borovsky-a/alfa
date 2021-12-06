using prototype.UserEritor.Desktop.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace prototype.UserEritor.Desktop
{
    /// <summary>
    ///     Базовое представление для отображения списков
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TableViewModel<T> : BaseViewModel
    {
       
        private string _lastResult;
        private bool _isError;
        private T _selectedItem;

        public TableViewModel()
        {
            Table = new TableDefinition<T>();
            Paging = new TablePagingDefinition(RefreshCommand);      
        }

        /// <summary>
        ///     Описание таблицы
        /// </summary>
        public virtual TableDefinition<T> Table { get; }

        /// <summary>
        ///     Описание навигации
        /// </summary>
        public virtual TablePagingDefinition Paging { get; }

        /// <summary>
        ///     Команда обновления списка
        /// </summary>
        public abstract ICommand RefreshCommand { get; }

        /// <summary>
        ///     Команда удаления записи
        /// </summary>
        public abstract ICommand DeleteRecordCommand { get; }

        /// <summary>
        ///     Команда создания записи
        /// </summary>
        public abstract ICommand CreateRecordCommand { get; }


        /// <summary>
        ///     Сообщение о последнем действии
        /// </summary>
        public string LastResult
        {
            get { return _lastResult; }
            set
            {
                if (_lastResult != value)
                {
                    _lastResult = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Если true, то последнее действие завершилось ошибкой
        /// </summary>
        public bool IsError
        {
            get { return _isError; }
            set
            {
                if (_isError != value)
                {
                    _isError = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Элемент списка на котором стоит курсор
        /// </summary>
        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == null || !_selectedItem.Equals(value))
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }


        /// <summary>
        ///    Применение ответа от сервера
        /// </summary>
        /// <param name="response"></param>
        protected virtual void ApplyResponse(IPagingResponse<T> response)
        {
            var value = response.Value;
            if (response.IsValid)
            {
                Table.Rows = new ObservableCollection<T>(value);
            }
            LastResult = response.GetMessage();
            ProcessFlag = false;
            IsError = !response.IsValid;
            Paging.NavsCount = response.NavsCount;
            Paging.PageCount = response.PageCount;
            Paging.PageIndex = response.PageIndex;      
            Paging.PageSize = response.PageSize;
            Paging.StartPageIndex = response.StartPageIndex;
            Paging.StopPageIndex = response.StopPageIndex;
            Paging.TotalRecordCount = response.TotalRecordCount;
            Paging.PageNumbers = response.PageNumbers;
        }

        /// <summary>
        ///     Применение настроек таблицы
        /// </summary>
        /// <param name="settings"></param>
        protected virtual void ApplySettings(TableSettings settings)
        {
            Paging.PageSize = settings.PageSize;
            Paging.NavsCount = settings.NavCount;
            Table.Columns = new ObservableCollection<TableColumnDefinition>(settings.Columns.Select(o=> new TableColumnDefinition
            {
                Binding = o.Binding,
                CanResize = o.CanResize,
                Header = o.Header,
                IsVisible = o.IsVisible,
                Order = o.Order,
                TemplateName = o.TemplateName,
                Width = o.Width
            }));
        }
    }
}
