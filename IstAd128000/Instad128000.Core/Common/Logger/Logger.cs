namespace Instad128000.Core.Common.Logger
{
    public class Logger
    {
        private static ILogger _commonLogger;

        public static ILogger Current
        {
            get
            {
                return _commonLogger ?? (_commonLogger = new Log4NetLogger());
            }
        }
    }
}
