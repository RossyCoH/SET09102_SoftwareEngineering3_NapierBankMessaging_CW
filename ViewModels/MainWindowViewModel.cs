using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NapierBankMessaging.Commands;

namespace NapierBankMessaging.ViewModels
{
    class MainWindowViewModel
    {
        public string MessageBtnCaption { get; private set; }
        public ICommand MessageBtnCommand { get; private set; }

        public MainWindowViewModel()
        {
            MessageBtnCaption = "Message";
            MessageBtnCommand = new RelayCommand(MessageBtn_Clicked);
        }

        private void MessageBtn_Clicked()
        {
            MessageSubmitWindow MsgSubmitWindow = new MessageSubmitWindow();
            MsgSubmitWindow.Show();
            
        }
    }
}
