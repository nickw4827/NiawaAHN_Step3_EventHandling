using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public interface ILogListener
    {
        Guid LogListenerID { get; }
        void ReceiveLogMessage(object o, NiawaLogMessageEventArgs e);
        
    }
}
