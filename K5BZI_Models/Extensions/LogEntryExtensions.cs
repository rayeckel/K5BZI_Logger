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
            logEntry.SignalReport.Sent = "599";
            logEntry.SignalReport.Received = "599";

            if(!contactTimeEnabled)
                logEntry.ContactTime = DateTime.Now;
        }

        public static LogEntry Clone(this LogEntry logEntry)
        {
            var clone = new LogEntry
            {
                Id = logEntry.Id,
                CallSign = logEntry.CallSign,
                Prefix = logEntry.Prefix,
                Continent = logEntry.Continent,
                Country = logEntry.Country,
                QslSent = logEntry.QslSent,
                QslReceived = logEntry.QslReceived,
                Assisted = logEntry.Assisted,
                Power = logEntry.Power,
                Operator = logEntry.Operator,
                ContactTime = logEntry.ContactTime,
                EventId = logEntry.EventId
            };

            clone.Signal.Band = logEntry.Signal.Band;
            clone.Signal.Mode = logEntry.Signal.Mode;
            clone.Signal.Frequency = logEntry.Signal.Frequency;
            clone.SignalReport.Sent = logEntry.SignalReport.Sent;
            clone.SignalReport.Received = logEntry.SignalReport.Received;

            return clone;
        }
    }
}
