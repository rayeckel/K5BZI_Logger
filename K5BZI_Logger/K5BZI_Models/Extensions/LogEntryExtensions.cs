using System;

namespace K5BZI_Models.Extensions
{
    public static class LogEntryExtensions
    {
        public static void ClearProperties(this LogEntry logEntry)
        {
            var entryId = logEntry.Id;

            logEntry.Id = entryId + 1;
            logEntry.CallSign = string.Empty;
            logEntry.ContactTime = DateTime.Now;
            logEntry.SignalReport.Sent = 599;
            logEntry.SignalReport.Received = 599;
        }

        public static LogEntry Clone(this LogEntry logEntry)
        {
            var clone = new LogEntry
            {
                Id = logEntry.Id,
                CallSign = logEntry.CallSign,
                ContactTime = logEntry.ContactTime,
                EventId = logEntry.EventId
            };

            clone.Signal.Band = logEntry.Signal.Band;
            clone.Signal.Frequency = logEntry.Signal.Frequency;
            clone.SignalReport.Sent = logEntry.SignalReport.Sent;
            clone.SignalReport.Received = logEntry.SignalReport.Received;

            return clone;
        }
    }
}
