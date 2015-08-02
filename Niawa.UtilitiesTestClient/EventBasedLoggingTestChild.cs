using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niawa.UtilitiesTestClient
{
    class EventBasedLoggingTestChild : Niawa.Utilities.Log.ILogBinder 
    {

        Niawa.Utilities.Log.BufferedLogger logger = null;
        Niawa.Utilities.Log.EventBasedLogWriter eventLogWriter = null;
        Utilities.Log.ILogListener eventLogListener = null;

        /// <summary>
        /// Instantiates EventBasedLoggingTestChild
        /// </summary>
        public EventBasedLoggingTestChild()
        {
            string myDesc = this.GetType().Name.ToString();

            //create evenet-based log writer
            eventLogWriter = new Niawa.Utilities.Log.EventBasedLogWriter();

            //create logger
            logger = new Niawa.Utilities.Log.BufferedLogger(myDesc);

            //add log writer to logger
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.INFO, eventLogWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.WARN, eventLogWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.ERROR, eventLogWriter);
            logger.AddLogWriter(Niawa.Utilities.NiawaLogType.SEVERE, eventLogWriter);

        }

        /// <summary>
        /// Bind listener to log events raised in this class
        /// </summary>
        /// <param name="listener"></param>
        public void BindLogEvents(Utilities.Log.ILogListener listener)
        {
            eventLogWriter.AddListener(listener);
            eventLogListener = listener;
        }

        /// <summary>
        /// Unbind listener from log events raised in this class
        /// </summary>
        public void UnbindLogEvents()
        {
            if(eventLogListener != null)
                eventLogWriter.RemoveListener(eventLogListener);
        }

        public void DoSomeTestsWithLogging()
        {
            logger.Info("[" + this.GetType().Name + "] ** log 1 via event **");
            logger.Warn("[" + this.GetType().Name + "] ** log 2 via event **");
            logger.Error("[" + this.GetType().Name + "] ** log 3 via event **");
            logger.Severe("[" + this.GetType().Name + "] ** log 4 via event **");
        }

        public void StartTestClass()
        {
            logger.Start();
        }

        public void StopTestClass()
        {
            logger.Stop();
        }


    }
}
