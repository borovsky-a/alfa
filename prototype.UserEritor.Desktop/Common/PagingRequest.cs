namespace prototype.UserEritor.Desktop
{
    /// <summary>
    ///     Запрос для получения ответа с постраничной навигацией
    /// </summary>
    public class PagingRequest
    {
        /// <summary>
        ///     ctor
        /// </summary>
        public PagingRequest()
        {
            NavsCount = 5;
            PageIndex = 1;
            PageSize = 10;
        }

        /// <summary>
        ///     Макс. количество отображаемых ссылок на страницы
        /// </summary>
        public int NavsCount { get; set; }

        /// <summary>
        ///     Номер отображаемой страницы
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     Количество элементов для отображения на странице
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Количество элементов, которое будет пропущено
        /// </summary>
        public int Offset => PageIndex < 1 ? 0 : (PageIndex - 1) * PageSize;
       
    }
}
