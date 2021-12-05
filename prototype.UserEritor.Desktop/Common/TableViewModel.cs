using prototype.UserEritor.Desktop.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace prototype.UserEritor.Desktop
{
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

        public virtual TableDefinition<T> Table { get; }

        public virtual TablePagingDefinition Paging { get; }

        public abstract ICommand RefreshCommand { get; }

        public abstract ICommand DeleteRecordCommand { get; }

        public abstract ICommand CreateRecordCommand { get; }


        
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
            Paging.PageNumbers = response.PageNumbers;
            Paging.PageSize = response.PageSize;
            Paging.StartPageIndex = response.StartPageIndex;
            Paging.StartPageIndex = response.StartPageIndex;
            Paging.TotalRecordCount = response.TotalRecordCount;
        }

        protected virtual void ApplySettings(UserTableSettings settings)
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
