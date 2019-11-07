using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    public class TaskManagerViewModel
    {
        public ObservableCollection<ProcessListItem> processList;
        public FlowDocument processDetails;
        public TaskManagerViewModel()
        {
            processList = new ObservableCollection<ProcessListItem>(Process.GetProcesses().Select(p => new ProcessListItem(p)));
            processDetails = new FlowDocument();
        }

        public void SelectedItemChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var proc = e.AddedCells[0].Item as ProcessListItem;
            var details = new DetailedProcessInfo(proc);
            processDetails.Clear();
            processDetails.AddText("Selected name: " + details.Name);
            processDetails.AddText("Window title: " + details.WindowTitle);
            processDetails.AddText("file path: " + details.FilePath);

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
    }
}
