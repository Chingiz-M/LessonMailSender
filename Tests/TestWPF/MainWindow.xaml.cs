using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        // Менюшку добавил для красоты, функционал пока не описал
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var from = new MailAddress(MailFrom.Text);
                var to = new MailAddress(MailTo.Text);

                var message = new MailMessage(from, to);
                message.Subject = SubjectEdit.Text;
                message.Body = Message.Text;

                var client = new SmtpClient(ServerName.Text, Convert.ToInt32(PortNum.Text));
                if(CbSSL.IsChecked == true)
                    client.EnableSsl = true;
                else
                    client.EnableSsl = false;

                client.Credentials = new NetworkCredential()
                {
                    UserName = LoginEdit.Text,
                    SecurePassword = PasswordEdit.SecurePassword
                };

                client.Send(message);
                MessageBox.Show("Почта успешно отправлена!", "Отправка почты", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show("Ошибка авторизации", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Ошибка адреса сервера", "Ошибка отправки почты", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Введите параметры!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введенные данные не соответствуют формату!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PortNum_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
