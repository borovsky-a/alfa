using System;
using System.Linq;
using System.Windows;

namespace prototype.UserEritor.Desktop.Themes
{
    public class ThemeSelector
    {

        public static readonly DependencyProperty ThemeProperty = 
            DependencyProperty.RegisterAttached("Theme", typeof(Uri), typeof(ThemeSelector), new UIPropertyMetadata(null, CurrentThemeDictionaryChanged));

        public static Uri GetCurrentThemeDictionary(DependencyObject obj)
        {
            return (Uri)obj.GetValue(ThemeProperty);
        }         

        public static void SetCurrentThemeDictionary(Uri value)
        {
            SetCurrentThemeDictionary(Application.Current.MainWindow, value);
        }

        public static void SetCurrentThemeDictionary(DependencyObject obj, Uri value)
        {
            obj.SetValue(ThemeProperty, value);
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

            var existingDictionaries = targetElement.Resources.MergedDictionaries.OfType<ThemeResourceDictionary>().ToList();

            for (int i = 0; i < existingDictionaries.Count; i++)
            {         
                targetElement.Resources.MergedDictionaries.Remove(existingDictionaries[i]);
            }

            var themeDictionary = new ThemeResourceDictionary
            {
                Source = dictionaryUri
            };
            targetElement.Resources.MergedDictionaries.Insert(0, themeDictionary);           
        }
    }
}
