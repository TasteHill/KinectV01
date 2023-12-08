﻿using KinectV01.UserInfoControls;
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


        private void EnterCompleteMethod()
        {
            string userName = UserName;
            string idolName = IDolID;

            this.model.initCurrentUserAndIdol(idolName, userName);
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
        public void startPointCalcMethod(object parameter)
        {
            List<object> parameters = parameter as List<object>;
            object canvas01 = parameters[0];
            object image02 = parameters[1];
            object image03 = parameters[2];
            this.model.startPointCalc();
            this.model.startDisplayColorStream(image02);
            this.model.startSkeletonStream(canvas01, image03);
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
            this.model.UpdateRank += sendCurrentUserAndIdolToView;
            this.model.UpdateUiScore += sendCurrentIdolScoreToView;
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

        
        public void sendCurrentUserAndIdolToView(object sender, args.UpdateRankArgs e)
        {
            UpdateRank?.Invoke(this, new args.UpdateRankArgs(e.Idols, e.Users));
        }

        public event EventHandler<args.UpdateRankArgs> UpdateRank;


        /// <summary>
        /// 점수 업데이트 이벤트
        /// </summary>

        public void sendCurrentIdolScoreToView(object sender, args.UpdateUiScoreArgs e)
        {
            UpdateUiScore?.Invoke(this, new args.UpdateUiScoreArgs(e.Idol, e.User));
        }

        public event EventHandler<args.UpdateUiScoreArgs> UpdateUiScore;
    }
}
