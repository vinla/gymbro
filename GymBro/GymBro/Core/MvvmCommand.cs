using System;
using System.Windows.Input;

namespace GymBro.Core
{
    public class MvvmCommand : ICommand
    {
        private readonly Action<Object> _action;

        public event EventHandler CanExecuteChanged;

        public MvvmCommand(Action<Object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
