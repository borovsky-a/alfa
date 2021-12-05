
namespace prototype.UserEritor.Desktop.Data
{
    public class UserTableSettings
    {
        public UserTableSettings()
        {
            Columns = new UserTableColumnsSettings[0];
        }

        public int PageSize { get; set; }

        public int NavCount { get; set; }       

        public UserTableColumnsSettings[] Columns { get; set; }
    }
}
