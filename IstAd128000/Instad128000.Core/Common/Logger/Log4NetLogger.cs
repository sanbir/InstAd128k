using System;
using log4net;

namespace Instad128000.Core.Common.Logger
{
    public class Log4NetLogger : ILogger
    {
        private ILog instance;
        public Log4NetLogger(string logName = "default")
        {
            this.instance = LogManager.GetLogger(logName);
            log4net.Config.XmlConfigurator.Configure();
        }

        public void Info(string message)
        {
            this.instance.Info(message);
        }

        public void Warning(string message, Exception e = null)
        {
            Console.WriteLine(message);
            this.instance.Warn(message, e);
        }

        public void Error(string message, Exception e = null)
        {
            this.instance.Error(message, e);
        }
    }
}
