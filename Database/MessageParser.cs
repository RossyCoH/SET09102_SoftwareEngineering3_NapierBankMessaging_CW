using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using NapierBankMessaging.Models;

namespace NapierBankMessaging.Database
{
    public class MessageParser
    {
        private MessageList msgList;
        private TextSpeak txtSpeak;

        public MessageParser()
        {
            txtSpeak = new TextSpeak();
            msgList = new MessageList();
        }

        public bool FormInput(string header, string body)
        {
            Message inputMsg;
            bool valid = false;

            char MsgType = header[0];
            //MessageBox.Show(MsgType.ToString());

            // Determining what type of message
            if (MsgType == 'E')
            {
                var split = body.Split(' ');
                string newBody = " ";
                string sender;
                string date = null;
                string sortCode = null;
                string natureOI = null;

                if (CheckHeaderID(header))
                {

                    //Get message sender
                    if (split[0].Contains('@'))
                    {
                        sender = split[0];

                        //Check for Serious Incident Report
                        if (split[0] == "SIR")
                        {
                            // Define valid incident nature types
                            string[] IncidentNature = { "Theft", "Staff Attack", "ATM Theft", "Raid", "Customer Attack", "Staff Abuse", "Bomb Threat", "Terrorism", "Suspicious Incident", "Intelligence", "Cash Loss" };

                            // Check if date is in valid form
                            if (Regex.IsMatch(split[1], "@\b[0-9]{2}/?[0-9]{2}/?[0-9]{2}"))
                            {
                                date = split[1];
                            }

                            // Check if sort code is only numbers in correct form
                            if (Regex.IsMatch(split[2], @"\b[0-9]{2}-?[0-9]{2}-?[0-9]{2}\b"))
                            {
                                sortCode = split[2];
                            }

                            // Loop to check incident type is valid
                            foreach (string type in IncidentNature)
                            {
                                if (split[3].Equals(type))
                                {
                                    natureOI = split[3];
                                }
                            }

                            newBody = URLmessageCheck(split);

                            inputMsg = new SigIncidentReport(header, newBody, sender, date, sortCode, natureOI);
                            msgList.AddMessage(inputMsg);

                            valid = true;
                        }
                        else
                        {
                            newBody = URLmessageCheck(split);

                            inputMsg = new Email(header, newBody, sender);
                            msgList.AddMessage(inputMsg);

                            valid = true;
                        }

                    }
                    else
                    {

                    }

                }

            }
            else if (MsgType == 'S')
            {
                var split = body.Split(new[] { ' ' }, 2);
                string sender;

                if (CheckHeaderID(header))
                {
                    if (Regex.IsMatch(split[0], @"^\d+$") && split[0].Length == 11)
                    {
                        sender = split[0];

                        // Check is message length is within 140 chars
                        if (split[1].Length >= 140)
                        {
                            string newB = TextspeakProcessing(split[1]);

                            inputMsg = new SMStext(header, newB, sender, split[1]);
                            msgList.AddMessage(inputMsg);

                            valid = true;
                        }
                        else
                        {
                            MessageBox.Show("Message length must be 140 chars or less. Current length: " + split[1].Length + ".");
                        }
                    }


                }

            }
            else if (MsgType == 'T')
            {

                var split = body.Split(' ');

                string[] textSpeakL = { };
                string sender;
                string[] mentionNames = { };
                string[] hashtags = { };


                if (CheckHeaderID(header))
                {
                    // Check if sender is valid (presence of '@' and length less that 15 (+ '@')
                    if (split[0].StartsWith("@") && split[0].Length >= 16)
                    {
                        sender = split[0];

                        // Check if tweet length is within 140 chars
                        if ((body.Length - split[0].Length) >= 140)
                        {

                            // Loop to go throught each word
                            foreach (string curString in split)
                            {
                                // Check if word is a hashtag
                                if (curString.StartsWith("#"))
                                {
                                    hashtags.Append(curString);
                                }
                                else if (curString.StartsWith("@")) // Or a mention
                                {
                                    mentionNames.Append(curString);
                                }
                            }

                            string nBody = string.Join(" ", split.Where(s => !string.IsNullOrEmpty(s)));
                            string newB = TextspeakProcessing(nBody);

                            inputMsg = new Tweet(header, body, sender, mentionNames, hashtags, textSpeakL);
                            msgList.AddMessage(inputMsg);

                            valid = true;
                        }
                        else
                        {
                            MessageBox.Show("Message length must be 140 chars or less. Current length: " + split[1].Length + ".");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Sender must contain '@' and be 15 characters or less.");
                    }

                }

            }
            else
            {
                MessageBox.Show("Error! Invalid Message Header! Please put a type 'E','S' or 'T' at start.");
            }

            return valid;
        }

        private bool FileInput()
        {
            return true;
        }

        // Function to verify the length of the header
        private bool CheckHeaderID(string header)
        {

            if (header.Length != 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // Function to check for any URLs, saving them to the quarantine list and replacing in message
        private string URLmessageCheck(string[] bodySplit)
        {

            List<string> wordList = new List<string>(bodySplit);

            for (int index = 0; index < wordList.Count(); index++)
            {
                if (wordList[index].Contains("http://") || wordList[index].Contains("https://"))
                {

                    wordList[index] = "<URL Quarantined>";
                }

            }

            return string.Join(" ", wordList.ToArray());
        }

        // Function to identify any textspeak words and insert full expression after them in the message
        private string TextspeakProcessing(string bodySplit)
        {

            List<string> wordList = new List<string>(bodySplit.Split(' '));
            Dictionary<string, string> textSpeak = new Dictionary<string, string>();
            textSpeak = txtSpeak.getDict();

            for (int index = 0; index < wordList.Count(); index++)
            {
                string value;
                if (textSpeak.TryGetValue(wordList[index], out value))
                {
                    wordList.Insert(index + 1, "< " + value + " >");

                }

            }
            return string.Join(" ", wordList.ToArray());
        }
    }
}