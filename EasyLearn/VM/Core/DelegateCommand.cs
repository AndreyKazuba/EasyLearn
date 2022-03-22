using System;
using System.Windows.Input;

namespace EasyLearn.VM.Core
{
    public class DelegateCommand : ICommand
    {
        private Action<object?> action;
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public DelegateCommand(Action<object?> action) => this.action = action;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter = null) => this.action(parameter);
    }
    public class DelegateCommand<TArgument> : ICommand
    {
        private Action<TArgument> action;
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public DelegateCommand(Action<TArgument> action) => this.action = action;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter = null)
        {
            if (parameter is not TArgument)
                throw new ArgumentException($"Аргумент {nameof(parameter)} должен быть типа {nameof(TArgument)}");
            this.action((TArgument)parameter);
        }
    }
}
