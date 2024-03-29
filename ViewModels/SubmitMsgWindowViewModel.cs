﻿using NapierBankMessaging.Commands;
using NapierBankMessaging.Database;
using NapierBankMessaging.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;


namespace NapierBankMessaging.ViewModels
{
    public class SubmitMsgWindowViewModel
    {
        // Vars for Label Captions
        public string HeaderLabel { get; private set; }
        public string BodyLabel { get; private set; }

        // Vars for Text Box Contents 
        public string HeaderTxt { get; set; }
        public string BodyTxt { get; set; }

        // Vars for button 
        public string SubmitBtnCaption { get; private set; }
        public ICommand SubmitBtnCommand { get; private set; }

        private TextSpeak txtSpeak;

        public SubmitMsgWindowViewModel()
        {
            // Set label texts
            HeaderLabel = "Message Header: ";
            BodyLabel = "Message Body: ";

            // Set textboxes to clear
            HeaderTxt = string.Empty;
            BodyTxt = string.Empty;

            // Set button caption and command action
            SubmitBtnCaption = "Submit";
            SubmitBtnCommand = new RelayCommand(SubmitBtn_Clicked);

            txtSpeak = new TextSpeak();
        }

        // Function to 
        private void SubmitBtn_Clicked()
        {
            Message Message;
            MessageParser msgParse;

            msgParse = new MessageParser();
            MessageList msgList = new MessageList();
            char MsgType = 'a';

            if (HeaderTxt != " " && BodyTxt != " ")
            {
                MsgType = HeaderTxt[0];
            }

            // Determining what type of message
            if (MsgType == 'E')
            {
                var split = BodyTxt.Split(' ');
                string newBody = " ";
                string sender;
                string date = null;
                string sortCode = null;
                string natureOI = null;

                if (CheckHeaderID(HeaderTxt))
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
                            if (Regex.IsMatch(split[1], @"\b[0-9]{2}/?[0-9]{2}/?[0-9]{2}"))
                            {
                                date = split[1];
                            }

                            Regex rSort = new Regex(@"^(\d){2}-(\d){2}-(\d){2}$");
                            // Check if sort code is only numbers in correct form
                            if (rSort.IsMatch(split[2]))
                            {
                                sortCode = split[2];
                            }
                            else 
                            {
                                MessageBox.Show("Sortcode form invalid.");
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

                            Message = new SigIncidentReport(HeaderTxt, newBody, sender, date, sortCode, natureOI);
                            MessageBox.Show(newBody + ", " + date + ", " + sortCode + ", " + natureOI);

                        }
                        else
                        {
                            newBody = URLmessageCheck(split);

                            Message = new Email(HeaderTxt, newBody, sender);
                            MessageBox.Show(newBody);
                        }

                    }
                    else
                    {

                    }

                }

            }
            else if (MsgType == 'S')
            {
                var split = BodyTxt.Split(new[] { ' ' }, 2);
                string sender;

                if (CheckHeaderID(HeaderTxt))
                {
                    if (Regex.IsMatch(split[0], @"^\d+$") && split[0].Length == 11)
                    {
                        sender = split[0];
                        MessageBox.Show(split[0]);

                        // Check is message length is within 140 chars
                        if (split[1].Length <= 140)
                        {
                            string newB = TextspeakProcessing(split[1]);

                            Message = new SMStext(HeaderTxt, BodyTxt, sender, newB);
                            MessageBox.Show(newB);
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

                var split = BodyTxt.Split(' ');

                string[] textSpeakL = { };
                string sender;
                string[] mentionNames = { };
                string[] hashtags = { };


                if (CheckHeaderID(HeaderTxt))
                {
                    // Check if sender is valid (presence of '@' and length less that 15 (+ '@')
                    if (split[0].StartsWith("@") && split[0].Length <= 16)
                    {
                        sender = split[0];
                        MessageBox.Show(sender);
                        split[0] = "";

                        // Check if tweet length is within 140 chars
                        if ((BodyTxt.Length - split[0].Length) <= 140)
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

                            string body = string.Join(" ", split.Where(s => !string.IsNullOrEmpty(s)));
                            string newB = TextspeakProcessing(body);

                            Message = new Tweet(HeaderTxt, newB, sender, mentionNames, hashtags, textSpeakL);
                            MessageBox.Show(newB);
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
                return;
            }

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

        // Function to check for any URLs, saving them to hte Quarantine list and replacing in message
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