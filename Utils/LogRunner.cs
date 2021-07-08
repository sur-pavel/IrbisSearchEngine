using NLog;
using System;
using System.IO;
using System.Reflection;

namespace IrbisSearchEngine.Utils
{
    public class LogRunner
    {
        public Logger Logger { get; }

        public LogRunner()
        {
            var config = new NLog.Config.LoggingConfiguration();
            string fileName = "IrbisSearchEngine_" + DateTime.Now.ToString().Replace(":", "-") + ".log";
            string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = appPath + @"\" + fileName };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            LogManager.Configuration = config;
            Logger = LogManager.GetCurrentClassLogger();
        }
    }
}