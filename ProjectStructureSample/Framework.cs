using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjectStructureSample
{
    public class Framework : ViewPropertyChanged
    {
        public static Framework Instance { get; private set; } = null;
        public MainWindow MainWindowInstance { get; set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged("Title"); }
        }

        private object _items;

        public object Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged("Items"); }
        }
        private object _model;

        public object Model
        {
            get { return _model; }
            set { _model = value; OnPropertyChanged("Model"); }
        }

        private ICollectionView _itemsSource;

        public ICollectionView ItemsSource
        {
            get { return _itemsSource; }
            set { _itemsSource = value; }
        }

        private Framework() {
            Items = new ObservableCollection<object>();
        }

        public static Framework GetFramework()
        {
            if (Instance == null)
                Instance = new Framework();
            return Instance;
        }
        

        public void InitializeProjectData(string projectName)
        {
            var project = ProjectFactory.GetProjectInstance(projectName);
            var projectType = project.GetType();
            Title = projectType.GetProperty("Title").GetValue(project, null) as string;
            Items = projectType.GetMethod("GetModel").Invoke(project, null);
            Model = projectType.GetProperty("ModelObject").GetValue(project, null);
            ItemsSource = CollectionViewSource.GetDefaultView(Items);
            MainWindowInstance.textBlock.Text = Title;
            MainWindowInstance.dataGrid.ItemsSource = ItemsSource;
            var mainViewModel = MainWindowInstance.DataContext.GetType();
            mainViewModel.GetProperty("SelectedObject")
                .SetValue(MainWindowInstance.DataContext , Model);
           //mainViewModel.GetMethod("LoadData").Invoke(MainWindowInstance.DataContext, null);
        }

    }
}
