namespace AOPLogger.AspectCore.Domain
{
    public interface INLogWriteService
    {
        void EventInfoLog(string MatchId, long sequence, string className,
            string message);

        void EventErrorLog(string MatchId, long sequence, string className,
            string message);

        void StateInfoLog(string MatchId, long sequence, string className,
            string message);

        void StateErrorLog(string MatchId, long sequence, string className,
            string message);

        void MatchInfoLog(string MatchId, string className,
            string message);

        void MatchErrorLog(string MatchId, string className,
            string message);
    }
}