using KinectV01.UserInfoControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KinectV01
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Navigator navigator = new Navigator();
        private MainModel model = new MainModel();
        public event PropertyChangedEventHandler PropertyChanged;


        public ICommand CreateEnterWindowCommand { get; private set; }
        
        private void CreateEnterWindowMethod()
        {
            this.navigator.showEnterWindow();
        }




        /// <summary>
        /// 로그인 로직
        /// </summary>
        public ICommand EnterCompleteCommand { get; private set; }

        public ICommand EnterCommand { get; private set; }

        public event EventHandler<args.EnterEventArgs> EnterComplete;

        public event EventHandler<args.SendIdolContextArgs> IdolExist;

        private void EnterCompleteMethod()
        {
            string userName = UserName;
            string idolName = IDolID;

            this.model.userAndIdolEntered(idolName, userName);
            navigator.exitEnterWindow();
        }


        ///<summary>
        ///아이돌 점수 관련
        /// </summary>


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        ///<summary>
        ///키넥트 시작
        /// </summary>

        public ICommand startPointCalcCommand { get; private set; }
        public void startPointCalcMethod()
        {
            this.model.startPointCalc();
        }



        /// <summary>
        /// 생성자
        /// </summary>
        public MainViewModel()
        {
            CreateEnterWindowCommand = new RelayCommand(CreateEnterWindowMethod);
            EnterCompleteCommand = new RelayCommand(EnterCompleteMethod);

            startPointCalcCommand = new RelayCommand(startPointCalcMethod);
            startDisplayColorStreamCommand = new RelayCommand(startDisplayColorStreamMethod);

        }


        public ICommand startDisplayColorStreamCommand { get; private set; }

        public void startDisplayColorStreamMethod(object parameter)
        {
            this.model.startDisplayColorStream(parameter);
        }



        public List<Idol> getIdolAndUserFromDB()
        {
            var idols = this.model.getIdolAndUserFromDB();
            return idols;

        }


        public string UserName { get; set; }
        public string IDolID { get; set; }

        

    }
}
