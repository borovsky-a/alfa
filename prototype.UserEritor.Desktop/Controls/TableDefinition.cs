
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace prototype.UserEritor.Desktop
{
    public class TableDefinition<T> : BaseViewModel
    {
        private ObservableCollection<T> _rows;
        public ObservableCollection<TableColumnDefinition> _columns;

        public TableDefinition()
        {
            Rows = new ObservableCollection<T>();
            Columns = new ObservableCollection<TableColumnDefinition>();
        }
        public ObservableCollection<T> Rows
        {
            get { return _rows; }
            set
            {
                if(_rows != value)
                {
                    _rows = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<TableColumnDefinition> Columns
        {
            get { return _columns; }
            set
            {
                if (_columns != value)
                {
                    _columns = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public bool IsReadOnly { get; set; } = true;

     
    }
}
