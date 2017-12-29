using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkFlowEngine.DBUtility
{
    public class Logger
    {
        public static log4net.ILog Log;

        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
            Log = log4net.LogManager.GetLogger("");
            
        }

        //public Logger()
        //{
        //    log4net.Config.XmlConfigurator.Configure();
        //    Log = log4net.LogManager.GetLogger("WorkFlowEngineLog");
        //}

    }
}
