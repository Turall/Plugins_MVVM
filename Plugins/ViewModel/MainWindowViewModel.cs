using MyPlugin;
using Plugins.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Plugins.ViewModel
{
    class MainWindowViewModel : Observable
    {
        public ObservableCollection<Assembly> assemblies { get; set; }  = new ObservableCollection<Assembly>();
        public ObservableCollection<string> ListOfPlugins { get; set; } = new ObservableCollection<string>();
        public MainWindowViewModel()
        {
            var directory = new DirectoryInfo("Plugins").GetFiles();
            foreach (var item in directory)
            {
                assemblies.Add(Assembly.LoadFile(item.FullName));
                ListOfPlugins.Add(item.Name.Split('.')[0]);
            }
        }

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        private string _resultText;
        public string ResultText
        {
            get
            {
                return _resultText;
            }
            set
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _cmd;
        public RelayCommand Cmd
        {
            get
            {
                return _cmd ?? (_cmd = new RelayCommand(
                    async x =>
                   {
                       var item = assemblies[SelectedIndex].GetTypes().First();
                       if (item.GetInterface("IPlugin") != null)
                       {
                           try
                           {
                               IPlugin plugin = Activator.CreateInstance(item) as IPlugin;
                               var obj = await plugin.Do(SearchText);
                               ResultText = obj;
                           }
                           catch (Exception ex)
                           {
                               MessageBox.Show(ex.Message);
                           }
                          
                       }
                   }
              
                ));
            }
        }
    }
}
