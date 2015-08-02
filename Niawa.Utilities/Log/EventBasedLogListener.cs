using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public class EventBasedLogListener : ILogListener
    {

        ILogger _logger = null;
        public Guid LogListenerID { get; private set; }

        public EventBasedLogListener(ILogger logger)
        {
            LogListenerID = new Guid();
            _logger = logger;
        }

        public void ReceiveLogMessage(object o, NiawaLogMessageEventArgs e)
        {
            //received a log message as a raised event from publisher
            //now pass the log message along to the local logger
            _logger.Log(e.LogMessage);
        }


    }
}
