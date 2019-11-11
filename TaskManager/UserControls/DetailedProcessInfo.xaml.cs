using System.Windows.Controls;
using System.Windows.Documents;
using TaskManager.Model;

namespace TaskManager.UserControls
{
    /// <summary>
    /// Interaction logic for DetailedProcessInfo.xaml
    /// </summary>
    public partial class DetailedProcessInfo : UserControl
    {

        public DetailedProcessInfo(ProcessListItem process)
        {
            InitializeComponent();
            FillInData(new DetailedProcessInfoModel(process));
        }

        void FillInData(DetailedProcessInfoModel model)
        {
            var basicDoc = basic.Document;
            basicDoc.Clear();
            basicDoc.AddText("Selected name: " + model.Name);
            basicDoc.AddText("Window title: " + model.WindowTitle);
            basicDoc.AddText("File path: " + model.FilePath);
            basicDoc.AddText("Priority: " + model.Priority);
            basicDoc.AddText("Threads: " + model.Threads);
            basicDoc.AddText("Modules: " + model.Modules);
            basicDoc.AddText("Nonpaged system memory: " + LongByteToString(model.Memory.NonpagedSystemMemory));
            basicDoc.AddText("Paged memory: " + LongByteToString(model.Memory.PagedMemory));
            basicDoc.AddText("Paged system memory: " + LongByteToString(model.Memory.PagedSystemMemory));
            basicDoc.AddText("Private memory: " + LongByteToString(model.Memory.PrivateMemory));
            basicDoc.AddText("Virtual memory: " + LongByteToString(model.Memory.VirtualMemory));
            basicDoc.AddText("Working set: " + LongByteToString(model.Memory.WorkingSet));

            var threadsDoc = threads.Document;
            threadsDoc.Clear();
            threadsDoc.AddText(model.ThreadsDump);

            var modulesDoc = modules.Document;
            modulesDoc.Clear();
            modulesDoc.AddText(model.ModulesDump);
        }

        string LongByteToString(long bytes)
        {
            string prefix = "B";
            double result = bytes;
            int giga = 1000000000, mega = 1000000, kilo = 1000;
            string gigaPrefix = "G", megaPrefix = "M", kiloPrefix = "K";
            if (bytes >= giga)
            {
                prefix = gigaPrefix + prefix;
                result /= giga;
            }
            else if (bytes >= mega)
            {
                prefix = megaPrefix + prefix;
                result /= mega;
            }
            else if (bytes > kilo)
            {
                prefix = kiloPrefix + prefix;
                result /= kilo;
            }
            return result.ToString("0.##") + " " + prefix;
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
