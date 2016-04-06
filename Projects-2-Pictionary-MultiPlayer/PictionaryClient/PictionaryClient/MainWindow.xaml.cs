/// <summary>
///     Author:     Alex Seceanschi & John Friesen
///     Student#:   0690827 & 0666315
///     Date:       Created: April 6, 2016
///     Purpose:    A Visual Studio Windows WPF project called Pictionary. 
///                     The main purpose of this project is to design a 
///                     multi-player game application that incorporates 
///                     multiple assemblies and WCF.
/// </summary>

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ServiceModel;
using System.Web.Script.Serialization;
using PictionaryClient.PictionaryServiceRef;

namespace PictionaryClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public partial class MainWindow : Window, IUserCallback
    {
        Point _drawPoint = new Point();
        private UserClient _cnvsBrd = null;
        private string _drawWord = "";

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            connectToPictionaryGame();
        }

        /// <summary>
        /// sets data context 
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {        
            this.DataContext = this;
            RoleSplashTb.Text += "UserName: " + App.Current._userName;
        }

        /// <summary>
        /// Configues ABCs. Shows and hides guess/draw panels based on role
        /// </summary>
        private void connectToPictionaryGame()
        {
            try
            {
                // Configure the ABCs of using the MessageBoard service
                _cnvsBrd = new UserClient(new InstanceContext(this), "User", App.Current._ipAddress);

                if (_cnvsBrd.Join(App.Current._userName))
                {
                    _drawWord = _cnvsBrd.GetWord();
                    var Drawer = _cnvsBrd.getDrawer();
                    DrawerNameTb.Text = Drawer;
                    if (_cnvsBrd.isDrawer(App.Current._userName)) //drawer settings
                    {
                        WordProperty = _drawWord;
                        GuessPanel.Visibility = Visibility.Hidden;
                        DrawPanel.Visibility = Visibility.Visible;
                    }
                    else //guesser settings
                    {
                        WordProperty = _cnvsBrd.GetWordHint();
                        GuessPanel.Visibility = Visibility.Visible;
                        DrawPanel.Visibility = Visibility.Hidden;                        
                    }
                    
                    var canvasLines = _cnvsBrd.getCanvas();

                    if (canvasLines.Length > 0)
                    {

                        foreach (var line in canvasLines)
                        {
                            var customLine = new JavaScriptSerializer().Deserialize<JsonLine>(line);
                            whiteBoard.Children.Add(JsonLine.LineDeserialize(customLine));
                        }
                    }
                }
                else
                {
                    // Alias rejected by the service so nullify service proxies
                    _cnvsBrd = null;
                    MessageBox.Show("ERROR: Alias in use. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect to service. Error Message: "+ex.Message);
            }
        }

        /// <summary>
        /// Mouse Down event for drawing
        /// </summary>
        private void whiteBoard_mouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                _drawPoint = e.GetPosition(whiteBoard);
        }

        /// <summary>
        /// Mouse Move event for drawing
        /// </summary>
        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed && _cnvsBrd.isDrawer(App.Current._userName))
                {
                    Line line = new Line();

                    line.Stroke = MyColorProperty;
                    line.StrokeThickness = MyMarkerThicknessProperty;

                    line.X1 = _drawPoint.X;
                    line.Y1 = _drawPoint.Y;
                    line.X2 = e.GetPosition(whiteBoard).X;
                    line.Y2 = e.GetPosition(whiteBoard).Y;

                    string serializedLine = new JavaScriptSerializer().Serialize(new JsonLine()
                    {
                        Color = MyColorProperty.Color.ToString(),
                        StrokeThickness = line.StrokeThickness,
                        X1 = line.X1,
                        Y1 = line.Y1,
                        X2 = line.X2,
                        Y2 = line.Y2
                    });

                    _cnvsBrd.PostLine(serializedLine);
                    _drawPoint = e.GetPosition(whiteBoard);
                    whiteBoard.Children.Add(line);
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show("Could not update. Please check connection .\nError Message: " + ex.Message);

            }
        }

        #region Dependencies

        public SolidColorBrush MyColorProperty
        {
            get { return (SolidColorBrush)GetValue(MyColorPropertyProperty); }
            set { SetValue(MyColorPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyColorProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyColorPropertyProperty =
            DependencyProperty.Register("MyColorProperty", typeof(SolidColorBrush), typeof(MainWindow), new PropertyMetadata(Brushes.Black));




        public string WordProperty
        {
            get { return (string)GetValue(WordPropertyProperty); }
            set { SetValue(WordPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyHintProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WordPropertyProperty =
            DependencyProperty.Register("WordProperty", typeof(string), typeof(MainWindow), new PropertyMetadata(""));



        public double MyMarkerThicknessProperty
        {
            get { return (double)GetValue(MyMarkerThicknessPropertyProperty); }
            set { SetValue(MyMarkerThicknessPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyMarkerThicknessProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyMarkerThicknessPropertyProperty =

            DependencyProperty.Register("MyMarkerThicknessProperty", typeof(double), typeof(MainWindow), new PropertyMetadata(5.0));
        #endregion

        #region Colors
        private void redMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.Red;
        }

        private void orangeMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.Orange;
        }

        private void yellowMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.Yellow;
        }

        private void greenMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.LawnGreen;
        }

        private void blueMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.Blue;
        }

        private void brownMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.Brown;
        }

        private void greyMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.Gray;
        }

        private void blackMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.Black;
        }

        private void eraser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.White;
        }

        private void purpleMarker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyColorProperty = Brushes.Purple;
        }
        #endregion 


        public delegate void CanvasUpdateDelegate(string jsonLine);
        /// <summary>
        /// sendLine callback that sends a drawn line to client window from drawer
        /// </summary>
        public void SendLine(string jsonLine)
        {
            if (this.Dispatcher.Thread == System.Threading.Thread.CurrentThread)
            {
                try
                {
                    var customLine = new JavaScriptSerializer().Deserialize<JsonLine>(jsonLine);
                    whiteBoard.Children.Add(JsonLine.LineDeserialize(customLine));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not update line .Error Message: " + ex.Message);
                }
            }
            else
                this.Dispatcher.BeginInvoke(new CanvasUpdateDelegate(SendLine), new object[] { jsonLine });
        }



        public delegate void GameUpdateDelegate(string userWinner, bool status = false);
        /// <summary>
        /// Finish current game callback that notifies winner and users that they lost
        /// Also resets the drawer and sets a new game word and modifies UIs accordingly
        /// </summary>
        public void FinishCurrentGame(string userWinner, bool status = false)
        {
            if (this.Dispatcher.Thread == System.Threading.Thread.CurrentThread)
            {
                try
                {                    
                    if (status) //winner
                    {
                        MessageBox.Show("Congratulations " + App.Current._userName + ", you won!");                       
                        GuessPanel.Visibility = Visibility.Hidden;
                        DrawPanel.Visibility = Visibility.Visible; //if you won, you become the next drawer   
                        _drawWord = WordProperty = _cnvsBrd.GetWord();                         
                    }
                    else //for losers
                    {                  
                        if (App.Current._userName == DrawerNameTb.Text)
                        {
                            MessageBox.Show("Congrats " + App.Current._userName + "! " + userWinner + " guessed your drawing!");
                            GuessPanel.Visibility = Visibility.Visible;
                            DrawPanel.Visibility = Visibility.Hidden;
                        }
                        else { 
                            MessageBox.Show("Sorry " + App.Current._userName + ", you lost. " + userWinner + " won! The correct word was " + _drawWord);
                            GuessPanel.Visibility = Visibility.Visible;
                            DrawPanel.Visibility = Visibility.Hidden;
                        }
                         WordProperty = _cnvsBrd.GetWordHint();
                         _drawWord = _cnvsBrd.GetWord();
                    }

                    //setup for next game
                    DrawerNameTb.Text = _cnvsBrd.getDrawer();
                    whiteBoard.Children.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error with finish game status .Error Message: " + ex.Message);
                }
            }
            else
                this.Dispatcher.BeginInvoke(new GameUpdateDelegate(FinishCurrentGame), new object[] { userWinner, status });
        }


        public delegate void GameResetDelegate(string newDrawerName);
        /// <summary>
        /// Resets all clients games. This callback gets called when a drawer leaves the game.
        /// It updates UIs accordingly and notifies all users of the new drawer.
        /// </summary>
        public void ResetClientsGame(string newDrawerName)
        {
            if (this.Dispatcher.Thread == System.Threading.Thread.CurrentThread)
            {
                try
                {                  
                    MessageBox.Show(DrawerNameTb.Text + " left the game. The new drawer is " + newDrawerName);
                    DrawerNameTb.Text = newDrawerName; //set the new drawer to the textbox
                    whiteBoard.Children.Clear();       //clear board      
                    _drawWord = _cnvsBrd.GetWord();  //get a new word

                    if (App.Current._userName == DrawerNameTb.Text)
                    {
                        WordProperty = _drawWord;
                        GuessPanel.Visibility = Visibility.Hidden;
                        DrawPanel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        WordProperty = _cnvsBrd.GetWordHint();
                        GuessPanel.Visibility = Visibility.Visible;
                        DrawPanel.Visibility = Visibility.Hidden;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reseting client .Error Message: " + ex.Message);
                }
            }
            else
                this.Dispatcher.BeginInvoke(new GameResetDelegate(ResetClientsGame), new object[] { newDrawerName });
        }

        /// <summary>
        /// Button click handler for submit button
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!_cnvsBrd.CheckWord(GuessTB.Text, App.Current._userName))
                    MessageBox.Show("Sorry. Guess again!");
                GuessTB.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving from host .Error Message: " + ex.Message);
            }

        }

        /// <summary>
        /// Window closing event handler to ensure all windows close properly
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (App.Current.exiting == false) //so that message doesnt appear for each closing 
            {
                MessageBoxResult answer = MessageBox.Show("Are you sure you want to exit? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (answer == MessageBoxResult.Yes)
                {
                    _cnvsBrd?.Leave(App.Current._userName);
                    App.Current.exiting = true;
                    e.Cancel = false;
                    App.Current.Shutdown();
                }
                else
                    e.Cancel = true;
            }
        }
    }
}
