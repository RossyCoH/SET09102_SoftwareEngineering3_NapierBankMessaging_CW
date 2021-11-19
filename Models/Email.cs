using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.Models
{
    class Email : Message
    {
        private bool type;
        private string sender;
        private string[] URL;

        public Email(string Header, string msgBody, string mSender) : base(Header, msgBody)
        {
            sender = mSender;
        }
    }
}
