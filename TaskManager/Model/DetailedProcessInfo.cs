using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Model
{
    class DetailedProcessInfo
    {
        public string Name;
        public string WindowTitle;
        public string FilePath;

        public DetailedProcessInfo(ProcessListItem process)
        {
            Name = process.Name;
            WindowTitle = process.Proc.MainWindowTitle;
            try
            {
                var module = process.Proc.MainModule;
                FilePath = module.FileName;
            }
            catch { FilePath = ""; }
        }
    }
}
