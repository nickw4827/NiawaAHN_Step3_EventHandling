using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Niawa.Utilities
{
    public abstract class ThreadedWorker
    {

        private SortedList<NiawaThreadState, DateTime> _stateHistory = new SortedList<NiawaThreadState, DateTime>();
        private Thread t;
        private DateTime _stateDate = DateTime.MinValue;
        private NiawaThreadState _state = NiawaThreadState.UNKNOWN;

        //description
        public string Description { get; set; }

        //state date
        public DateTime StateDate
        {
            get { return _stateDate; }
        }

        //state
        public NiawaThreadState State
        {
            get
            {
                return _state;
            }
            private set
            {
                _state = value;
                _stateDate = DateTime.Now;
                UpdateStateHistory(_state, _stateDate);
            }
        }

        /// <summary>
        /// Instantiate ThreadedWorker
        /// </summary>
        public ThreadedWorker(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Initialize ThreadedWorker
        /// </summary>
        public void Initialize()
        {
            State = NiawaThreadState.INITIALIZING;

            //concrete initialize
            InitializeImpl();

            State = NiawaThreadState.INITIALIZED;

        }

        /// <summary>
        /// Concrete class implementation of Initialize
        /// </summary>
        public abstract void InitializeImpl();

        /// <summary>
        /// Start ThreadedWorker
        /// </summary>
        public void Start()
        {

            //validate state then start
            if (State == NiawaThreadState.INITIALIZED || State == NiawaThreadState.STOPPED || State == NiawaThreadState.UNKNOWN)
            {
                //initialize first if unknown
                if (State == NiawaThreadState.UNKNOWN)
                    Initialize();

                //start thread
                t = new System.Threading.Thread(ThreadStart);
                t.Start();

            }
            else
                throw new System.Exception("Cannot start thread while state is [" + State.ToString() + "]");

        }

        /// <summary>
        /// Suspend ThreadedWorker
        /// </summary>
        public void Suspend()
        {

            //validate state then suspend
            if (State == NiawaThreadState.STARTED)
                State = NiawaThreadState.SUSPENDED;
            else
                throw new System.Exception("Cannot suspend thread while state is [" + State.ToString() + "]");
        }

        /// <summary>
        /// Resume ThreadedWorker
        /// </summary>
        public void Resume()
        {

            //validate state then resume
            if (State == NiawaThreadState.SUSPENDED)
                State = NiawaThreadState.STARTED;
            else
                throw new System.Exception("Cannot resume thread while state is [" + State.ToString() + "]");

        }

        /// <summary>
        /// Stop ThreadedWorker
        /// </summary>
        public void Stop()
        {

            //validate state then stop
            if (State == NiawaThreadState.STARTED || State == NiawaThreadState.SUSPENDED)
                State = NiawaThreadState.STOPPING;
            else
                throw new System.Exception("Cannot stop thread while state is [" + State.ToString() + "]");

        }

        /// <summary>
        /// Finalize ThreadedWorker
        /// </summary>
        public void FinalizeWorker()
        {
            if (State == NiawaThreadState.FINALIZED)
                throw new System.Exception("Cannot finalize thread while state is [" + State.ToString() + "]");

            State = NiawaThreadState.FINALIZING;

            //concrete finalize
            FinalizeImpl();

            State = NiawaThreadState.FINALIZED;

        }

        /// <summary>
        /// Concrete class implementation of Finalize
        /// </summary>
        public abstract void FinalizeImpl();


        /// <summary>
        /// Started by the thread object 
        /// </summary>
        /// <param name="data"></param>
        private void ThreadStart(object data)
        {
            State = NiawaThreadState.STARTED;

            do
            {
                if (State == NiawaThreadState.SUSPENDED)
                {
                    Thread.Sleep(25);
                    continue;
                }

                //concrete thread implementation
                ThreadImpl();

            } while (State == NiawaThreadState.STARTED || State == NiawaThreadState.SUSPENDED);

            State = NiawaThreadState.STOPPED;

        }

        /// <summary>
        /// Concrete class implementation of Thread.  Called by ThreadStart, which is started by the thread.
        /// </summary>
        public abstract void ThreadImpl();

        /// <summary>
        /// Update state history with a specified state and date
        /// </summary>
        /// <param name="state"></param>
        private void UpdateStateHistory(NiawaThreadState state, DateTime stateDate)
        {
            if (_stateHistory.ContainsKey(state))
                //update if exists
                _stateHistory[state] = stateDate;
            else
                //add if new
                _stateHistory.Add(state, stateDate);

        }

        /// <summary>
        /// Return the latest date for a specified state (or null if the state has not been recorded).
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public DateTime? GetStateHistory(NiawaThreadState state)
        {
            if (_stateHistory.ContainsKey(state))
                return _stateHistory[state];
            else
                return null;

        }



    }
}
