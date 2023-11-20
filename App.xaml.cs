using FirstPage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace KinectV01
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class App : Application
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 콘솔 창 생성
            AllocConsole();
            Console.WriteLine("프로그램 초기화 시작...");

            // 비동기적으로 초기화 로직 실행
            await InitializeApplicationAsync();

            // 초기화 작업 후 콘솔 창 제거
            FreeConsole();

            // 메인 윈도우 생성 및 표시
        }

        private async Task InitializeApplicationAsync()
        {
            DB DB = new DB();

            List<Idol> idols = DB.getIdolsFromDB();

            foreach(var idol in idols)
            {
                Console.WriteLine(idol.IName);

            }

            Console.WriteLine("출력 끝");
            Task.Delay(5000).Wait();
        }
    }
}
