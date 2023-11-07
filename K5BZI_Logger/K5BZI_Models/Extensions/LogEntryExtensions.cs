﻿using System;

namespace K5BZI_Models.Extensions
{
    public static class LogEntryExtensions
    {
        public static void ClearProperties(this LogEntry logEntry, bool contactTimeEnabled)
        {
            logEntry.CallSign = string.Empty;
            logEntry.SignalReport.Sent = "599";
            logEntry.SignalReport.Received = "599";
            logEntry.CQZone = String.Empty;
            logEntry.Notes = String.Empty;

            if (!contactTimeEnabled)
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
                EventId = logEntry.EventId,
                CQZone = logEntry.CQZone,
                Notes = logEntry.Notes,
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
