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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;


namespace PictionaryClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point drawPoint = new Point();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            List<DrawWord> dwList = new List<DrawWord>();

            //Generate words:
            dwList.Add(new DrawWord("DARK", "ADJECTIVE", "_ _ _ _", "D _ _ _"));
            dwList.Add(new DrawWord("LASER", "NOUN", "_ _ _ _ _", "L _ _ _ _"));
            dwList.Add(new DrawWord("WOBBLE", "VERB", "_ _ _ _ _ _", "W _ _ _ _ _"));
            dwList.Add(new DrawWord("SKI GOGGLES", "NOUN", "_ _ _  _ _ _ _ _ _ _", "S _ _  G _ _ _ _ _ _"));

            Random r = new Random();
            DrawWord dw = dwList[r.Next(0, dwList.Count)]; //pick a random DrawWord from the list
            MyHintProperty = dw.wordHintFirstLetter_;  //set the hintProperty


            this.DataContext = this;
        }

        private void whiteBoard_mouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                drawPoint = e.GetPosition(whiteBoard);
        }

        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Line line = new Line();

                line.Stroke = MyColorProperty;
                line.StrokeThickness = MyMarkerThicknessProperty;

                line.X1 = drawPoint.X;
                line.Y1 = drawPoint.Y;
                line.X2 = e.GetPosition(whiteBoard).X;
                line.Y2 = e.GetPosition(whiteBoard).Y;

                drawPoint = e.GetPosition(whiteBoard);

                whiteBoard.Children.Add(line);
            }
        }




        public SolidColorBrush MyColorProperty
        {
            get { return (SolidColorBrush)GetValue(MyColorPropertyProperty); }
            set { SetValue(MyColorPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyColorProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyColorPropertyProperty =
            DependencyProperty.Register("MyColorProperty", typeof(SolidColorBrush), typeof(MainWindow), new PropertyMetadata(Brushes.Black));




        public string MyHintProperty
        {
            get { return (string)GetValue(MyHintPropertyProperty); }
            set { SetValue(MyHintPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyHintProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyHintPropertyProperty =
            DependencyProperty.Register("MyHintProperty", typeof(string), typeof(MainWindow), new PropertyMetadata(""));



        public double MyMarkerThicknessProperty
        {
            get { return (double)GetValue(MyMarkerThicknessPropertyProperty); }
            set { SetValue(MyMarkerThicknessPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyMarkerThicknessProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyMarkerThicknessPropertyProperty =
            DependencyProperty.Register("MyMarkerThicknessProperty", typeof(double), typeof(MainWindow), new PropertyMetadata(5.0));





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
    }
}
