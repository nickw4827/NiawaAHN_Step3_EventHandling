using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public class NiawaLogMessage
    {
        public NiawaLogMessage(DateTime logDate
            , NiawaLogType logType
            , string logMessage
            , Exception logException)
        {
            LogDate = logDate;
            LogType = logType;
            LogMessage = logMessage;
            LogException = logException;
        }

        public DateTime LogDate { get; set; }
        public NiawaLogType LogType { get; set; }
        public string LogMessage { get; set; }
        public Exception LogException { get; set; }
    }
}
