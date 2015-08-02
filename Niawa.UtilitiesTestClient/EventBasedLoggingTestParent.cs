using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.UtilitiesTestClient
{
    class EventBasedLoggingTestParent
    {
        string _logPath = string.Empty;

        public EventBasedLoggingTestParent(string logPath)
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

            //add log listener
            Niawa.Utilities.Log.ILogListener eventBasedLogListener = new Niawa.Utilities.Log.EventBasedLogListener(logger);

            //add log writers to logger
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.INFO, consoleWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.WARN, consoleWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.ERROR, consoleWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.SEVERE, consoleWriter);

            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.WARN, textFileWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.ERROR, textFileWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.SEVERE, textFileWriter);

            logger.Start();

            logger.Info("Parent class started up");
            
            //create the test child
            EventBasedLoggingTestChild tc = new EventBasedLoggingTestChild();

            //bind logging to the test child
            tc.BindLogEvents(eventBasedLogListener);

            //do something that causes test child to log
            logger.Info("Starting child class");
            tc.StartTestClass();

            logger.Info("Invoking child to do some logging");
            tc.DoSomeTestsWithLogging();
            System.Threading.Thread.Sleep(100);

            logger.Info("Stopping child class");
            tc.StopTestClass();

            //unbind logging
            tc.UnbindLogEvents();

            //shut down
            System.Threading.Thread.Sleep(100);
            logger.Stop();

        }

    }
}
