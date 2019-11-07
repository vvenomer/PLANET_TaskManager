using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TaskManager.ViewModel;
using TaskManager.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TaskManagerViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new TaskManagerViewModel();
            processListGrid.ItemsSource = vm.processList;
            processListGrid.SelectedCellsChanged += vm.SelectedItemChanged;
            processDetails.Document = vm.processDetails;
        }
    }
}
