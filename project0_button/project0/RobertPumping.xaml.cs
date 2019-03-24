using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for RobertPumping.xaml
    /// </summary>
    public partial class RobertPumping : UserControl
    {

        bool pumpCompleted = false;
        bool pumpReverseCompleted = true;
        bool liftCompleted = true;


        Storyboard sb;
        Storyboard sbReverse;

        public RobertPumping()
        {
            InitializeComponent();
            sb = (this.Resources["Pump"] as Storyboard);
            sbReverse = (this.Resources["PumpReverse"] as Storyboard);
        }

        /// <summary>
        /// Animate Robert
        /// </summary>
        public void AnimateUp()
        {
            pumpCompleted = false;
            liftCompleted = false;

            sb.Begin();
           
        }

        public void AnimateDown()
        {
            pumpReverseCompleted = false;
            sb.Stop();
            sbReverse.Begin();
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            //pump completed
            pumpCompleted = true;
            
        }

        private void Storyboard_Completed_1(object sender, EventArgs e)
        {
            //pump reverse completed
            pumpReverseCompleted = true;
            if (pumpCompleted)
            {
                liftCompleted = true;
            }
        }

        public bool isLiftComplete()
        {
            return liftCompleted;
        }

        public bool isPumpComplete()
        {
            return pumpCompleted;
        }

        public bool isWeightDown()
        {
            return pumpReverseCompleted;
        }


        public void animateDropWeight()
        {
            sb.GetCurrentTime();

            TimeSpan tempTime = new TimeSpan(0, 0, 1) - sb.GetCurrentTime();
            sb.Stop();

            sbReverse.Seek(tempTime);
            sbReverse.Resume();


        }

        /// <summary>
        /// Raise the bar if in middle of lowering bar
        /// </summary>
        public void animateRaiseWeight()
        {
            sbReverse.GetCurrentTime();

            TimeSpan tempTime = new TimeSpan(0, 0, 1) - sbReverse.GetCurrentTime();
            sbReverse.Stop();

            sb.Seek(tempTime);
            sb.Resume();

        }

    }
}
