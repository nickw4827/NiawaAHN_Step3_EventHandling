using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.Utilities.Log
{
    public class TextFileLogWriter : ILogWriter
    {
        System.IO.StreamWriter _stream = null;
        string _filename = string.Empty;

        public Guid LogWriterID { get; private set; }
        public bool IsStreamOpen { get; private set; }

        /// <summary>
        /// Instantiate TextFileLogWriter
        /// </summary>
        /// <param name="logPath">The path that log messages should be written to.  If blank, will write to the current working directory.</param>
        public TextFileLogWriter(string logPath)
        {
            LogWriterID = new Guid();
            string filename = logPath;

            //if path is empty, set to current directory
            if (filename.Trim().Length == 0)
                filename = System.Environment.CurrentDirectory;

            //add trailing backslash if missing
            if (!filename.EndsWith("\\"))
                filename = filename + "\\";

            //create filename
            filename = filename + "Log_" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            _filename = filename;

        }


        /// <summary>
        /// Write a message to the log file
        /// </summary>
        /// <param name="message"></param>
        public void WriteLogMessage(NiawaLogMessage message)
        {
            //open file stream
            _stream = new System.IO.StreamWriter(_filename, true);

            //build log message
            StringBuilder sb = new StringBuilder();

            //log date
            sb.Append(message.LogDate.ToString());
            //log type
            sb.Append("|");
            sb.Append(Enum.GetName(message.LogType.GetType(), message.LogType));
            //log message
            sb.Append("|");
            sb.Append(message.LogMessage);
            //log error message
            if (!(message.LogException == null))
            {
                sb.Append("|");
                sb.Append(message.LogException.Message);
                sb.Append(message.LogException.StackTrace);
            }

            //output log message to text file
            _stream.WriteLine(sb);
            IsStreamOpen = true;

            //TODO optimize by moving this to end of string of log messages being written
            CloseFile();

        }

        /// <summary>
        /// Close open text file
        /// </summary>
        public void CloseFile()
        {
            _stream.Flush();
            _stream.Close();
            IsStreamOpen = false;
        }

        public void InitializeWriter()
        {
        }

        public void FinalizeWriter()
        {
            CloseFile();
        }
    }
}
