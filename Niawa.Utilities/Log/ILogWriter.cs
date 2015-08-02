using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public interface ILogWriter
    {
        Guid LogWriterID { get; }
        void WriteLogMessage(NiawaLogMessage message);
        void InitializeWriter();
        void FinalizeWriter();
    }
}
