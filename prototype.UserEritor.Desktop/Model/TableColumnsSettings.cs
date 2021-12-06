
namespace prototype.UserEritor.Desktop
{
    public class TableColumnsSettings
    {
        public bool? IsVisible { get; set; } = true;

        public int Order { get; set; }

        public string TemplateName { get; set; }

        public string Header { get; set; }

        public string Binding { get; set; }

        public string Width { get; set; } = "*";

        public bool CanResize { get; set; } = true;
    }
}
