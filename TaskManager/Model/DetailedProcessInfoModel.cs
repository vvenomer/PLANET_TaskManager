using Newtonsoft.Json;
using System.Collections;

namespace TaskManager.Model
{
    public class DetailedProcessInfoModel
    {

        public string Name;
        public string WindowTitle;
        public string FilePath;
        public string Priority;
        public int Threads;
        public string ThreadsDump;
        public int Modules;
        public string ModulesDump;
        public MemorySizeInfo Memory;
        public struct MemorySizeInfo
        {
            public long NonpagedSystemMemory;
            public long PagedMemory;
            public long PagedSystemMemory;
            public long PrivateMemory;
            public long VirtualMemory;
            public long WorkingSet;
        }


        public DetailedProcessInfoModel(ProcessListItem process)
        {
            Name = process.Name;
            WindowTitle = process.Proc.MainWindowTitle;
            Threads = process.Proc.Threads.Count;
            ThreadsDump = DumpCollection(process.Proc.Threads);
            try
            {
                var module = process.Proc.MainModule;
                FilePath = module.FileName;
            }
            catch { FilePath = ""; }
            try
            {
                Priority = process.Proc.PriorityClass.ToString();
            }
            catch { Priority = "Unknown"; }
            try
            {
                Modules = process.Proc.Modules.Count;
                ModulesDump = DumpCollection(process.Proc.Modules);
            }
            catch { Modules = 0; ModulesDump = ""; }
            Memory.NonpagedSystemMemory = process.Proc.NonpagedSystemMemorySize64;
            Memory.PagedMemory = process.Proc.PagedMemorySize64;
            Memory.PagedSystemMemory = process.Proc.PagedSystemMemorySize64;
            Memory.PrivateMemory = process.Proc.PrivateMemorySize64;
            Memory.VirtualMemory = process.Proc.VirtualMemorySize64;
            Memory.WorkingSet = process.Proc.WorkingSet64;
        }

        string DumpCollection(ICollection collection)
        {

            return JsonConvert.SerializeObject(collection, new JsonSerializerSettings()
            {
                Error = (serializer, err) => err.ErrorContext.Handled = true
            });
        }
    }
}