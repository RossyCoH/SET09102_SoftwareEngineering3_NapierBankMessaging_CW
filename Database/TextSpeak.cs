using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NapierBankMessaging.Database
{
    /// <summary>
    /// Class to read in and store a dictionary of text speak abrevistions 
    /// Written By: Ross HuntSer
    /// Date Created: 05/11/21
    /// </summary>
    class TextSpeak
    {
        private Dictionary<string, string> TextspeakAbvs { get; set; }

        public TextSpeak()
        {
            TextspeakAbvs = new Dictionary<string, string>();
            readFromFile();
        }

        private void readFromFile()
        {
            using (var reader = new StreamReader(@"D:\textwords.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var split = line.Split(',');

                    TextspeakAbvs.Add(split[0], split[1]);
                }

                reader.Close();
            }

        }


        public Dictionary<string, string> getDict()
        {
            return TextspeakAbvs;
        }

        public string OutputTxt(string phrase)
        {
            string result;
            TextspeakAbvs.TryGetValue(phrase, out result);
            return result;

        }
    }
    
}
