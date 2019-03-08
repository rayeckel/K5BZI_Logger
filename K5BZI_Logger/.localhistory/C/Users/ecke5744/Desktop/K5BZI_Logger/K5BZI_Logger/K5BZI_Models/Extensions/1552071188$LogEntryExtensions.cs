using System;

namespace K5BZI_Models.Extensions
{
    public static class LogEntryExtensions
    {
        public static void ClearProperties(this LogEntry logEntry)
        {
            logEntry.CallSign = string.Empty;
            logEntry.ContactTime = DateTime.Now;
        }
    }
}
