using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TaskManager.Command;
using TaskManager.Model;
using TaskManager.UserControls;

namespace TaskManager.ViewModel
{
    public class TaskManagerViewModel
    {
        public ObservableCollection<ProcessListItem> processList;
        public ObservableCollection<FrameworkElement> details = new ObservableCollection<FrameworkElement>();

        public ProcessListItem selectedProcess = null;

        public TaskManagerViewModel()
        {
            processList = new ObservableCollection<ProcessListItem>(Process.GetProcesses().Select(p => new ProcessListItem(p)));
            DisplayDetails(processList[0]);
        }

        public void SelectedItemChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedProcess = e.AddedCells[0].Item as ProcessListItem;

            if (selectedProcess.Proc.newHasExited())
            {
                processList.Remove(selectedProcess);
                return;
            }
            DisplayDetails(selectedProcess);
        }

        public DeleteProcess DeleteProcess { get; } = new DeleteProcess();

        void DisplayDetails(ProcessListItem process)
        {
            details.Clear();
            details.Add(new DetailedProcessInfo(process));
        }
    }
    static class FlowDocumentExt
    {
        public static void AddText(this FlowDocument textBox, string text)
        {
            textBox.Blocks.Add(new Paragraph(new Run(text)));
        }

        public static void Clear(this FlowDocument textBox)
        {
            textBox.Blocks.Clear();
        }

        public static bool newHasExited(this Process process)
        {
            try
            {
                return process.HasExited;
            }
            catch
            {
                return false;
            }
        }
    }
}
