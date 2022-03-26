using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Core;

namespace OcrRoid.Util
{
    internal class Log
    {
        private static readonly log4net.ILog _logger = LogManager.GetLogger(Assembly.GetExecutingAssembly().FullName);


        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Error(string message)
        {
            _logger.Error(message);
        }

        public static void Warn(string message)
        {
            _logger.Warn(message);
        }
    }
}
