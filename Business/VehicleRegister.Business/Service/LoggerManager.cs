using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.Interfaces.Logger.Interface;

namespace VehicleRegister.Business.Service
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILogger logger;

        public LoggerManager()
        {
           logger = LogManager.GetLogger(GetType().ToString());
        }


        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
