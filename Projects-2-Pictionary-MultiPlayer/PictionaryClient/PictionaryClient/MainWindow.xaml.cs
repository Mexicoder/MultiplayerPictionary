using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

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
      

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            connectToPictionaryGame();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //MyHintProperty = dw.wordHintFirstLetter_;  //set the hintProperty
            this.DataContext = this;
            RoleSplashTb.Text += "UserName: " + App.Current._userName;
        }

      

        private void connectToPictionaryGame()
        {
            try
            {
                // Configure the ABCs of using the MessageBoard service
                _cnvsBrd = new UserClient(new InstanceContext(this));                
                                             
                if (_cnvsBrd.Join(App.Current._userName))
                {
                    WordProperty = _cnvsBrd.GetWordHint();  

                    // TODO: currently we have it so that new players cant join mid game
                    whiteBoard.Children.Clear();

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
                MessageBox.Show(ex.Message);
            }
        }

        private void whiteBoard_mouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                _drawPoint = e.GetPosition(whiteBoard);
        }

        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
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
                    MessageBox.Show(ex.Message);
                }
            }
            else
                this.Dispatcher.BeginInvoke(new CanvasUpdateDelegate(SendLine), new object[] { jsonLine });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _cnvsBrd?.Leave(App.Current._userName);
            base.OnClosing(e);
        }



        public delegate void GameUpdateDelegate(bool status = false);

        public void FinishCurrentGame(bool status = false)
        {
            if (this.Dispatcher.Thread == System.Threading.Thread.CurrentThread)
            {
                try
                {

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                this.Dispatcher.BeginInvoke(new GameUpdateDelegate(FinishCurrentGame), new object[] { status });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //_cnvsBrd.CheckWord(GuessTB.Text);

        }

        //TODO  make this clear all windows not just the current one
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            whiteBoard.Children.Clear(); 
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
