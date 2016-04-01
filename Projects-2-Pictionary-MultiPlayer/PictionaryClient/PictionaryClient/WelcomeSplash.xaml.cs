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
                //populate to app's variables
                App.Current._userName = UserNameTb.Text;
                App.Current._ipAddress = $"http://{HostIpTb.Text}:12000/PictionaryLibrary/User";

                App.Current.mainWindow = new MainWindow(); //create a game window                 
                App.Current.mainWindow.Show();
                App.Current.WelcomeSplashWindow.Hide();
            }
        }

        /// <summary>
        /// Window closing event handler to ensure all windows close properly
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.CloseAllWindows(e);
        }
    }
}
