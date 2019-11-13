using System;
using System.Windows.Input;
using TaskManager.ViewModel;

namespace TaskManager.Command
{
    public class RefreshList : ICommand
    {
        private readonly TaskManagerViewModel _taskManagerViewModel;

        public event EventHandler CanExecuteChanged;

        public RefreshList(TaskManagerViewModel taskManagerViewModel)
        {
            _taskManagerViewModel = taskManagerViewModel;
        }

        public void Execute(object parameter)
        {
            _taskManagerViewModel.DoRefresh();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
