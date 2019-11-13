using System;
using System.Windows.Input;

namespace TaskManager.Command
{
    public class UpdateSpeedChanged : ICommand
    {
        private readonly Action<RefreshRate> _updateSpeed;
        public event EventHandler CanExecuteChanged;

        public UpdateSpeedChanged(Action<RefreshRate> updateSpeed)
        {
            _updateSpeed = updateSpeed;
        }

        public void Execute(object parameter)
        {
            var speed = parameter as string;
            RefreshRate refreshRate;
            switch (speed)
            {
                case "High":
                    refreshRate = RefreshRate.High;
                    break;
                case "Normal":
                    refreshRate = RefreshRate.Normal;
                    break;
                case "Low":
                    refreshRate = RefreshRate.Low;
                    break;
                case "Paused":
                    refreshRate = RefreshRate.Paused;
                    break;
                default:
                    return;
            }

            _updateSpeed(refreshRate);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
