using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using FirstPage;
using Microsoft.Kinect;

namespace KinectV01
{
    public class MainModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Idol> Idols = new List<Idol>();
        private List<User> users = new List<User>();

        public List<Idol> GetIdols()
        {
            return this.Idols;
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        ///<summary>
        ///키넥트 관련
        ///</summary>


        KinectController kinectController = new KinectController();
        public void startPointCalc()
        {
            kinectController.StartCapturingPlayerMovement((double score) =>
            {
                //this.CurrentIdolScore += (int)(score*100);
            }, out CancellationTokenSource startPointCalcCanceltokensource);
        }
        

        ///<summary>
        ///DB 관련
        /// </summary>
        
        public void startDisplayColorStream(object imageObj)
        {
            Image targetImage = imageObj as Image;
            kinectController.DisplayColorStreamAt(targetImage);
        }




        DB DB = new DB();
        public List<Idol> getIdolAndUserFromDB()
        {
            var idols = DB.getIdolsFromDB();
            var users = DB.getUserFromDB();


            
            foreach(var idol in idols)
            {
                this.Idols.Add(idol);
            }

            foreach (var user in users)
            {
                this.users.Add(user);
            }

            return idols;
        }


        public void userAndIdolEntered(string idolName, string userName)
        {
            //DB.SetUserAndIdol(userName, idolName, this.Idols, this.users);


        }





    }
}
