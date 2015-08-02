using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.UtilitiesTestClient
{
    class ThreadedWorkerTestImpl : Niawa.Utilities.ThreadedWorker
    {
        public ThreadedWorkerTestImpl(string description)
            : base(description)
        {

        }

        /// <summary>
        /// Initialize thread
        /// </summary>
        public override void InitializeImpl()
        {

            Console.WriteLine(DateTime.Now.ToString() + " Initializing ThreadedWorkerTest");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine(DateTime.Now.ToString() + " Initialized ThreadedWorkerTest");
        }

        /// <summary>
        /// Finalize thread
        /// </summary>
        public override void FinalizeImpl()
        {
            Console.WriteLine(DateTime.Now.ToString() + " Finalizing ThreadedWorkerTest");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine(DateTime.Now.ToString() + " Finalized ThreadedWorkerTest");
        }

        /// <summary>
        /// Thread implementation
        /// </summary>
        public override void ThreadImpl()
        {
            Console.WriteLine(DateTime.Now.ToString() + " ThreadedWorkerTest thread implementation doing work");
            System.Threading.Thread.Sleep(500);

        }
    }
}
