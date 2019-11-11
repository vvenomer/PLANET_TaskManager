using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TaskManager.ViewModel;
using TaskManager.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TaskManager.UserControls;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var vm = new TaskManagerViewModel();
            DataContext = vm;
            InitializeComponent();
            processListGrid.ItemsSource = vm.processList;
            processListGrid.SelectedCellsChanged += vm.SelectedItemChanged;
            processDetails.ItemsSource = vm.details;
        }
    }
}
