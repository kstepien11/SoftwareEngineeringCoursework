using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using coursework.Commands;
using Microsoft.Win32;

namespace coursework.ViewModels
{
    class MainWindowViewModel: BaseViewModel
    {
        public string MessageHeader { get; private set; }
        public string MessageBody { get; private set; }
        public string MessagTypeTextBlock { get; private set; }
        
        public string LoadXMLText { get; private set; }
        public string GenerateReportButtonText { get; private set; }

        public ICommand LoadXMLCommand { get; private set; }
        public ICommand GenerateReportCommand { get; private set; }

        //public string MessageHeaderTextBox { get; set; }
        //public string MessageBodyTextBox { get; set ; }
        //public string MessagTypeText { get; set; }

        public string _MessageBodyTextBox;
        public string MessageBodyTextBox {
            get {
                return _MessageBodyTextBox;
            }
            set {
                if (_MessageBodyTextBox != value)
                {
                    _MessageBodyTextBox = value;
                    OnPropertyChanged("MessageBodyTextBox");
                }

            } 
        }

        public string _MessageHeaderTextBox;
        public string MessageHeaderTextBox
        {
            get
            {
                return _MessageHeaderTextBox;
            }
            set
            {
                if (_MessageHeaderTextBox != value)
                {
                    _MessageHeaderTextBox = value;
                    OnPropertyChanged("MessageHeaderTextBox");
                }

            }
        }

        public string _MessagTypeText;
        public string MessagTypeText
        {
            get
            {
                return _MessagTypeText;
            }
            set
            {
                if (_MessagTypeText != value)
                {
                    _MessagTypeText = value;
                    OnPropertyChanged("MessagTypeText");
                }

            }
        }

        public MainWindowViewModel()
        {
            MessageHeader = "Message Header";
            MessageBody = "Message Body";
            MessagTypeTextBlock = "Message type:";
            LoadXMLText = "Load File";
            GenerateReportButtonText = "Generate Report";

            _MessageHeaderTextBox = string.Empty;
            _MessageBodyTextBox = string.Empty;
            MessagTypeText = "SMS";

            LoadXMLCommand = new RelayCommands(LoadXMLClick);
            GenerateReportCommand = new RelayCommands(GenerateReportClick);

        }

        private void GenerateReportClick()
        {
            MessageBox.Show("Generate Report Click");
            MessageBodyTextBox = "blablatest";
            MessageHeaderTextBox = "testtest";
            MessagTypeText = "Twitter";
        }

        private void LoadXMLClick()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == false) return;
            MessageBox.Show(dlg.FileName);

        }
    }
}
