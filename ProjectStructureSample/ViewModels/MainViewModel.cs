using ProjectStructureSample.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ProjectStructureSample.ViewModels
{
    public class MainViewModel : ViewPropertyChanged
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged("Title"); }
        }

        private object _selectedObject;

        public object SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                _selectedObject = value;
                OnPropertyChanged("SelectedObject");
                InitializeComboBox();
            }
        }


        private List<string> _comboBoxItemSource;
        public List<string> ComboBoxItemSource
        {
            get
            {
                return _comboBoxItemSource;
            }
            set
            {
                _comboBoxItemSource = value;
                OnPropertyChanged("ComboBoxItemSource");
            }
        }
        private string _selectedProperty;

        public string SelectedProperty
        {
            get { return _selectedProperty; }
            set { _selectedProperty = value; OnPropertyChanged("SelectedProperty"); }
        }

        private DataTable _dataSource;
        private ICollectionView _items;

        public ICollectionView Items
        {
            get { return _items; }
            set { _items = value; }
        }
        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                _search = value; OnPropertyChanged(nameof(Search));
                Items.Refresh();
            }
        }
        public MainViewModel()
        {
            ComboBoxItemSource = new List<string>();
            _dataSource = new DataTable();
        }
        
        private bool FilterData(object parameter)
        {
            return parameter == null ||
                   string.IsNullOrEmpty(Search) ||
                   parameter.GetType().GetProperty(SelectedProperty).
                   GetValue(parameter).ToString().Contains(Search)
                   ;
        }
        public void InitializeComboBox()
        {
            if (SelectedObject == null) return;
            ComboBoxItemSource.Clear();
            foreach (var property in SelectedObject.GetType().GetProperties())
            {
                ComboBoxItemSource.Add(property.Name);
            }
        }

    }
}
