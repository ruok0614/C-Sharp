using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TickTacToe.ViewModels
{
    public class DelegateCommandT<T> : ICommand
    {

        private readonly Action<T> _execute;
        private readonly Func<T,bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommandT(Action<T> execute) : this(execute, (T) => true) { }

        public DelegateCommandT(Action<T> execute, Func<T,bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            this._execute((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
           
            return this._canExecute((T)parameter);
        }

    }
}
