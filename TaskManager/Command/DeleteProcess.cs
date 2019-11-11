using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TaskManager.ViewModel;

namespace TaskManager.Command
{
    public class DeleteProcess : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var selectedProcess = (parameter as TaskManagerViewModel).selectedProcess;
            if (selectedProcess == null)
                return;
            selectedProcess.Proc.Kill();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
