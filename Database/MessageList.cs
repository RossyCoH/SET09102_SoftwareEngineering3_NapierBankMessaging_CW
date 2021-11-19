using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NapierBankMessaging.Models;

namespace NapierBankMessaging.Database
{
    public class MessageList
    {
        private List<Message> msgList;
        private Dictionary<string, string> quarntURLs;

        public MessageList()
        {
            msgList = new List<Message>();
            quarntURLs = new Dictionary<string, string>();
        }

        public void AddMessage(Message msg)
        {
            msgList.Add(msg);
        }

        public void AddURL(string msgID, string URL)
        {
            quarntURLs.Add(msgID, URL);
        }
    }
}
