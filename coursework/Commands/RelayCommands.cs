﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace coursework.Commands
{
    class RelayCommands : ICommand
    {
        private Action _action;
        public event EventHandler CanExecuteChanged= (sender,e) => { };

        public RelayCommands(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
           return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
