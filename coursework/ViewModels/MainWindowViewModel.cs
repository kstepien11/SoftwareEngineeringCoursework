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

        public ObservableCollection<Message> sirList;
        public ObservableCollection<Message> SirList
        {
            get
            {
                if (sirList == null)
                {
                    sirList = new ObservableCollection<Message>();
                }
                return sirList;
            }
            set
            {
                if (sirList == null)
                {
                    sirList = new ObservableCollection<Message>();
                }
                sirList.Clear();
                foreach (var item in value)
                {
                    sirList.Add(item);
                }
                this.OnPropertyChanged("SirList");
            }
        }


        public List<string> _incidentNature;
        public List<string> incidentNature {
            get {
                return new List<string>() { "Theft of Properties","Staff Attack", "Device Damage", "Raid", "Customer Attack", "Staff Abuse", "Bomb Threat", "Terrorism",
                "Suspicious Incident", "Sport Injury", "Personal Info Leak" };
            }
            set {
                _incidentNature = value;
                OnPropertyChanged("incidentNature");
            }
        }
        public string userChoice { get; set;}

        public string MessageHeader { get; private set; }
        public string MessageBody { get; private set; }
        public string MessagTypeTextBlock { get; private set; }

        public string LoadXMLText { get; private set; }
        public string GenerateReportButtonText { get; private set; }
        public string pathToXML { get; private set; }

        public ICommand LoadXMLCommand { get; private set; }
        public ICommand GenerateReportCommand { get; private set; }
        public ICommand AddNewCommand { get; private set; }
        public ICommand AddNewSirCommand { get; private set; }

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


        public string _cCode;
        public string cCode
        {
            get
            {
                return _cCode;
            }
            set
            {
                if (_cCode != value)
                {
                    _cCode = value;
                    OnPropertyChanged("cCode");
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

        public MessageProcessor p;

        public MainWindowViewModel()
        {

            MessageHeader = "Message Header";
            MessageBody = "Message Body";
            MessagTypeTextBlock = "Message type:";
            LoadXMLText = "Load File";
            GenerateReportButtonText = "Generate Report";

            _MessageHeaderTextBox = string.Empty;
            _MessageBodyTextBox = string.Empty;
            _Subject = string.Empty;
            _Sender = string.Empty;

            LoadXMLCommand = new RelayCommands(LoadXMLClick);
            GenerateReportCommand = new RelayCommands(GenerateReportClick);
            AddNewCommand = new RelayCommands(AddNew);
            AddNewSirCommand = new RelayCommands(AddNewSir);

            p = new MessageProcessor();
            p.loadAbbreviations();

        }

        private void AddNew()
        {
            MessageBox.Show(userChoice);
            MessageBox.Show(_cCode);

            if (_MessageHeaderTextBox == string.Empty || _MessageBodyTextBox == string.Empty || _Sender == string.Empty)
            {
                MessageBox.Show("Please fill the values for header, sender and message body");
                p.replaceAbreviations(_MessageBodyTextBox);
            }
            else if (!p.ValidateInput(_MessageHeaderTextBox, _Sender, _Subject)) { }
            else
            {
                Message mes = new Message(_MessageHeaderTextBox, _Sender, _Subject, p.replaceAbreviations(_MessageBodyTextBox));
                mes.Type = p.SetType(mes.Header); 
                MessageBox.Show(mes.Type);
                this.MessageList.Add(mes);
                MessageHeaderTextBox = string.Empty;
                MessageBodyTextBox = string.Empty;
                Subject = string.Empty;
                Sender = string.Empty;
                MessageBox.Show("Message succesfully added!");
            }
        }


        private void AddNewSir()
        {
            if (_MessageHeaderTextBox == string.Empty || _MessageBodyTextBox == string.Empty || _Sender == string.Empty)
            {
                MessageBox.Show("Please fill the values for header, sender and message body");

            }
            else if (!p.ValidateInput(_MessageHeaderTextBox, _Sender, _Subject)) { }
            else if (!p.validateSirDate(_Subject)) { }
            else if (!p.validateSirCentreCode(_cCode) || _cCode == string.Empty) {
                MessageBox.Show("Please enter a valid Centre Code numbers in syntax - XX-XXX-XX");
            }
            else if (userChoice == string.Empty)
            {
                MessageBox.Show("Please pick a nature of incident");
            }
            else
            {
                _Subject = "SIR" + _Subject;
                Message mes = new Message(_MessageHeaderTextBox, _Sender, _Subject,
                    "Centre Code: "+_cCode+"\n" +
                    "Nature of incident: "+ userChoice+ "\n"+
                    p.replaceAbreviations(_MessageBodyTextBox));
                
                mes.Type = p.SetType(mes.Header);
                this.MessageList.Add(mes);
                this.SirList.Add(mes);

                MessageHeaderTextBox = string.Empty;
                MessageBodyTextBox = string.Empty;
                Subject = string.Empty;
                Sender = string.Empty;
                MessageBox.Show("Message succesfully added!");
            }
        }


        private void GenerateReportClick()
        {
            MessageBox.Show("Generate Report Click");
            string hold = "";
            hold += "\nList of mentions: \n\n";
            for (int i = 0; i < p.listOfMentions.Count; i++)
            {                
                hold += p.listOfMentions[i]+ 
                "\n";
            }
            hold += "\nList of Hashtags: \n\n";
            for (int i = 0; i < p.listOfHashtags.Count; i++)
            {                
                hold += p.listOfHashtags[i] +
                "\n";
            }
            hold += "\nList of Emails: \n\n";
            for (int i = 0; i < p.listOfEmails.Count; i++)
            {             
                hold += p.listOfEmails[i] +
                "\n";
            }
            hold += "\nList of Significant Incidents: \n";
            for (int i = 0; i < SirList.Count; i++)
            {               
                hold += SirList[i].ToString()+ "\n\n" ;
            }


            if (hold != null)
            {
                MessageWindow window = new MessageWindow(hold.ToString());
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Topmost = true;
                window.Show();

            }


        }

        private void LoadXMLClick()
        {
            MessageProcessor processor = new MessageProcessor();
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
                                          p.replaceAbreviations(node["Body"].InnerText)
                                         );

                mes.Type = processor.SetType(mes.Header);
                this.MessageList.Add(mes);
              
            }
                                
            /* string json = JsonConvert.SerializeXmlNode(doc);
             MessageBox.Show(json);
             File.WriteAllText(@"messages.json", JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented));*/

        }

    }
}
