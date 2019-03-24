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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace project0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public UserButton userButton = new UserButton();
        public const bool DEBUG = false;

        public MainWindow()
        {

            if (DEBUG) { Console.WriteLine("[Init]"); }
            InitializeComponent();

            //event Handlers
            this.KeyUp += OnMainWindowKeyUp;

            this.userButton.PropertyChanged += OnCounterPropertyChanged;

        }

        #region methods

        /// <summary>
        /// Closes the window with the escape key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMainWindowKeyUp(object sender, KeyEventArgs e)
        {
            // if the user 'releases' escape, close the program
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Changes the power label when signalled (for debugging - delete when required)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCounterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (DEBUG) { Console.WriteLine("[On Counter property Change]:" + e); }
            //this.powerLabel.Content = this.userButton.Power;
        }


        private void PowerMeter_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            if (userButton.Power == -1)
            {
                this.powerMeter.resetMeter();
                this.cake.Visibility = Visibility.Hidden;
                this.userButton.reset();
                this.cake.reset();
            }

                if (userButton.Power > 5)
            {
                this.sweating.Visibility = Visibility.Visible;
                this.sweating.animateSweating();
            }
        }
       
        /// <summary>
        /// Calls the button logic when a button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleUserClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DEBUG) { Console.WriteLine("[Handle User Click]"); }

                this.userButton.processClick();
                this.powerMeter.progressBar.EndAngle = userButton.Angle;
                this.powerMeter.meterColor(userButton.Angle);
           

            if (userButton.isMax())
            {
                this.cake.Visibility = Visibility.Visible;
                this.sweating.Visibility = Visibility.Hidden;
                this.powerMeter.hideRobert();
                this.powerMeter.rotateMeter();
                this.cake.animate();

            }

                
            
        }
        #endregion

        private void RobertPumping_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void PowerMeter_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void PowerMeter_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void Cake_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PowerMeter_MouseLeftButtonDown_1(sender, e);
        }
    }
}
