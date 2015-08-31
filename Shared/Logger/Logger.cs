using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Common;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository;

namespace Shared.Logger
{
    public class Logger
    {
        private static ILog _log = null;
        public static ILog Log
        {
            get
            {
                if (_log == null)
                {
                    //InitFolderNames();
                    _log = LogManager.GetLogger("TTMK4");
                }

                return _log;
            }
        }

        private static void InitFolderNames()
        {
            string logFolder = ApplicationPaths.Log;

            ILoggerRepository repository = log4net.LogManager.GetRepository();
            IAppender[] appenders = repository.GetAppenders();

            foreach (IAppender appender in appenders)
            {
                if (appender is RollingFileAppender)
                {
                    RollingFileAppender rollingAppender = (RollingFileAppender)appender;

                    string[] names = rollingAppender.File.Split('\\', '/');
                    rollingAppender.File = logFolder + "\\" + names.Last();

                    rollingAppender.ActivateOptions();
                }
            }
        }

        // --------------------
        public static void Info(object msg)
        {
            Log.Info(msg);
        }

        public static void Info(object msg, Exception ex)
        {
            Log.Info(msg, ex);
        }

        public static void InfoFormat(string msg, params object[] args)
        {
            Log.InfoFormat(msg, args);
        }

        // --------------------
        public static void Error(object msg)
        {
            Log.Error(msg);
        }

        public static void Error(object msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        public static void ErrorFormat(string msg, params object[] args)
        {
            Log.ErrorFormat(msg, args);
        }

        // --------------------
        public static void Warn(object msg)
        {
            Log.Warn(msg);
        }

        public static void Warn(object msg, Exception ex)
        {
            Log.Warn(msg, ex);
        }

        public static void WarnFormat(string msg, params object[] args)
        {
            Log.WarnFormat(msg, args);
        }

    }
}
