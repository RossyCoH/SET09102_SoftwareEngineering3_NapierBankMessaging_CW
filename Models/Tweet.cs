using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.Models
{
    class Tweet : Message
    {
        private string[] mentions;
        private string[] hashtags;

        public Tweet(string Header) : base(Header)
        {

        }
    }
}
