using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTxt.Sharp.UI.Library
{
    public class SimpleCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public SimpleCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public SimpleCommand(Action<T> execute)
            : this(execute, o => true)
        {
        }

        public virtual bool CanExecute(T parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;

        void ICommand.Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public void Execute(T parameter)
        {
            _execute(parameter);
        }
    }

    public class SimpleCommand : SimpleCommand<object>
    {
        public SimpleCommand(Action execute, Func<bool> canExecute)
            : base(o => execute(), o => canExecute())
        {
        }

        public SimpleCommand(Action execute)
            : base(o => execute())
        {
        }

        public bool CanExecute()
        {
            return base.CanExecute(null);
        }

        public void Execute()
        {
            Execute(null);
        }
    }
}
