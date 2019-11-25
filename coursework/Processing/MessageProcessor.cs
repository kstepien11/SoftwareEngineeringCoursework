using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace coursework
{

    class MessageProcessor
    {
        List<string> abbreviations ;
        List<string> replaceWith ;
        string startupPath = Environment.CurrentDirectory;

        public MessageProcessor()
        {
            this.abbreviations = new List<string>();
            this.replaceWith = new List<string>();
        }

        public void loadAbbreviations()
        {
            using (var reader = new System.IO.StreamReader(@startupPath+@"\textspeakAbbreviations.csv"))
            {                               
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    this.abbreviations.Add(values[0].Trim());
                    this.replaceWith.Add(values[1].Trim());
                }

                string hold = "";
                for (int i = 0; i < abbreviations.Count; i++)
                {
                    hold += abbreviations[i] + "\n";
                }
                MessageBox.Show(hold);

            }
        }
        public string SetType(string header)
        {
            string type="";           
            if (StringInfo.GetNextTextElement(header, 0)=="T") type = "Twitter";
            if (StringInfo.GetNextTextElement(header, 0) == "S") type = "SMS";
            if (StringInfo.GetNextTextElement(header, 0) == "E") type = "Email";
            return type;
        }

        public bool ValidateInput(string header, string sender, string subject)
        {
            Regex regexEmail = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@"
                                        + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            Regex regexPhone = new Regex(@"^(?:(?:\(?(?:0(?:0|11)\)?[\s-]?\(?|\+)44\)?[\s-]?(?:\(?0\)?[\s-]?)?)|(?:\(?0))(?:(?:\d{5}\)?[\s-]?\d{4,5})|(?:\d{4}\)?[\s-]?(?:\d{5}|\d{3}[\s-]?\d{3}))|(?:\d{3}\)?[\s-]?\d{3}[\s-]?\d{3,4})|(?:\d{2}\)?[\s-]?\d{4}[\s-]?\d{4}))(?:[\s-]?(?:x|ext\.?|\#)\d{3,4})?$");
            Match matchPhone = regexPhone.Match(sender);
            Match match = regexEmail.Match(sender);

            bool check= false;

            if (StringInfo.GetNextTextElement(header, 0) == "T")
            {
                
                if (StringInfo.GetNextTextElement(sender, 0) != "@")
                {
                    MessageBox.Show("Please enter a valid Sender value- start with @");
                    check = false;
                }
                else if (header.Length != 10)
                {
                    MessageBox.Show("Please enter valid syntax for email header T- followed by 9 numeric values");
                    check = false;

                }
                else check = true; 
            }

            if (StringInfo.GetNextTextElement(header, 0) == "E")
            {
                if (!match.Success)
                {
                    MessageBox.Show("Please enter a valid Sender Email address");
                    check = false;
                }
                else if (subject == string.Empty)
                {
                    MessageBox.Show("Please enter a subject of the message");
                    check = false;

                }
                else if (header.Length!= 10)
                {
                    MessageBox.Show("Please enter valid syntax for email header E- followed by 9 numeric values");
                    check = false;

                }
                else check = true;
            }

            if (StringInfo.GetNextTextElement(header, 0) == "S")
            {
                if (!matchPhone.Success)
                {
                    MessageBox.Show("Please enter a valid Sender- phone number");
                    check = false;
                }
                else if (header.Length != 10)
                {
                    MessageBox.Show("Please enter valid syntax for email header S- followed by 9 numeric values");
                    check = false;

                }

                else check = true;
            }

            return check;
        }

        public string replaceAbreviations(string textBody)
        {
            string hold=textBody;
            string replace="";
            string pattern = "";
            MessageBox.Show(""+this.abbreviations.Count);

            for (int i=0;i<this.abbreviations.Count; i++)
            {
                pattern = this.abbreviations[i].Trim();
                replace = @"<"+this.replaceWith[i]+ @">";

                if (hold.Contains(pattern))
                {
                    hold = Regex.Replace(hold, pattern, replace);
                }
            }
            MessageBox.Show(hold);
            return hold;
        }
    }

       
}
