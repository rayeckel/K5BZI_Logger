using System;

namespace K5BZI_Models.Extensions
{
    public static class LogEntryExtensions
    {
        public static void ClearProperties(this LogEntry logEntry)
        {
            logEntry.CallSign = string.Empty;
            logEntry.ContactTime = DateTime.Now;
            logEntry.SignalReport.Sent = 599;
            logEntry.SignalReport.Received = 599;
        }

        public static LogEntry Clone(this LogEntry logEntry)
        {
            var clone = new LogEntry();

            clone.CallSign = logEntry.CallSign;
            clone.ContactTime = logEntry.ContactTime;
            clone.Event.EventName = logEntry.Event.EventName;
            clone.Signal.Band = logEntry.Signal.Band;
            clone.Signal.Frequency = logEntry.Signal.Frequency;
            clone.SignalReport.Sent = logEntry.SignalReport.Sent;
            clone.SignalReport.Received = logEntry.SignalReport.Received;

            return clone;
        }
    }
}
