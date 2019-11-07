using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TaskManager.Model
{
    public class ProcessListItem
    {
        public int Id { get; }
        public string Name { get;  }
        public ImageSource IconSource { get;}

        public Process Proc { get; }

        public ProcessListItem(Process proc)
        {
            Id = proc.Id;
            Proc = proc;

            Name = proc.ProcessName;

            try
            {
                var module = proc.MainModule;
                Icon icon = Icon.ExtractAssociatedIcon(module.FileName);
                if (icon != null)
                {
                    IconSource =  Imaging.CreateBitmapSourceFromHIcon(
                            icon.Handle,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions());
                }
            }
            catch { IconSource = null; }

        }

    }
}
