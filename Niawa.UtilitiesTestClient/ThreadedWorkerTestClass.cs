using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.UtilitiesTestClient
{
    class ThreadedWorkerTestClass
    {
        public void RunTest()
        {

            ThreadedWorkerTestImpl worker = new ThreadedWorkerTestImpl("Test Class");

            //start thread
            Console.WriteLine("Start request at " + DateTime.Now.ToString());
            worker.Start();
            System.Threading.Thread.Sleep(5000);

            //suspend thread
            Console.WriteLine("Suspend request at " + DateTime.Now.ToString());
            worker.Suspend();
            System.Threading.Thread.Sleep(5000);

            //resume thread
            Console.WriteLine("Resume request at " + DateTime.Now.ToString());
            worker.Resume();
            System.Threading.Thread.Sleep(5000);

            //stop thread
            Console.WriteLine("Stop request at " + DateTime.Now.ToString());
            worker.Stop();

            //finalize thread
            Console.WriteLine("Finalize request at " + DateTime.Now.ToString());
            worker.FinalizeWorker();


        }

    }
}
