using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.UtilitiesTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadedWorkerTestClass tc = new ThreadedWorkerTestClass();
            //tc.RunTest();

            //BufferedLoggerTestClass tc = new BufferedLoggerTestClass("");
            //tc.RunTest();

            EventBasedLoggingTestParent tc = new EventBasedLoggingTestParent("");
            tc.RunTest();


        }
    }
}
