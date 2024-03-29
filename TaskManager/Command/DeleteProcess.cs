﻿using System;
using System.Windows.Input;
using TaskManager.ViewModel;

namespace TaskManager.Command
{
    public class DeleteProcess : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var selectedProcess = (parameter as TaskManagerViewModel)?.SelectedProcess;
            selectedProcess?.Proc.Kill();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
