using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PictionaryClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainWindow mainWindow { get; set; }

        public WelcomeSplash WelcomeSplashWindow { get; set; }

        public bool exiting { get; set; } // bool flag so that exiting message doesnt appear for each window closing 



        public string _userName { get; set; }

        public string _ipAddress { get; set; }
        /// <summary>
        /// Override of startup for custom initializations to be able to happen
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WelcomeSplashWindow = new WelcomeSplash(); //create an instance of that xaml class... WelcomeSplash.cs ... first window launched
            exiting = false;
            WelcomeSplashWindow.Show(); //show window
        }

        /// <summary>
        /// Allows access to members shared across all windows
        /// </summary>
        public static new App Current //static means it stays in memory whenever it is created for the first time and is sent to other windows
        {
            get
            {
                return Application.Current as App;
            }
        }

        /// <summary>
        /// Closing helper method that closes windows properly
        /// </summary>
        public void CloseAllWindows(System.ComponentModel.CancelEventArgs e)
        {
            if (Current.exiting == false) //so that message doesnt appear for each closing 
            {
                MessageBoxResult answer = MessageBox.Show("Are you sure you want to exit? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (answer == MessageBoxResult.Yes)
                {
                    Current.exiting = true;
                    e.Cancel = false;
                    Current.Shutdown();
                }
                else
                    e.Cancel = true;
            }

        }
    }
}
