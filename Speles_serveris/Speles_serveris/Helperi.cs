using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Speles_serveris
{
    public class Helperi
    {
        public static void CreateNewThread(Action Function)
        {
            //  Function.Invoke();
            ThreadStart start = () => Function();
            Thread t = new Thread(start);
            t.Start();
        }
        public static string ServeraLaiks(TimeSpan UpTime)
        {
            return string.Format("{0:0}:{1:00}:{2:00}:{3:00}", (int)(UpTime.TotalDays), UpTime.Hours, UpTime.Minutes, UpTime.Seconds);
        }
    }
}
