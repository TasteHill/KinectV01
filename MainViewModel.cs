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
        public string UserName { get; set; }
        public string IDolID { get; set; }
        public ICommand EnterCommand { get; private set; }

        public event EventHandler<args.EnterEventArgs> EnterComplete;

        public event EventHandler<args.SendIdolContextArgs> IdolExist;

        private void EnterCompleteMethod()
        {
            //MessageBox.Show($"입력된 이름 {UserName} 입력된 아이돌 {IDolID}");
            if (String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(IDolID))
                return;

            CurrentInfo.CURRENT_USER_NAME = UserName;
            CurrentInfo.CURRENT_IDOL_NAME = IDolID;

            if (this.model.isIdolExist(IDolID))
            {
                IdolExist?.Invoke(this, new args.SendIdolContextArgs(IDolID, this));
                this.navigator.exitEnterWindow();
                UserName = String.Empty;
                IDolID = String.Empty;
                return;
            }


            Idol idol = new Idol(IDolID);
            this.model.addIdol(idol);

            UserName = String.Empty;
            IDolID = String.Empty;

            EnterComplete?.Invoke(this, new args.EnterEventArgs(idol));
            this.navigator.exitEnterWindow();
        }


        ///<summary>
        ///아이돌 점수 관련
        /// </summary>


        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.model.CurrentIdolScore))
            {
                OnPropertyChanged(nameof(CurrentIdolScore));
            }
        }

        public int CurrentIdolScore
        {
            get { return this.model.CurrentIdolScore; }
            set
            {
                if (this.model.CurrentIdolScore != value)
                {
                    this.model.CurrentIdolScore = value;
                    OnPropertyChanged(nameof(CurrentIdolScore));
                }
            }
        }

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
            this.model.PropertyChanged += Model_PropertyChanged;
            startPointCalcCommand = new RelayCommand(startPointCalcMethod);


        }


    }
}
