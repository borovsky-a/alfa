using System;
using System.Linq;
using System.Windows;

namespace prototype.UserEritor.Desktop.Themes
{
    public class ThemeSelector : DependencyObject
    {

        public static readonly DependencyProperty CurrentThemeDictionaryProperty = 
            DependencyProperty.RegisterAttached("CurrentThemeDictionary", typeof(Uri), typeof(ThemeSelector), new UIPropertyMetadata(null, CurrentThemeDictionaryChanged));

        public static Uri GetCurrentThemeDictionary(DependencyObject obj)
        {
            return (Uri)obj.GetValue(CurrentThemeDictionaryProperty);
        }         

        public static void SetCurrentThemeDictionary(Uri value)
        {
            SetCurrentThemeDictionary(Application.Current.MainWindow, value);
        }

        public static void SetCurrentThemeDictionary(DependencyObject obj, Uri value)
        {
            obj.SetValue(CurrentThemeDictionaryProperty, value);
        }

        private static void CurrentThemeDictionaryChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is FrameworkElement)
            {
                ApplyTheme(obj as FrameworkElement, GetCurrentThemeDictionary(obj));
            }
        }

        private static void ApplyTheme(FrameworkElement targetElement, Uri dictionaryUri)
        {
            if (targetElement == null) return;
            if (dictionaryUri == null) return;

            var existingDictionaries = targetElement.Resources.MergedDictionaries.OfType<ThemeDictionary>().ToList();

            for (int i = 0; i < existingDictionaries.Count; i++)
            {
                var existingDictionary = existingDictionaries[i];              
                targetElement.Resources.MergedDictionaries.Remove(existingDictionary);
            }

            var themeDictionary = new ThemeDictionary
            {
                Source = dictionaryUri
            };
            targetElement.Resources.MergedDictionaries.Insert(0, themeDictionary);           
        }
    }
}
