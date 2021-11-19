using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NapierBankMessaging.Commands;
using NapierBankMessaging.Database;

namespace NapierBankMessaging.ViewModels
{
    public class MainWindowViewModel
    {
        public string MessageBtnContent { get; private set; }
        public string ViewMsgsBtnContent { get; private set; }


        public ICommand MessageBtnCommand { get; private set; }
        public ICommand ViewMsgsBtnCommand { get; private set; }


        public MainWindowViewModel()
        {
            MessageBtnContent = "Message";
            ViewMsgsBtnContent = "View Messages";

            MessageBtnCommand = new RelayCommand(MessageBtn_Clicked);
            ViewMsgsBtnCommand = new RelayCommand(ViewMsgsBtn_Clicked);
        }

        private void MessageBtn_Clicked()
        {
            MessageSubmitWindow MsgSubmitWindow = new MessageSubmitWindow();
            MsgSubmitWindow.Show();
            
        }

        private void ViewMsgsBtn_Clicked()
        {
            
        }
    }
}
