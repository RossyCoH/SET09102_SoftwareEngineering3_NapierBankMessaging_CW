using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NapierBankMessaging.Commands;
using NapierBankMessaging.Models;

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
        }

        // Function to 
        private void SubmitBtn_Clicked()
        {
            Message Message;

            char MsgType = HeaderTxt[0];
            MessageBox.Show(MsgType.ToString());

            // Determining what type of message
            if (MsgType == 'E')
            {
                if (CheckHeaderID())
                {
                    
                }

                Message = new Email(HeaderTxt);
                MessageBox.Show("Email");

            }
            else if (MsgType == 'S')
            {
                var split = BodyTxt.Split(new[] { ' ' }, 2);

                if (CheckHeaderID())
                {
                    Message = new SMStext(HeaderTxt, split[0], split[1]);
                    MessageBox.Show("SMS" + ", " + HeaderTxt + " ," + split[0] + " ," + split[1]);
                }

            }
            else if (MsgType == 'T')
            {
                Message = new Tweet(HeaderTxt);
                MessageBox.Show("Tweet");
            }
            else
            {
                MessageBox.Show("Error! Invalid Message Header! Please put a type 'E','S' or 'T' at start.");
                return;
            }
        }

        private bool CheckHeaderID()
        {
            string header;

            header = HeaderTxt;

            if (header.Length != 10)
            {
                MessageBox.Show("HeaderID not valid");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}