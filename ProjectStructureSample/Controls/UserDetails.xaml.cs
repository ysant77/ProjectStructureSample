using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectStructureSample.Controls
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : UserControl
    {
        private  object _item;

        public UserDetails(object item)
        {
            InitializeComponent();
            _item = item;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = MainWindowSingletion.Instance;
            mainWindow.grid1.Children.Remove(this);
            MainWindowSingletion.IsChildRemoved = true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CreateControlsUsingObjects(_item);
        }

        private void CreateControlsUsingObjects(object item)
        {
            Grid rootGrid = new Grid();
            rootGrid.Margin = new Thickness(10.0);

            rootGrid.ColumnDefinitions.Add(
               new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            rootGrid.ColumnDefinitions.Add(
                 new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            rootGrid.ColumnDefinitions.Add(
                new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            rootGrid.ColumnDefinitions.Add(
                   new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            rootGrid.ColumnDefinitions.Add(
                   new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            rootGrid.ColumnDefinitions.Add(
                   new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            rootGrid.ColumnDefinitions.Add(
                   new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            rootGrid.ColumnDefinitions.Add(
                   new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            var properties = item.GetType().GetProperties();
            rootGrid.RowDefinitions.Add(CreateRowDefinition());
            int j = 1;
            foreach(var property in properties)
            {
                Debug.WriteLine($"{property.PropertyType.Name}  {property.Name} = {property.GetValue(item)}");
                if (property.PropertyType.Name == "String" || property.PropertyType.Name == "Int32")
                {

                    rootGrid.RowDefinitions.Add(CreateRowDefinition());

                    var Label = CreateTextBlock(property.Name, j, 6);
                    rootGrid.Children.Add(Label);

                    var Textbox = CreateTextBox(j, 7, property.GetValue(item));
                    rootGrid.Children.Add(Textbox);
                    j++;
                }
                if (property.PropertyType.Name == "Boolean")
                {
                    rootGrid.RowDefinitions.Add(CreateRowDefinition());

                    var Label = CreateTextBlock(property.Name, j, 6);
                    rootGrid.Children.Add(Label);

                    var Textbox = CreateCheckBox(j, 7, property.GetValue(item));
                    rootGrid.Children.Add(Textbox);
                    j++;
                }
                if(property.PropertyType.Name == "DateTime")
                {
                    rootGrid.RowDefinitions.Add(CreateRowDefinition());

                    var Label = CreateTextBlock(property.Name, j, 6);
                    rootGrid.Children.Add(Label);

                    var CheckBox = CreateDatePicker(j, 7, property.GetValue(item));
                    rootGrid.Children.Add(CheckBox);
                    j++;
                }
            }
            //rootGrid.RowDefinitions.Add(CreateRowDefinition());
            //var Button = CreateButton("Back", j + 1, 7);
            //Button.Click += new RoutedEventHandler(Button_Click);

            //rootGrid.Children.Add(Button);
            LayoutRoot.Children.Add(rootGrid);
        }
        private Button CreateButton(string text, int row, int column)
        {
            Button tb = new Button() { Content = text, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(5, 8, 0, 5) };
            tb.Width = 90;
            tb.Height = 25;
            tb.Margin = new Thickness(5);
            Grid.SetColumn(tb, column);
            Grid.SetRow(tb, row);
            return tb;
        }



        private TextBlock CreateTextBlock(string text, int row, int column)
        {
            string[] aa = BreakUpperCB(text);
            string prop = "";
            for (int i = 0; i < aa.Length; i++)
            {
                prop = prop + " " + aa[i];
            }
            TextBlock tb = new TextBlock() { Text = prop, Margin = new Thickness(5, 8, 0, 5) };
            tb.MinWidth = 90;
            tb.FontWeight = FontWeights.Bold;
            tb.Margin = new Thickness(5);
            var bc = new BrushConverter();
            tb.Foreground = (Brush)bc.ConvertFrom("#FF2D72BC");
            Grid.SetColumn(tb, column);
            Grid.SetRow(tb, row);
            return tb;
        }

        private TextBox CreateTextBox(int row, int column, object value)
        {
            TextBox tb = new TextBox();
            tb.Margin = new Thickness(5);
            tb.Height = 22;
            tb.Width = 150;
            tb.Text = value.ToString();
            Grid.SetColumn(tb, column);
            Grid.SetRow(tb, row);

            return tb;
        }


        private CheckBox CreateCheckBox(int row, int column, object value)
        {
            CheckBox cb = new CheckBox();

            cb.Margin = new Thickness(5);
            cb.Height = 22;
            cb.MinWidth = 50;
            cb.IsChecked = (bool)value;
            Grid.SetColumn(cb, column);
            Grid.SetRow(cb, row);

            return cb;
        }

        private RowDefinition CreateRowDefinition()
        {
            RowDefinition RowDefinition = new RowDefinition();
            RowDefinition.Height = GridLength.Auto;
            return RowDefinition;
        }


        public string[] BreakUpperCB(string sInput)
        {
            StringBuilder[] sReturn = new StringBuilder[1];
            sReturn[0] = new StringBuilder(sInput.Length);
            const string CUPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int iArrayCount = 0;
            for (int iIndex = 0; iIndex < sInput.Length; iIndex++)
            {
                string sChar = sInput.Substring(iIndex, 1); // get a char
                if ((CUPPER.Contains(sChar)) && (iIndex > 0))
                {
                    iArrayCount++;
                    System.Text.StringBuilder[] sTemp = new System.Text.StringBuilder[iArrayCount + 1];
                    Array.Copy(sReturn, 0, sTemp, 0, iArrayCount);
                    sTemp[iArrayCount] = new StringBuilder(sInput.Length);
                    sReturn = sTemp;
                }
                sReturn[iArrayCount].Append(sChar);
            }
            string[] sReturnString = new string[iArrayCount + 1];
            for (int iIndex = 0; iIndex < sReturn.Length; iIndex++)
            {
                sReturnString[iIndex] = sReturn[iIndex].ToString();
            }
            return sReturnString;
        }
        private DatePicker CreateDatePicker(int row, int column, object value)
        {
            DatePicker MonthlyCalendar = new DatePicker();
            MonthlyCalendar.Width = 100;
            MonthlyCalendar.Height = 20;
            MonthlyCalendar.FirstDayOfWeek = DayOfWeek.Monday;
            MonthlyCalendar.IsTodayHighlighted = true;
            MonthlyCalendar.SelectedDate = (DateTime)value;
            Grid.SetColumn(MonthlyCalendar, column);
            Grid.SetRow(MonthlyCalendar, row);
            return MonthlyCalendar;
        }
    }
}
