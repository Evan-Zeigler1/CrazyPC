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
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Diagnostics;
using System.Windows.Threading;

namespace WPFMouseGame
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Random _random = new Random();
        int playerScore = 0;
        public static int xMouseLocation = 20;
        public static int yMouseLocation = 20;
        Stopwatch stopWatch = new Stopwatch();
        public static string stopwatchTime;

        public MainWindow()
        {


            Thread crazyMouseThread = new Thread(new ThreadStart(CrazyMouseThread));
            crazyMouseThread.Start();
            stopWatch.Start();
        }       

        public void CrazyMouseThread()
        {
            int moveX = 0;
            int moveY = 0;
         
            while (true)
            {
                moveX = _random.Next(xMouseLocation) - (xMouseLocation / 2);
                moveY = _random.Next(yMouseLocation) - (yMouseLocation / 2);

                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X + moveX, System.Windows.Forms.Cursor.Position.Y + moveY);
                Thread.Sleep(50);
            }
        }

        private void xTheButton_Click(object sender, RoutedEventArgs e)
        {
            if (xTheButton.Height <= 100)
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                stopwatchTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
                MessageBox.Show($"Congrats You won in {stopwatchTime} Seconds!");
                MessageBox.Show("You set the hight score!");
                Environment.Exit(0);
            }
            else
            {
                //Increment Score
                playerScore++;
                xMyScore.Text = playerScore.ToString();

                //Increment Randomness
                xMouseLocation = xMouseLocation + 2;
                yMouseLocation = yMouseLocation + 2;

                //Shrink the Target
                xTheButton.Height = xTheButton.Height - 5;
                xTheButton.Width = xTheButton.Width - 7;
            }
        }
    }
}
