using AOPLogger.AspectCore.Domain;
using NLog;

namespace AOPLogger.AspectCore.Service
{
    public class NLogWriteService : INLogWriteService
    {
        private Logger logger = LogManager.GetLogger("MatchesLog");

        private const string NO_SEQUENCE = "----";
        private const string NO_SOURCE = "-";
        private const string STATS_SOURCE = "S";
        private const string EVENT_SOURCE = "E";
        private const string MATCH_SOURCE = "M";

        private const int NAME_WIDTH = 40;

        public void EventInfoLog(string providerMatchId, long sequence, string className,
            string message)
        {
            WriteLog(providerMatchId, sequence, className, message, STATS_SOURCE, LogLevel.Info);
        }
        public void EventErrorLog(string providerMatchId, long sequence, string className,
            string message)
        {
            WriteLog(providerMatchId, sequence, className, message, STATS_SOURCE, LogLevel.Error);
        }
        public void StateInfoLog(string providerMatchId, long sequence, string className,
            string message)
        {
            WriteLog(providerMatchId, sequence, className, message, STATS_SOURCE, LogLevel.Error);
        }
        public void StateErrorLog(string providerMatchId, long sequence, string className,
            string message)
        {
            WriteLog(providerMatchId, sequence, className, message, STATS_SOURCE, LogLevel.Error);
        }
        public void MatchInfoLog(string providerMatchId, string className,
            string message)
        {
            WriteLog(providerMatchId, NO_SEQUENCE, className, message, MATCH_SOURCE, LogLevel.Info);
        }
        public void MatchErrorLog(string providerMatchId, string className,
            string message)
        {
            WriteLog(providerMatchId, NO_SEQUENCE, className, message, MATCH_SOURCE, LogLevel.Error);
        }

        private void WriteLog(string providerMatchId, long sequence, string className,
            string message, string source, LogLevel logLevel)
        {
            var logEventInfo = LogEventInfo.Create(logLevel, logger.Name, message);

            logEventInfo.Properties["ProviderMatchId"] = providerMatchId;

            logEventInfo.Properties["sequence"] = $"{sequence:d4}";

            logEventInfo.Properties["code"] = source;

            logEventInfo.Properties["classname"] = className.PadRight(NAME_WIDTH);

            logger.Log(logEventInfo);
        }
        private void WriteLog(string providerMatchId, string sequence, string className,
            string message, string source, LogLevel logLevel)
        {
            var logEventInfo = LogEventInfo.Create(logLevel, logger.Name, message);

            logEventInfo.Properties["ProviderMatchId"] = providerMatchId;

            logEventInfo.Properties["sequence"] = sequence;

            logEventInfo.Properties["code"] = source;

            logEventInfo.Properties["classname"] = className.PadRight(NAME_WIDTH);

            logger.Log(logEventInfo);
        }
    }
}