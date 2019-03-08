using System;

namespace K5BZI_Models.Extensions
{
    public static class LogEntryExtensions
    {
        public static void ClearProperties(this LogEntry logEntry)
        {
            logEntry.CallSign = String.Empty;
            logEntry.ContactTime = null;
            logEntry.Event = null;
            logEntry.Signal = null;
            logEntry.SignalReport = null;
        }
    }
}
