using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace prototype.UserEritor.Desktop
{
   public class TablePagingDefinition : BaseViewModel
    {
        private int _totalRecordCount;
        private int _navsCount;
        private int _pageIndex;
        private int _pageSize;
        private int _startPageIndex;
        private int _stopPageIndex;
        private IEnumerable<int> _pageNumbers;
        private int _pageCount;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="refreshCommand"></param>
        public TablePagingDefinition(ICommand refreshCommand)
        {
            RefreshCommand = refreshCommand;
        }

        public ICommand RefreshCommand { get; }
        /// <summary>
        ///     Общее количество элементов
        /// </summary>
        public virtual int TotalRecordCount
        {
            get { return _totalRecordCount; }
            set
            {
                if (_totalRecordCount != value)
                {
                    _totalRecordCount = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        ///     Количество ссылок настраницы
        /// </summary>
        public int NavsCount
        {
            get { return _navsCount; }
            set
            {
                if (_navsCount != value)
                {
                    _navsCount = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        ///     Текущая страница
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                if (_pageIndex != value)
                {
                    _pageIndex = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedIndex));
                }
            }
        }

        /// <summary>
        ///     Количество элементов на странице
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Начальный индекс отображаемой страницы
        /// </summary>
        public int StartPageIndex
        {
            get { return _startPageIndex; }
            set
            {
                if (_startPageIndex != value)
                {
                    _startPageIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Последний индекс отображаемой страницы
        /// </summary>
        public int StopPageIndex
        {
            get { return _stopPageIndex; }
            set
            {
                if (_stopPageIndex != value)
                {
                    _stopPageIndex = value;
                    OnPropertyChanged();
                }
            }
        }


        /// <summary>
        ///    Массив для вормирования постраничной навигации
        /// </summary>
        public IEnumerable<int> PageNumbers
        {
            get { return _pageNumbers; }
            set
            {
                if (_pageNumbers != value)
                {
                    _pageNumbers = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Количество страниц
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                if (_pageCount != value)
                {
                    _pageCount = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Возвращает индекс индекатора, номер которого совпадает с текущей страницей
        /// </summary>
        public int SelectedIndex 
            => GetSelectedIndex();

        private int GetSelectedIndex()
        {
            if (PageNumbers == null)
            {
                return 0;
            }
            var numbers = 
                PageNumbers;

            var item = 
                numbers.FirstOrDefault(o => o == PageIndex);

            if (item > 0)
            {
                var index = 0;

                foreach (var number in numbers)
                {
                    if(number == item)
                    {
                        return index;
                    }
                    index = index +1;
                }
            }
            return -1;
        }
    }
}
