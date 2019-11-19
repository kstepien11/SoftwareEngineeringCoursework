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

        protected void OnPropertyChanged(params string[] propertyNames)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                foreach (string propertyName in propertyNames) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                handler(this, new PropertyChangedEventArgs("HasError"));
            }
        }
    }
}
