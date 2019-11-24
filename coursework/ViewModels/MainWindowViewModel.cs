using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using coursework.Commands;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace coursework.ViewModels
{
    class MainWindowViewModel: BaseViewModel
    {
       // public List<Message> MessageList = new List<Message>();
        public ObservableCollection<Message> messageList;
        public ObservableCollection<Message> MessageList
        {
            get
            {
                if (messageList == null)
                {
                    messageList = new ObservableCollection<Message>();
                }
                return messageList;
            }
            set
            {
                if (messageList == null)
                {
                    messageList = new ObservableCollection<Message>();
                }
                messageList.Clear();
                foreach (var item in value)
                {
                    messageList.Add(item);
                }
                this.OnPropertyChanged("MessageList");
            }
        }



       
        public string MessageHeader { get; private set; }
        public string MessageBody { get; private set; }
        public string MessagTypeTextBlock { get; private set; }
        
        public string LoadXMLText { get; private set; }
        public string GenerateReportButtonText { get; private set; }
        public string pathToXML { get; private set; }

        public ICommand LoadXMLCommand { get; private set; }
        public ICommand GenerateReportCommand { get; private set; }
        public ICommand AddNewCommand { get; private set; }
        

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

        public string _Sender;
        public string Sender
        {
            get
            {
                return _Sender;
            }
            set
            {
                if (_Sender != value)
                {
                    _Sender = value;
                    OnPropertyChanged("Sender");
                }

            }
        }

        public string _Subject;
        public string Subject
        {
            get
            {
                return _Subject;
            }
            set
            {
                if (_Subject != value)
                {
                    _Subject = value;
                    OnPropertyChanged("Subject");
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
            _Subject = "SMS";
            _Sender = "test";

            LoadXMLCommand = new RelayCommands(LoadXMLClick);
            GenerateReportCommand = new RelayCommands(GenerateReportClick);
            AddNewCommand = new RelayCommands(AddNew);
           /* PrevMessageCommand = new RelayCommands(PrevMessage);*/



        }
        private void AddNew()
        {
            Message mes = new Message(_MessageHeaderTextBox, _Sender, _Subject, _MessageBodyTextBox);
            this.MessageList.Add(mes);

            string hold = "";
            for (int i = 0; i < MessageList.Count; i++)
            {
                hold += " Message {0}" + i + "\n------------------------------------------" +
                 "\n Header : {0}" + MessageList[i].Header +
                 "\n Sender  : {0}" + MessageList[i].Sender +
                 "\n Subject ...... : {0}" + MessageList[i].Subject +
                 "\n Body ... : {0}" + MessageList[i].Body +
                 "\n";
            }
            MessageBox.Show(hold);

        }
        private void GenerateReportClick()
        {
            MessageBox.Show("Generate Report Click");
            MessageBodyTextBox = "blablatest";
            MessageHeaderTextBox = "testtest";
            MessagTypeText = "Twitter";
        }

        /*private void NextMessage()
        {
            if (whichMessageIsDisplayed < 0) whichMessageIsDisplayed = 0;
            //MessageBox.Show("Clicked next button");
            //fill the text boxes with first message 
            
            if (whichMessageIsDisplayed >= 0 && whichMessageIsDisplayed <= MessageList.Count)
            {
              
                MessageBodyTextBox = "Sender: " + MessageList[whichMessageIsDisplayed].Sender +
                                   "\nSubject: " + MessageList[whichMessageIsDisplayed].Subject +
                                   "\n" + MessageList[whichMessageIsDisplayed].Body;

                MessageHeaderTextBox = MessageList[whichMessageIsDisplayed].Header;
                MessagTypeText = "Twitter";
                MessageBox.Show("" + whichMessageIsDisplayed);
                this.whichMessageIsDisplayed++;
            }
           else
            {
                MessageBox.Show("No more messages- next");
                MessageBox.Show("" + whichMessageIsDisplayed);
                this.whichMessageIsDisplayed = MessageList.Count;
            }
        }

        private void PrevMessage()
        {
            //MessageBox.Show("Clicked prev button");
            //fill the text boxes with first message 

            if (whichMessageIsDisplayed >= 0 && whichMessageIsDisplayed <= MessageList.Count)
            {
                MessageBodyTextBox = "Sender: " + MessageList[whichMessageIsDisplayed].Sender +
                                   "\nSubject: " + MessageList[whichMessageIsDisplayed].Subject +
                                   "\n" + MessageList[whichMessageIsDisplayed].Body;

                MessageHeaderTextBox = MessageList[whichMessageIsDisplayed].Header;
                MessagTypeText = "Twitter";
                MessageBox.Show(""+whichMessageIsDisplayed);
                this.whichMessageIsDisplayed--;
            }
            else
            {
                MessageBox.Show("no more msg");
                MessageBox.Show("" + whichMessageIsDisplayed);
                this.whichMessageIsDisplayed = 0;
            }
        }*/


        
        private void LoadXMLClick()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name
            if (result == false) return;
            this.pathToXML = dlg.FileName;
            MessageBox.Show(pathToXML);
            XmlDocument doc = new XmlDocument();
            doc.Load(@pathToXML);
            XmlNodeList XmlDocNodes = doc.SelectNodes("/Messages/Message");

            foreach (XmlNode node in XmlDocNodes)
            {
                Message mes = new Message(node["Header"].InnerText,
                                          node["Sender"].InnerText,
                                          node["Subject"].InnerText,
                                          node["Body"].InnerText);

              
                this.MessageList.Add(mes);
            }
            string hold="";
            for (int i = 0; i < MessageList.Count; i++)
            {
               hold+= " Message {0}"+ i+"\n------------------------------------------"+
                "\n Header : {0}"+ MessageList[i].Header+
                "\n Sender  : {0}"+ MessageList[i].Sender+
                "\n Subject ...... : {0}"+ MessageList[i].Subject+
                "\n Body ... : {0}"+ MessageList[i].Body+
                "\n";
            }
            MessageBox.Show(hold);

            //fill the text boxes with first message 
            MessageBodyTextBox = "Sender: "+ MessageList[0].Sender + 
                                 "\nSubject: "+ MessageList[0].Subject+
                                 "\n"+MessageList[0].Body;

            MessageHeaderTextBox = MessageList[0].Header;
            MessagTypeText = "Twitter";

           
            /* string json = JsonConvert.SerializeXmlNode(doc);
             MessageBox.Show(json);
             File.WriteAllText(@"messages.json", JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented));*/

        }

    }
}
