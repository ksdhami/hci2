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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project0
{
    /// <summary>
    /// Interaction logic for cake.xaml
    /// </summary>
    public partial class cake : UserControl
    {
        public cake()
        {
            InitializeComponent();
            this.MouseDown += UserControl_MouseLeftButtonUp;
        }

        /// <summary>
        /// Provide a routed event for main window or other windwos listening
        /// </summary>
        public static readonly RoutedEvent UserClickEvent = EventManager.RegisterRoutedEvent("UserClickCake", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PowerMeter));

        // Provide CLR accessors for the event
        public event RoutedEventHandler UserClickCake
        {
            add { AddHandler(UserClickEvent, value); }
            remove { RemoveHandler(UserClickEvent, value); }
        }

        private void RaiseUserClickEvent()
        {
            RoutedEventArgs userClickEventArgs = new RoutedEventArgs();
        }

        /// <summary>
        /// animate the cake
        /// </summary>
        public void animate()
        {
            Storyboard sb = (this.Resources["CakeRiseSB"] as Storyboard);
            sb.Begin();
        }

        /// <summary>
        /// Reset the cake
        /// </summary>
        public void reset()
        {
            Storyboard sb = (this.Resources["CakeRiseSB"] as Storyboard);
            sb.Seek(TimeSpan.Zero, TimeSeekOrigin.BeginTime);
            sb.Stop();
  
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.RaiseEvent(new RoutedEventArgs(UserClickEvent, this));
        }
    }

}
