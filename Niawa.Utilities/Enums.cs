using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities
{
    public enum NiawaThreadState
    {
        UNKNOWN = 1,
        INITIALIZING = 2,
        INITIALIZED = 3,
        STARTING = 4,
        STARTED = 5,
        SUSPENDED = 6,
        STOPPING = 7,
        STOPPED = 8,
        FINALIZING = 9,
        FINALIZED = 10

    }

    //Enum.GetName(Type MyEnumType,  object enumvariable)  

    public enum NiawaLogType
    {
        DEBUG = 1,
        INFO = 2,
        WARN = 3,
        ERROR = 4,
        SEVERE = 5
    }

}
