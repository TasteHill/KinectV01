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
using System.Windows.Shapes;
namespace KinectV01.UserInfoControls
{
    /// <summary>
    /// UserInfoDetail.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserInfoDetail : Window
    {
        private int progressBarValue = 0;
        public UserInfoDetail()
        {
            InitializeComponent();
            TextBoxTest.TextChanged += TextBox_TextChanged;

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TextBox에 입력된 값 가져오기
            if (int.TryParse(TextBoxTest.Text, out int inputValue))
            {
                // 입력된 값으로 ProgressBar 업데이트
                UpdateProgressBar(inputValue);
            }
        }
        private void UpdateProgressBar(int value)
        {
            progressBarValue = value;
            if (progressBarValue <= 100)
            {
                ExpProgressBar.Value = progressBarValue;

                // 10% 증가할 때 마다 점수 출력

            }
            else
            {
                ExpProgressBar.Value = 100;
            }
        }

        private void ExpProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }
    }
}
