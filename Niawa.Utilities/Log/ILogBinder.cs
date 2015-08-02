using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public interface ILogBinder
    {
        void BindLogEvents(ILogListener listener);
        void UnbindLogEvents();
    }
}
