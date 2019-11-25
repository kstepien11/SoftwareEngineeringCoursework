using System;
using System.Collections.Generic;
using System.Text;

namespace coursework
{
    class Message
    {
        public string Header { get;  set; }
        public string Sender { get;  set; }
        public string Subject { get;  set; }
        public string Body { get;  set; }
        public string Type { get;  set; }

        public Message(string header, string sender, string subject, string body)
        {
            Header = header;
            Sender = sender;
            Subject = subject;
            Body = body;
        }

        public override string ToString()
        {
            return "Message Type: "+this.Type+
                "\n\nHeader: "+ this.Header+
                "\n\nSender: "+ this.Sender+
                "\n\nSubject: "+this.Subject+
                "\n\nMessage: \n"+this.Body;
        }
    }
}
