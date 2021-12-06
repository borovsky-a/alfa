
namespace prototype.UserEritor.Desktop
{
    public class TableSettings
    {
        public TableSettings()
        {
            Columns = new TableColumnsSettings[0];
        }

        public int PageSize { get; set; }

        public int NavCount { get; set; }       

        public TableColumnsSettings[] Columns { get; set; }
    }
}
