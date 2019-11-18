using System;
using System.Collections.Generic;
using System.Text;

namespace coursework.ViewModels
{
    class MainWindowViewModel: BaseViewModel
    {
        public string NameTextBlock { get; private set; }
        public string AgeTextBlock { get; private set; }
        public string AddressTextBlock { get; private set; }
        
        public string ClearButtonText { get; private set; }
        public string SaveButtonText { get; private set; }

        public string NameTextBox { get; set; }
        public string AgeTextBox { get; set; }
        public string AddressTextBox { get; set; }



        public MainWindowViewModel()
        {
            NameTextBlock = "Name";
            AgeTextBlock = "Age";
            AddressTextBlock = "Address";
            ClearButtonText = "Clear";
            SaveButtonText = "Save";


            NameTextBox = string.Empty;
            AgeTextBox = string.Empty;
            AddressTextBox = string.Empty;
        }
    }
}
