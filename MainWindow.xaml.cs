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
            viewModel.EnterComplete += ViewModel_EnterComplete;
            viewModel.IdolExist += setDataContextToExistCelebPointInstance;
        }

        private void setDataContextToExistCelebPointInstance(object sender, SendIdolContextArgs e)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(rankWindow.gridList); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(rankWindow.gridList, i);

                if (child is CelebPoint celebPoint)
                {
                    if((child as CelebPoint).lblIdolName.Content.Equals(e.IdolName))
                    {
                        (child as CelebPoint).DataContext = e.MainViewModel;
                    }
                    else
                    {
                        int tempIdolPoint = (int)(child as CelebPoint).lblIdolPoint.Content;
                        (child as CelebPoint).DataContext = null;
                        (child as CelebPoint).lblIdolPoint.Content = tempIdolPoint;

                    }
                }
            }
        }

        private void ViewModel_EnterComplete(object sender, EnterEventArgs e)
        {

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(rankWindow.gridList); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(rankWindow.gridList, i);

                if (child is CelebPoint celebPoint)
                {
                    int tempIdolPoint = (int)(child as CelebPoint).lblIdolPoint.Content;
                    (child as CelebPoint).DataContext = null;
                    (child as CelebPoint).lblIdolPoint.Content = tempIdolPoint;
                }
            }
            this.rankWindow.AddIdol(e.Idol);
        }
    }
}
