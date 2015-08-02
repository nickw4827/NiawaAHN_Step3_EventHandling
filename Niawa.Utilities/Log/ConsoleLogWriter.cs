using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public class ConsoleLogWriter : ILogWriter
    {

        /// <summary>
        /// LogWriterID property
        /// </summary>
        public Guid LogWriterID
        {
            get;
            private set;
        }

        /// <summary>
        /// Instantiate ConsoleLogWriter
        /// </summary>
        public ConsoleLogWriter()
        {
            LogWriterID = new Guid();
        }

        /// <summary>
        /// Write a message to the console
        /// </summary>
        /// <param name="message"></param>
        public void WriteLogMessage(NiawaLogMessage message)
        {

            //build log message
            StringBuilder sb = new StringBuilder();

            //log date
            sb.Append(message.LogDate.ToString());
            //log type
            sb.Append(" ");
            sb.Append(Enum.GetName(message.LogType.GetType(), message.LogType));
            //log message
            sb.Append(" ");
            sb.Append(message.LogMessage);
            //log error message
            if (!(message.LogException == null))
            {
                sb.Append(": ");
                sb.Append(message.LogException.Message);
                sb.Append(message.LogException.StackTrace);
            }

            //output log message to console
            Console.WriteLine(sb);
        }

        public void InitializeWriter()
        {

        }

        public void FinalizeWriter()
        {

        }

    }
}
