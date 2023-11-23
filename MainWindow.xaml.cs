using CefSharp.DevTools.IndexedDB;
using KinectV01.args;
using KinectV01.UserInfoControls;
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

namespace KinectV01
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = (MainViewModel)Application.Current.TryFindResource("mainViewModel");
            viewModel.UpdateRank += updateComponents;
            viewModel.UpdateUiScore += updateComponentsScore;

        }

        
        private void updateComponents(object sender, args.UpdateRankArgs e)
        {
            rankWindow.reset();
            foreach(var idol in e.Idols)
            {
                rankWindow.AddIdol(idol);
            }
        }

        private void updateComponentsScore(object sender, args.UpdateUiScoreArgs e)
        {
            rankWindow.updateScoreAt(e.Idol,e.User ,e.Idol.IScore);
        }













        private void UserInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            UserInfoDetail abc = new UserInfoDetail();
            abc.Show();
        }

    }
}
