using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using System;

namespace GherkinWebAPI.Utilities
{
    public static class ExceptonLog
    {
        public static void LogError(Exception ex, string fileName, string methodName)
        {
            Logger Logger = LogManager.GetCurrentClassLogger();
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Error, ex.Source, ex.Message);
            theEvent.Properties["methodname"] = methodName;
            theEvent.Properties["filename"] = fileName;
            theEvent.Exception = ex;
            Logger.Log(theEvent);
        }
    }
}