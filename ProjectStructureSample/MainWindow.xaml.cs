using ProjectStructureSample.Controls;
using ProjectStructureSample.ViewModels;
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

namespace ProjectStructureSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowSingletion.Instance = this;
            DataContext = new MainViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var projectName = (sender as Button).Content.ToString();
            var framework = Framework.GetFramework();
            framework.MainWindowInstance = MainWindowSingletion.Instance;
            framework.InitializeProjectData(projectName);
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dGrid = sender as DataGrid;
            if(dGrid != null && dGrid.SelectedItems != null && dGrid.SelectedItems.Count == 1)
            {
                var row = dataGrid.ItemContainerGenerator.ContainerFromItem(dataGrid.SelectedItem) as DataGridRow;
                var item = row.Item;
                //dataGrid.Items.Clear();
                var userDetailControl = new UserDetails(item);
                grid1.Children.Clear();
                grid1.Children.Add(userDetailControl);
                if (MainWindowSingletion.IsChildRemoved)
                {
                    grid1.Children.Remove(userDetailControl);
                    grid1.Children.Add(dock1);
                }
            }
        }
    }
}
