using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public interface ILogger
    {
        void Log(NiawaLogType logMessageType, string logMessage);
        void Log(NiawaLogType logMessageType, string logMessage, Exception logException);
        void Log(NiawaLogMessage message);
        void Debug(string logMessage);
        void Info(string logMessage);
        void Warn(string logMessage);
        void Error(string logMessage);
        void Error(string logMessage, Exception logException);
        void Severe(string logMessage);
        void Severe(string logMessage, Exception logException);
        void AddLogWriter(NiawaLogType logMessageType, ILogWriter logWriter);
        void AddLogListener(ILogListener logListener);
    }
}
