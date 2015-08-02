using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public delegate void NiawaLogMessageEventHandler(object o, NiawaLogMessageEventArgs e);

    public class EventBasedLogWriter : ILogWriter
    {

        //event declaration
        public event NiawaLogMessageEventHandler NiawaLogMessageEvent;

        //properties
        public Guid LogWriterID {  get; private set; }

        /// <summary>
        /// Instantiates EventBasedLogWriter
        /// </summary>
        public EventBasedLogWriter()
        {
            LogWriterID = new Guid();
        }

        /// <summary>
        /// Adds a listener to subscribe to any log events raised
        /// </summary>
        /// <param name="listener"></param>
        public void AddListener(ILogListener listener)
        {
            NiawaLogMessageEvent += new NiawaLogMessageEventHandler(listener.ReceiveLogMessage);
        }

        /// <summary>
        /// Removes listener from subscribing to log events
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveListener(ILogListener listener)
        {
            NiawaLogMessageEvent -= new NiawaLogMessageEventHandler(listener.ReceiveLogMessage);
        }

        /// <summary>
        /// Raises a log event to any added listeners.
        /// </summary>
        /// <param name="message"></param>
        public void WriteLogMessage(NiawaLogMessage message)
        {
            //raise event
            if (NiawaLogMessageEvent != null)
                NiawaLogMessageEvent(this, new NiawaLogMessageEventArgs(message));
        }

        public void InitializeWriter()
        {
        }

        public void FinalizeWriter()
        {
            NiawaLogMessageEvent = null;
        }
    }
}
