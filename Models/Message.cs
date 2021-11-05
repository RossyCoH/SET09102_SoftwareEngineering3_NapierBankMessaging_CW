using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.Models
{
    class Message
    {
        private string idNum;
        private string body;

        public Message(string id)
        {
            idNum = id;
        }
    }
}
