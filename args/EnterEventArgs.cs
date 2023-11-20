using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectV01.args
{
    public class EnterEventArgs : EventArgs
    {
        public Idol Idol { get; private set; }
        public string Message { get; private set; }

        public EnterEventArgs(Idol idol, string message = "")
        {
            Idol = idol;
            Message = message;
        }
    }
}
