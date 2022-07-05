using System.Windows;

namespace H_W_10._5_WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TelegramMessageClient client;

        public MainWindow()
        {
            InitializeComponent();

            client = new TelegramMessageClient(this);

            Loglist.ItemsSource = client.BotMessageLog;

            client.SendMessage(txtMsgSend.Text, TargetSend.Text);
        }

        public void BtnMsgSendClick(object sender, RoutedEventArgs e)
        {
            client.SendMessage(txtMsgSend.Text, TargetSend.Text);
        }
    }
}
