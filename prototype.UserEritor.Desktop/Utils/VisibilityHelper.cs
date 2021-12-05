using System.Windows;

namespace prototype.UserEritor.Desktop.Utils
{
    public static class VisibilityHelper
    {
        public static Visibility BooleanToVisibility(bool? value)
        {
            switch (value)
            {
                case null:
                    return Visibility.Collapsed;
                case true:
                    return Visibility.Visible;
                case false:
                    return Visibility.Hidden;
            }
            return Visibility.Collapsed;
        }
    }
}
