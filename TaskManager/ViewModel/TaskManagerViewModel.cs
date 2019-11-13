using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly Action<ProcessPriorityClass?> _selectRadio;
        private RefreshRate _refreshRate = RefreshRate.Low;
        public ObservableCollection<ProcessListItem> ProcessList;
        public ObservableCollection<FrameworkElement> Details = new ObservableCollection<FrameworkElement>();

        public ProcessListItem SelectedProcess;


        public DeleteProcess DeleteProcess { get; }
        public PriorityChanged PriorityChanged { get; }
        public UpdateSpeedChanged UpdateSpeedChanged { get; }
        public RefreshList RefreshList { get; }
        public RunProcess RunProcess { get; }

        public TaskManagerViewModel(Action<ProcessPriorityClass?> selectRadio)
        {
            _selectRadio = selectRadio;
            ProcessList = new ObservableCollection<ProcessListItem>(Process.GetProcesses().Select(p => new ProcessListItem(p)));
            SelectedProcess = ProcessList[0];
            PriorityChanged = new PriorityChanged(this);
            RefreshList = new RefreshList(this);
            DeleteProcess = new DeleteProcess();
            RunProcess = new RunProcess();
            UpdateSpeedChanged = new UpdateSpeedChanged(async (r) =>
            {
                _refreshRate = r;
                await StartRefresh();
            });
        }

        public void SelectedItemChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count == 0) return;
            SelectedProcess = e.AddedCells[0].Item as ProcessListItem;
            try
            {
                if (SelectedProcess != null && SelectedProcess.Proc.HasExited)
                {
                    ProcessList.Remove(SelectedProcess);
                    return;
                }
            }
            catch { }
            DisplayDetails();
        }


        private bool _started = false;

        public async Task StartRefresh()
        {
            if (_started || _refreshRate == RefreshRate.Paused)
            {
                return;
            }

            _started = true;
            await Task.Run(() =>
            {
                while (_refreshRate != RefreshRate.Paused)
                {
                    DoRefresh();
                    Thread.Sleep((int)_refreshRate);
                }
            });
            _started = false;
        }

        public void DoRefresh()
        {
            //Remove Old Processes
            var processesToRemove = new List<ProcessListItem>();
            foreach (var processListItem in ProcessList)
            {
                try
                {
                    if (processListItem.Proc.HasExited)
                    {
                        processesToRemove.Add(processListItem);
                    }
                }
                catch
                {
                }
            }
            Application.Current.Dispatcher?.Invoke(() => processesToRemove.ForEach(p => ProcessList.Remove(p)));

            //Add new Processes
            var currentProcesses = Process.GetProcesses();
            var processesToAdd = new List<ProcessListItem>();
            foreach (var process in currentProcesses)
            {
                if (!ProcessList.Select(p => p.Id).Contains(process.Id))
                {
                    processesToAdd.Add(new ProcessListItem(process));
                }
            }
            Application.Current.Dispatcher?.Invoke(() => processesToAdd.ForEach(p => ProcessList.Add(p)));

            //Update Selected Process
            if (!ProcessList.Contains(SelectedProcess))
            {
                SelectedProcess = ProcessList[0];
            }
            Application.Current.Dispatcher?.Invoke(DisplayDetails);
        }

        void DisplayDetails()
        {
            try
            {
                _selectRadio(SelectedProcess.Proc.PriorityClass);
                PriorityChanged.CanChange = true;
            }
            catch
            {
                PriorityChanged.CanChange = false;
                _selectRadio(null);
            }
            Details.Clear();
            Details.Add(new DetailedProcessInfo(SelectedProcess));
        }
    }
}
