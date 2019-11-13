using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for RunProcessWindow.xaml
    /// </summary>
    public partial class RunProcessWindow : Window
    {
        public string ProcessName { get; set; }
        public int Timeout { get; set; }

        public RunProcessWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void RunProcess(object sender, RoutedEventArgs e)
        {
            var process = Process.Start(new ProcessStartInfo(ProcessName));
            int percentMax = 100;
            Task.Run(() =>
            {
                for (double i = 0; i < percentMax; i++)
                {
                    Application.Current.Dispatcher?.Invoke(() => ProgressBar.Value = i);
                    Thread.Sleep(Timeout/ percentMax);
                }
                process?.Kill();
            });
        }
    }
}
