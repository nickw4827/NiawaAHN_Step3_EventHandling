using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public class BufferedLogger : ThreadedWorker, ILogger
    {

        private Queue<NiawaLogMessage> _messages = null;
        private SortedList<string, ILogWriter> _logWriters = null;
        private SortedList<NiawaLogType, List<ILogWriter>> _logMessageTypeLogWriters = null;
        private SortedList<Guid, ILogListener> _logListeners = null;

        /// <summary>
        /// Instantiate BufferedLogger
        /// </summary>
        /// <param name="description"></param>
        public BufferedLogger(string description)
            : base(description)
        {
            _messages = new Queue<NiawaLogMessage>();
            _logWriters = new SortedList<string, ILogWriter>();
            _logMessageTypeLogWriters = new SortedList<NiawaLogType, List<ILogWriter>>();
            _logListeners = new SortedList<Guid, ILogListener>();
    
        }

        /// <summary>
        /// Initialize implementation for threaded worker
        /// </summary>
        public override void InitializeImpl()
        {
            foreach (KeyValuePair<string, ILogWriter> logWriter in _logWriters)
            {
                logWriter.Value.InitializeWriter();
            }
        }

        /// <summary>
        /// Finalize implementation for threaded worker
        /// </summary>
        public override void FinalizeImpl()
        {
            foreach (KeyValuePair<string, ILogWriter> logWriter in _logWriters)
            {
                logWriter.Value.FinalizeWriter();
            }
        }

        /// <summary>
        /// ThreadStart implementation for threaded worker
        /// </summary>
        public override void ThreadImpl()
        {
            while (_messages.Count > 0)
            {
                //don't process messages if thread is not running
                if (!(this.State == NiawaThreadState.STARTED))
                    break;

                NiawaLogMessage message = _messages.Dequeue();

                //check if log message type is mapped
                if (_logMessageTypeLogWriters.ContainsKey(message.LogType))
                {

                    //get log writers mapped to this log message type
                    List<ILogWriter> logWriters = _logMessageTypeLogWriters[message.LogType];

                    foreach (ILogWriter logWriter in logWriters)
                    {
                        //write message to log writer
                        logWriter.WriteLogMessage(message);
                    }
                }

            }

            //sleep before returning to thread loop
            System.Threading.Thread.Sleep(25);

        }

        /// <summary>
        /// Adds a log writer and binds to a specific log message type
        /// </summary>
        /// <param name="logMessageType">Type of log message</param>
        /// <param name="logWriter">Log writer to add</param>
        public void AddLogWriter(NiawaLogType logMessageType, ILogWriter logWriter)
        {

            //Ensure that more than one instance of a ILogWriter implementation (of a specific type) is not added.
            if (_logWriters.ContainsKey(logWriter.GetType().ToString()))
                if (_logWriters[logWriter.GetType().ToString()].LogWriterID != logWriter.LogWriterID)
                    throw new Exception("Cannot add two instances of the same ILogWriter type [" + logWriter.GetType().ToString() + "].  Add a different type of ILogWriter, or add an existing instance of this ILogWriter type.");

            //Check for duplicate entry attempts into the LogMessageTypeLogWriters collection
            if (_logMessageTypeLogWriters.ContainsKey(logMessageType))
                if (_logMessageTypeLogWriters[logMessageType].Contains(logWriter))
                    throw new Exception("Cannot add the same ILogWriter [" + logWriter.GetType().ToString() + "] for LogMessageType [" + Enum.GetName(logMessageType.GetType(), logMessageType) + "] more than once.");

            //add log writer to list
            if (!(_logWriters.ContainsKey(logWriter.GetType().ToString())))
                _logWriters.Add(logWriter.GetType().ToString(), logWriter);

            //add log writer to map
            List<ILogWriter> mapLogWriters = null;

            //check if log message type already in map
            if (_logMessageTypeLogWriters.ContainsKey(logMessageType))
                mapLogWriters = _logMessageTypeLogWriters[logMessageType];
            else
            {
                //create new entry of log writers for log message type and add to map
                mapLogWriters = new List<ILogWriter>();
                _logMessageTypeLogWriters.Add(logMessageType, mapLogWriters);
            }

            //add log writer to list of log writers mapped to log message type
            mapLogWriters.Add(logWriter);

        }

        /// <summary>
        /// Adds a log listener to the logger.
        /// Log listeners receive log messages from other sources, then 
        /// pass them on to their registered logger.
        /// </summary>
        /// <param name="logListener"></param>
        public void AddLogListener(ILogListener logListener)
        {
            //add log writer to list
            if (!(_logListeners.ContainsKey(logListener.LogListenerID)))
                _logListeners.Add(logListener.LogListenerID, logListener);
        }

        /// <summary>
        /// Add log message to be written asynchronously
        /// </summary>
        /// <param name="logType">Log message type</param>
        /// <param name="logMessage">Log message text</param>
        /// <param name="logException">Log exception, if any</param>
        public void Log(NiawaLogType logType, string logMessage, Exception logException)
        {
            //enqueue only if at least one log writer is mapped for this log message type
            if (_logMessageTypeLogWriters.ContainsKey(logType))
                _messages.Enqueue(new NiawaLogMessage(DateTime.Now, logType, logMessage, logException));
        }

        /// <summary>
        /// Add log message to be written asynchronously
        /// </summary>
        /// <param name="message">Log message to be written</param>
        public void Log(NiawaLogMessage message)
        {
            if (_logMessageTypeLogWriters.ContainsKey(message.LogType))
                _messages.Enqueue(message);
        }

        public void Log(NiawaLogType logType, string logMessage)
        {
            Log(logType, logMessage, null);
        }

        public void Debug(string logMessage)
        {
            Log(NiawaLogType.DEBUG, logMessage, null);
        }

        public void Info(string logMessage)
        {
            Log(NiawaLogType.INFO, logMessage, null);
        }

        public void Warn(string logMessage)
        {
            Log(NiawaLogType.WARN, logMessage, null);
        }

        public void Error(string logMessage)
        {
            Log(NiawaLogType.ERROR, logMessage, null);
        }

        public void Error(string logMessage, Exception logException)
        {
            Log(NiawaLogType.ERROR, logMessage, logException);
        }

        public void Severe(string logMessage)
        {
            Log(NiawaLogType.SEVERE, logMessage, null);
        }

        public void Severe(string logMessage, Exception logException)
        {
            Log(NiawaLogType.SEVERE, logMessage, logException);
        }

    }
}
