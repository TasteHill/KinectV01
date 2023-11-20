using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectV01.args
{
    public class SendIdolContextArgs
    {
        public String IdolName { get; private set; }
        public MainViewModel MainViewModel { get; private set; }

        public SendIdolContextArgs(String idolName, MainViewModel mainViewModel)
        {
            IdolName = idolName;
            MainViewModel = mainViewModel;
        }
    }
}
