using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NapierBankMessaging.ViewModels;
using NapierBankMessaging.Models;

namespace NapierBankMessaging
{
    /// <summary>
    /// Interaction logic for MessageViewWindow.xaml
    /// </summary>
    public partial class MessageViewWindow : Window
    {
        public MessageViewWindow(List<Message> msgList)
        {
            InitializeComponent();
            this.DataContext = new MessageViewWindowViewModel(msgList);
        }
    }
}
