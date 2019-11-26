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
        public List<string> listOfMentions { get; set; }
        public List<string> listOfURL { get; set; }
        public List<string> listOfHashtags { get; set; }

        string startupPath = Environment.CurrentDirectory;

        public MessageProcessor()
        {
            this.abbreviations = new List<string>();
            this.replaceWith = new List<string>();
            this.listOfMentions = new List<string>();
            this.listOfHashtags = new List<string>();
            this.listOfURL = new List<string>();

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

            
            string patternURLReplace = "<URL Quarantined>";
            Regex regexURL = new Regex(@"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})");
            Regex mentions = new Regex(@"(\@[a-zA-Z0-9_%]*)");
            Regex hashtags = new Regex(@"([#＃]+)([0-9A-Z_]*[A-Z_]+[a-z0-9_üÀ-ÖØ-öø-ÿ]*)");
           


            foreach (Match match in mentions.Matches(hold))
            {
               // MessageBox.Show("found" + match.Value);
                this.listOfMentions.Add(match.Value);
            }
            foreach (Match match in regexURL.Matches(hold))
            {
                //MessageBox.Show("found email" + match.Value);
                this.listOfURL.Add(match.Value);
            }
            foreach (Match match in hashtags.Matches(hold))
            {
                // MessageBox.Show("found hash" + match.Value);
                this.listOfHashtags.Add(match.Value);
            }


            for (int i=0;i<this.abbreviations.Count; i++)
            {

                pattern = this.abbreviations[i].Trim();
                replace = @"<"+this.replaceWith[i]+ @">";

                if (hold.Contains(pattern))
                {
                    hold = Regex.Replace(hold, pattern, replace);
                }
            }
            hold = regexURL.Replace(hold, patternURLReplace);
            return hold;
        }

        public bool validateSirDate(string date) {
            bool check= true;
            Regex regexdate = new Regex(@"^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$");
            Match matchDate = regexdate.Match(date);
            if (!matchDate.Success)
            {
                MessageBox.Show("Please enter a date of the Significant Incident in the subject field");
                check = false;
            }
            return check;
        }


        public bool validateSirCentreCode(string cCode)
        {
            bool check = true;
            Regex regCCode = new Regex(@"^[0-9]{2}-[0-9]{3}-[0-9]{2}");
            Match match = regCCode.Match(cCode);
            if (!match.Success)
            {
                check = false;
            }
            return check;
        }

        public bool validateSirFromFile(string subject)
        {
            bool check = true;
            Regex regSirSubject = new Regex(@"^(?:SIR)[0-9]{2}\/[0-9]{2}\/[0-9]{2}");
            Match match = regSirSubject.Match(subject);
            if (!match.Success)
            {
                check = false;
            }
            return check;
        }
    }

       
}
