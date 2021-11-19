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
    /// <summary>
    /// 
    /// </summary>
    public class MessageViewWindowViewModel
    {
        string typeLabel { get;  set; }
        string headerLbl { get;  set; }
        string bodyLbl { get; set; }

        string typeTxt { get; set; }
        string headerTxt { get; set; }
        string bodyTxt { get; set; }

        string backBtnContent { get; set; }
        string prvBtnContent { get; set; }
        string nextBtnContent { get; set; }

        ICommand backBtnCommand { get; set; }
        ICommand prvBtnCommand { get; set; }
        ICommand nextBtnCommand { get; set; }

        List<Message> msgList;
        int msgNum;

        public MessageViewWindowViewModel(List<Message> messages)
        {
            msgList = messages;
            msgNum = 0;

            typeLabel = "Type: ";
            headerLbl = "Header: ";
            bodyLbl = "Body: ";

            backBtnContent = "Back";
            prvBtnContent = "Previous";
            nextBtnContent = "Next";

            backBtnCommand = new RelayCommand(BackBtn_Clicked);
            prvBtnCommand = new RelayCommand(PrvBtn_Clicked);
            nextBtnCommand = new RelayCommand(NextBtn_Clicked);

            showMessage(); //Call to show first (0) message
        }

        private void PrvBtn_Clicked()
        {
            if(msgNum == 0)
            {
                MessageBox.Show("No more previous messages!");
            }
            else
            {
                msgNum -= 1;

                showMessage();
            }
        }

        private void NextBtn_Clicked()
        {
            if (msgNum == msgList.Count)
            {
                MessageBox.Show("No more messages!");
            }
            else
            {
                msgNum += 1;

                showMessage();
            }
        }

        private void BackBtn_Clicked()
        {

        }

        private void showMessage()
        {
            if (msgList.Count > 0)
            {
                typeTxt = msgList[msgNum].GetType().ToString();
                headerTxt = msgList[msgNum].GetHeader();
                bodyTxt = msgList[msgNum].GetBody();
            }
            else
            {
                MessageBox.Show("No messages");
            }
            
        }
    }
}
