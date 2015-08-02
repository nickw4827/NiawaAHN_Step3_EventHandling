using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public class NiawaLogMessageEventArgs : EventArgs
    {
        public Guid ID { get; private set; }
        public NiawaLogMessage LogMessage { get; private set; }

        /// <summary>
        /// Instantiate NiawaLogMessageEventArgs with explicit ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="logMessage"></param>
        public NiawaLogMessageEventArgs(Guid id, NiawaLogMessage logMessage)
        {
            ID = id;
            LogMessage = logMessage;
        }

        /// <summary>
        /// Instantiate NiawaLogMessageEventArgs and ID is auto-generated
        /// </summary>
        /// <param name="logMessage"></param>
        public NiawaLogMessageEventArgs(NiawaLogMessage logMessage)
        {
            ID = new Guid();
            LogMessage = logMessage;
        }
    }
}
