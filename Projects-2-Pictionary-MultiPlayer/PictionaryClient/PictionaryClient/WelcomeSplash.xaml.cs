using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PictionaryClient
{
    /// <summary>
    /// Interaction logic for WelcomeSplash.xaml
    /// </summary>
    public partial class WelcomeSplash : Window
    {
        public WelcomeSplash()
        {
            InitializeComponent();
        }
        
        private void JoinGame_Click(object sender, RoutedEventArgs e)
        {
            var regex = @"^(([1-9]?\d|1\d\d|25[0-5]|2[0-4]\d)\.){3}([1-9]?\d|1\d\d|25[0-5]|2[0-4]\d)$";
            if (UserNameTb.Text == "")
            {
                MessageBox.Show("Please fill out userName field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (HostIpTb.Text == "")
            {                
                MessageBox.Show("Please fill out Host IP Address field", "Error", MessageBoxButton.OK, MessageBoxImage.Error);          
            }               
            else
            {
                var match = Regex.Match(HostIpTb.Text, regex, RegexOptions.IgnoreCase);

                if (!match.Success && HostIpTb.Text != "localhost")
                {
                    MessageBox.Show("Host IP Address field is incorrect format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //populate to app's variables
                    App.Current._userName = UserNameTb.Text;
                    App.Current._ipAddress = $"http://{HostIpTb.Text}:12000/PictionaryLibrary/User";
                    App.Current.mainWindow = new MainWindow(); //create a game window                 
                    App.Current.mainWindow.Show();
                    App.Current.WelcomeSplashWindow.Hide();
                }                
            }
        }

        /// <summary>
        /// Window closing event handler to ensure all windows close properly
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
