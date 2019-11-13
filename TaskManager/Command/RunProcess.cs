using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace TaskManager.Command
{
    public class RunProcess : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            new RunProcessWindow().Show();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
