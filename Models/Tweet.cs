using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.Models
{
    class Tweet : Message
    {
        private string sender;
        private string[] mentions;
        private string[] hashtags;
        private string[] TextSpeak;

        public Tweet(string Header, string msgBody, string senderM, string[] mentionL, string[] hashtagL, string[] textSpeak) : base(Header, msgBody)
        {
            sender = senderM;
            mentions = mentionL;
            hashtags = hashtagL;
        }
    }
}
