using EasyLearn.Infrastructure.Exceptions;
using System;
using System.Windows.Input;

namespace EasyLearn.VM.Core
{
    public class Command : ICommand
    {
        private Action action;
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public Command(Action action) => this.action = action;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter = null) => action.Invoke();
    }
    public class Command<TArgument> : ICommand
    {
        private Action<TArgument> action;
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public Command(Action<TArgument> action) => this.action = action;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter = null)
        {
            if (parameter is not TArgument)
                throw new ArgumentException(ExceptionMessagesHelper.InvalidArgumentType(nameof(parameter), nameof(TArgument)));
            action((TArgument)parameter);
        }
    }
}
