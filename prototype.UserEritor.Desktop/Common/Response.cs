
using System;
using System.Collections.Generic;
using System.Linq;

namespace prototype.UserEritor.Desktop
{
    /// <summary>
    ///     Ответ от сервиса с результатом работы
    /// </summary>
    public class Response : IResponse
    {
        /// <summary>
        ///     ctor
        /// </summary>
        public Response()
        {
            EventTime = DateTime.Now;
        }

        /// <summary>
        ///     Время события
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        ///     Объект для хранения доплнительной информации или состояния
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        ///     Если true, nто ошибок нет
        /// </summary>
        public bool IsValid { get; set; } = true;


        /// <summary>
        ///     Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Возвращает сообщение с временем события
        /// </summary>
        /// <returns></returns>
        public virtual string GetMessage()
        {
            var description = string.IsNullOrEmpty(Description) ? "Данные обновлены." : Description;
            return $"[{EventTime.ToString("dd.MM.yyyy HH:mm:ss")}] {description}"; 
        }
    }

    /// <summary>
    ///     Ответ от сервиса с результатом работы
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T> : Response, IResponse<T>
    {
        public T Value { get; set; }
    }

    /// <summary>
    ///     Ответ от сервиса с результатом в виде массива и объектами для постраничной навигации
    /// </summary>
    public abstract class PagingResponse : Response, IPagingResponse
    {
        private int _totalRecordsCount;
        private int _pageIndex;
        private int _navsCount;
        private int _pageSize;

        /// <summary>
        ///     ctor
        /// </summary>
        public PagingResponse()
        {

        }

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="totalRecordCount"></param>
        public PagingResponse(int totalRecordCount)
            : this(1, 10, 5, totalRecordCount)
        {

        }


        /// <summary>
        ///    Объект для создания постраничной навигации
        /// </summary>
        /// <param name="pageIndex">Номер текущей страницы</param>
        /// <param name="pageSize"></param>
        /// <param name="numberOfPagesToShow"></param>
        /// <param name="totalRecordCount"></param>
        public PagingResponse(int pageIndex, int pageSize, int numberOfPagesToShow, int totalRecordCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            NavsCount = numberOfPagesToShow;
            TotalRecordCount = totalRecordCount;
        }
        /// <summary>
        ///     Общее количество элементов
        /// </summary>
        public virtual int TotalRecordCount
        {
            get { return _totalRecordsCount < 0 ? (_totalRecordsCount = 0) : _totalRecordsCount; }
            set { _totalRecordsCount = value; }
        }
        /// <summary>
        ///     Количество ссылок настраницы
        /// </summary>
        public int NavsCount
        {
            get { return _navsCount < 0 ? (_navsCount = 0) : _navsCount; }
            set { _navsCount = value; }
        }
        /// <summary>
        ///     Текущая страница
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex < 1 ? (_pageIndex = 1) : _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        ///     Количество элементов на странице
        /// </summary>
        public int PageSize
        {
            get { return _pageSize < 1 ? (_pageSize = 10) : _pageSize; }
            set { _pageSize = value; }
        }

        /// <summary>
        ///     Начальный индекс отображаемой страницы
        /// </summary>
        public int StartPageIndex =>
            GetStartPageIndex();

        /// <summary>
        ///     Последний индекс отображаемой страницы
        /// </summary>
        public int StopPageIndex =>
            GetStopPageIndex();


        /// <summary>
        ///     Номера страниц для постраничной навигации
        /// </summary>
        public IEnumerable<int> PageNumbers
        {
            get
            {
                if (StartPageIndex < 0 || StopPageIndex < 0)
                {
                    return Enumerable.Empty<int>();
                }
                var result = Enumerable.Range(StartPageIndex, StopPageIndex - StartPageIndex + 1);
                return result;
            }
        }

        /// <summary>
        ///     Общее количество страниц
        /// </summary>
        public int PageCount
            => (TotalRecordCount < 1 || PageSize < 1) ? 0 : (int)Math.Ceiling(TotalRecordCount / (double)PageSize);



        protected virtual int GetStartPageIndex()
        {
            var half = (int)((NavsCount - 0.5) / 2);
            var start = Math.Max(1, PageIndex - half);
            if (start + NavsCount - 1 > PageCount)
            {
                start = PageCount - NavsCount + 1;
            }
            return Math.Max(1, start);
        }

        protected virtual int GetStopPageIndex()
        {
            return Math.Min(PageCount, StartPageIndex + NavsCount - 1);
        }

        public static int GetPageIndex(int recordIndex, int pageSize)
        {
            return (recordIndex - 1) / pageSize + 1;
        }

        public static int GetSkipCount(int pageIndex, int pageSize)
        {
            if (pageIndex < 2)
                return 0;
            return pageSize * (pageIndex - 1);
        }
    }
    public class PagingResponse<TItem> : PagingResponse, IPagingResponse<TItem>
    {
        public PagingResponse() :
            this(new List<TItem>(), 0, 0)
        {
        }
        public PagingResponse(IEnumerable<TItem> value, int pageIndex, int totalRecordCount)
            : this(value, pageIndex, 10, 5, totalRecordCount)
        {

        }

        public PagingResponse(IEnumerable<TItem> value, int pageIndex, int pageSize, int numberOfPagesToShow, int totalRecordCount)
            : base(pageIndex, pageSize, numberOfPagesToShow, totalRecordCount)
        {
            Value = value;
        }
        public IEnumerable<TItem> Value { get; set; }
    }
   
}
