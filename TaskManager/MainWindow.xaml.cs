using System.Windows;
using TaskManager.ViewModel;
using System.Diagnostics;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var vm = new TaskManagerViewModel(SelectRadio);
            DataContext = vm;
            InitializeComponent();
            processListGrid.ItemsSource = vm.ProcessList;
            processListGrid.SelectedCellsChanged += vm.SelectedItemChanged;
            processDetails.ItemsSource = vm.Details;

            vm.StartRefresh();
        }

        private void SelectRadio(ProcessPriorityClass? selectedProcessPriorityClass)
        {
            try
            {
                switch (selectedProcessPriorityClass)
                {
                    case ProcessPriorityClass.RealTime:
                        RealTime.IsChecked = true;
                        break;
                    case ProcessPriorityClass.Idle:
                        Idle.IsChecked = true;
                        break;
                    case ProcessPriorityClass.BelowNormal:
                        BelowNormal.IsChecked = true;
                        break;
                    case ProcessPriorityClass.Normal:
                        Normal.IsChecked = true;
                        break;
                    case ProcessPriorityClass.AboveNormal:
                        AboveNormal.IsChecked = true;
                        break;
                    case ProcessPriorityClass.High:
                        High.IsChecked = true;
                        break;
                    default:
                        High.IsChecked = RealTime.IsChecked = Idle.IsChecked = BelowNormal.IsChecked = Normal.IsChecked = AboveNormal.IsChecked = false;
                        break;
                }
            }
            catch
            {
            }
        }
    }
}
