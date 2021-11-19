using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.Models
{
    class SMStext : Message
    {
        private string Sender;
        private string Text;
        //private string[] TextSpeak;

        public SMStext(string Header, string msgBody, string SenderNo, string text) : base(Header, msgBody)
        {
            Sender = SenderNo;
            Text = text;
        }
    }
}