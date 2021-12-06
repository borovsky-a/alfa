using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace prototype.UserEritor.Desktop
{
    public static class TableProperties
    {
        public static ObservableCollection<DataGridColumn> GetColumns(DependencyObject d)
        {
            return (ObservableCollection<DataGridColumn>)d.GetValue(ColumnsProperty);
        }

        public static void SetColumns(DependencyObject d, ObservableCollection<DataGridColumn> value)
        {
            d.SetValue(ColumnsProperty, value);
        }

        public static readonly DependencyProperty ColumnsProperty =
               DependencyProperty.RegisterAttached("Columns",
               typeof(ObservableCollection<TableColumnDefinition>),
               typeof(TableProperties),
               new UIPropertyMetadata(new ObservableCollection<TableColumnDefinition>(),
               OnDataGridColumnsPropertyChanged));


        private static void OnDataGridColumnsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                return;
            }
            if (d.GetType() != typeof(DataGrid))
            {
                return;
            }
            var dataGridrid = d as DataGrid;

            var definitions = (ObservableCollection<TableColumnDefinition>)e.NewValue;

            if (!definitions.Any())
            {
                return;
            }

            dataGridrid.Columns.Clear();

            foreach (var definition in definitions.OrderBy(o=> o.Order))
            {
                var dataGridColumn = CreateColumn(definition);
                dataGridrid.Columns.Add(dataGridColumn);
            }


            definitions.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems != null)
                {
                    foreach (var definition in args.NewItems.Cast<TableColumnDefinition>())
                    {
                        var dataGridColumn = CreateColumn(definition);
                        dataGridrid.Columns.Add(dataGridColumn);
                    }
                }

                if (args.OldItems != null)
                {
                    foreach (var definition in args.OldItems.Cast<TableColumnDefinition>())
                    {
                        var dataGridColumn = CreateColumn(definition);
                        dataGridrid.Columns.Remove(dataGridColumn);
                    }
                }
            };
        }

        private static DataGridColumn CreateColumn(TableColumnDefinition model)
        {
            var result = default(DataGridColumn);

            switch (model.TemplateName)
            {
                case TableColumnTemplateName.Text:
                    var textColumn = new DataGridTextColumn();
                    textColumn.Binding = new Binding(model.Binding);
                    result = textColumn;
                    break;
                default:
                    result = new DataGridTemplateColumn();
                    break;
            }          
      
            switch (model.IsVisible)
            {
                case true:
                    result.Visibility = Visibility.Visible;
                    break;
                case false:
                    result.Visibility = Visibility.Hidden;
                    break;
                case null:
                    result.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }


            result.Header = model.Header;
            result.CanUserResize = model.CanResize;
            result.CanUserSort = false;


            if (double.TryParse(model.Width, out var width))
            {
                result.Width = new DataGridLength(width);
            }
            else
            {
                result.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            return result;
        }
    }
}
