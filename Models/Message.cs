using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.Models
{
    public class Message
    {
        private string header;
        private string body;

        public Message(string id, string msgBody)
        {
            header = id;
            body = msgBody;
        }

        public string GetHeader()
        {
            return header;
        }

        public string GetBody()
        {
            return body;
        }
    }
}
