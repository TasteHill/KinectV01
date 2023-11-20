using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FirstPage;
using Microsoft.Kinect;

namespace KinectV01
{
    public class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Idol> Idols = new List<Idol>();

        public List<Idol> GetIdols()
        {
            return this.Idols;
        }

        public Idol getIdol(String name)
        {
            foreach (Idol idol in this.Idols)
            {
                if (idol.IName.Equals(name))
                {
                    return idol;
                }
            }
            return null;
        }

        public bool isIdolExist(String name)
        {
            foreach (Idol idol in this.Idols)
            {
                if (idol.IName.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }


        public void addIdol(Idol idol)
        {
            this.Idols.Add(idol);
        }

        public int CurrentIdolScore
        {
            get
            {
                int score = 0;
                score = this.getIdol(CurrentInfo.CURRENT_IDOL_NAME).IScore;
                return score;
            }
            set
            {
                var idol = this.getIdol(CurrentInfo.CURRENT_IDOL_NAME);
                idol.IScore = value;
                OnPropertyChanged(nameof(CurrentIdolScore));

            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        ///<summary>
        ///키넥트 관련
        ///</summary>

        

        public void startPointCalc()
        {
            var nui = KinectSensor.KinectSensors[0];

            KinectController kinectController = new KinectController();



            kinectController.StartCapturingPlayerMovement((double score) =>
            {
                this.CurrentIdolScore += (int)(score*100);
            }, out CancellationTokenSource startPointCalcCanceltokensource);
        }
        

        ///<summary>
        ///DB 관련
        /// </summary>
        



        ///<summary>
        ///키넥트 초기화
        /// </summary>






    }
}
