using System;

namespace K5BZI_Models.Extensions
{
    public static class LogEntryExtensions
    {
        public static void ClearProperties(this LogEntry logEntry, bool contactTimeEnabled)
        {
            var entryId = logEntry.Id;

            logEntry.Id = entryId + 1;
            logEntry.CallSign = string.Empty;

            if(!contactTimeEnabled)
                logEntry.ContactTime = DateTime.Now;
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
