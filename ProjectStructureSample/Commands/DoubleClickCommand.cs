﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectStructureSample.Commands
{
    public class DoubleClickCommand : ICommand
    {
        private readonly Action<object> _action;

        public event EventHandler CanExecuteChanged;
        public DoubleClickCommand(Action<object> action)
        {
            _action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
