using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace project0
{
    public class UserButton: INotifyPropertyChanged
    {

        public const bool DEBUG = false;

        #region instance variables
        private int power = 0;
        private const int maxPower = 12;
        private int angle = 0;

        private bool pumpAudio = false;
        private bool endAudio = false;

        #endregion

        #region attributes
        public int Power
        {
            get
            {
                return this.power;
            }
            set
            {
                this.power = value;
                RaisePropertyChanged(new PropertyChangedEventArgs("Power"));
            }
        }

        public int Angle
        {
            get
            {
                return this.angle;
            }
            set
            {
                this.angle = value;
                RaisePropertyChanged(new PropertyChangedEventArgs("Angle"));
            }
        }

        #endregion

        /// <summary>
        /// Checks if power is at max level
        /// </summary>
        /// <returns>true or false</returns>
        public bool isMax()
        {
            if (power >= maxPower)
            {
                power = -1; //used to reset the button
                return true;
            }
            return false;
     
        }

        #region Methods
        /// <summary>
        /// Processes the events when the button is clicked
        /// </summary>
        public void processClick()
        {
            if (DEBUG) { Console.Write("[ProcessClick]\n"); }
            //TODO: Enter Code Here
            this.incrementPower();
            this.Angle += (360 / maxPower);
            this.playAudio();
        }

        /// <summary>
        /// Play mass effect audio for pumping and ending
        /// WARNING: Ending audio is louder then pumping audio
        /// </summary>
        private void playAudio()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            if (pumpAudio == false)
            {
                player.Stream = Properties.Resources.MassEffect2_SuicideMission;
                try
                {
                    player.Load();
                    player.Play();
                }
                catch (Exception E) { }
                pumpAudio = true;
            }
            else if (pumpAudio == true && this.power == 12)
            {
                player.Stop();
                player.Stream = Properties.Resources.MassEffect2_MissionAccomplished;
                try
                {
                    player.Load();
                    player.Play();
                }
                catch (Exception E) { }
                pumpAudio = false;
                endAudio = true;
            }
            else if (endAudio == true || this.power == -1)
            {
                player.Stop();
                pumpAudio = false;
                endAudio = false;
            }
        }


        /// <summary>
        /// increment power by 1
        /// </summary>
        public void incrementPower()
        {
            this.power++;
            RaisePropertyChanged(new PropertyChangedEventArgs("Power"));
            if (DEBUG) { Console.Write("[incrementPower]Power Incremented to:" + power + "\n"); }
        }

        public void reset()
        {
            //this angle is needed to properly display meter
            this.angle = -(360 / maxPower);
            //this.angle = 0;
            power = -1;
        }

        #endregion

        #region Events

        /// <summary>
        /// notifies listeners/observers of a property change
        /// </summary>
        /// <param name="e"></param>
        /// 
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            // Most events have a similar structure, they have a "sender", and some
            // type of event args
            PropertyChanged?.Invoke(this, e);
            if (DEBUG) { Console.Write("[Property Changed]\n"); }
        }
        #endregion

    }

}
