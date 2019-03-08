using System;

namespace K5BZI_Models.Extensions
{
    public static class LogEntryExtensions
    {
        public static void ClearProperties(this LogEntry logEntry)
        {
            logEntry.CallSign = string.Empty;
            logEntry.ContactTime = DateTime.Now;
            logEntry.Event.EventName = string.Empty;
            logEntry.Signal.Band = string.Empty;
            logEntry.Signal.Frequency = 0;
            logEntry.SignalReport.Sent = 0;
            logEntry.SignalReport.Received = 0;
        }
    }
}
