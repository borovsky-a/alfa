
namespace prototype.UserEritor.Desktop
{
    public  class TableColumnDefinition : BaseViewModel
    {
        private bool? _isVisible;

        public bool? IsVisible
        {
            get { return _isVisible; }
            set
            {
                if(_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }


        public int Order { get; set; }

        public string TemplateName { get; set; }

        public string Header { get; set; }

        public string Binding { get; set; }

        public string Width { get; set; } = "*";

        public bool CanResize { get; set; } = true;
    }
}
