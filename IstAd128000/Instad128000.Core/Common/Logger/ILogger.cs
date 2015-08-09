using System;

namespace Instad128000.Core.Common.Logger
{
    public interface ILogger
    {
        void Info(string message);

        void Warning(string message, Exception e = null);

        void Error(string message, Exception e = null);
 
    }
}