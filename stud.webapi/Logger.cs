using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;

namespace stud.webapi
{
    public static class Logger
    {
        private static ILog log = LogManager.GetLogger("LOGGER");


        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }
    }
}