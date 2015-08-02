using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.UtilitiesTestClient
{
    class BufferedLoggerTestClass
    {
        string _logPath = string.Empty;

        public BufferedLoggerTestClass(string logPath)
        {
            _logPath = logPath;
        }

        public void RunTest()
        {
            string myDesc = this.GetType().Name.ToString();

            //create log writers
            Niawa.Utilities.Log.ILogWriter consoleWriter = new Niawa.Utilities.Log.ConsoleLogWriter();
            Niawa.Utilities.Log.ILogWriter textFileWriter = new Niawa.Utilities.Log.TextFileLogWriter(_logPath);

            //create logger
            Niawa.Utilities.Log.BufferedLogger logger = new Niawa.Utilities.Log.BufferedLogger(myDesc);

            //add log writers to logger
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.INFO, consoleWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.WARN, consoleWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.ERROR, consoleWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.SEVERE, consoleWriter);

            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.WARN, textFileWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.ERROR, textFileWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.SEVERE, textFileWriter);

            logger.Start();

            logger.Info("test info message");
            logger.Log(Utilities.NiawaLogType.INFO, "test info message 2");
            logger.Warn("test warn message");
            logger.Error("test error message");
            logger.Severe("test severe message");

            try
            {
                throw new Exception("test throw an exception");
            }
            catch (Exception ex)
            {
                logger.Error("test error message w/exception", ex);
            }


            System.Threading.Thread.Sleep(100);
            logger.Stop();

        }


    }
}
