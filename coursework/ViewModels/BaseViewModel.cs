using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace coursework.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChanged(String propertyChanged)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyChanged));
        }
    }
}
