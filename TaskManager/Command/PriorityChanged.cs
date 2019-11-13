using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TaskManager.ViewModel;

namespace TaskManager.Command
{
    public class PriorityChanged : ICommand
    {
        private readonly TaskManagerViewModel _taskManagerViewModel;
        public event EventHandler CanExecuteChanged;

        private bool _canChange;
        public bool CanChange
        {
            get => _canChange;
            set
            {
                _canChange = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public PriorityChanged(TaskManagerViewModel taskManagerViewModel)
        {
            _taskManagerViewModel = taskManagerViewModel;
            CanChange = true;
        }

        public void Execute(object parameter)
        {
            string priority = parameter as string;
            var selectedProcess = _taskManagerViewModel.SelectedProcess;
            ProcessPriorityClass selectedPriorityClass;
            switch (priority)
            {
                case "Real Time":
                    selectedPriorityClass = ProcessPriorityClass.RealTime;
                    break;
                case "Idle":
                    selectedPriorityClass = ProcessPriorityClass.Idle;
                    break;
                case "Below Normal":
                    selectedPriorityClass = ProcessPriorityClass.BelowNormal;
                    break;
                case "Normal":
                    selectedPriorityClass = ProcessPriorityClass.Normal;
                    break;
                case "Above Normal":
                    selectedPriorityClass = ProcessPriorityClass.AboveNormal;
                    break;
                case "High":
                    selectedPriorityClass = ProcessPriorityClass.High;
                    break;
                default:
                    return;
            }

            try
            {
                selectedProcess.Proc.PriorityClass = selectedPriorityClass;
            }
            catch (Win32Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanChange;
        }
    }
}
