using K5BZI_Models;
using K5BZI_Models.Attributes;
using K5BZI_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K5BZI_Services
{
    public class ExportService : IExportService
    {
        private readonly IFileStoreService _fileStoreService;

        public ExportService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;
        }

        public void ExportLog(Event evntLog, ICollection<LogEntry> logEntries, LogType logType)
        {
            switch(logType)
            {
                case LogType.Adif:
                    ExportToAdif(evntLog, logEntries);
                    break;
            }
        }

        private async void ExportToAdif(Event eventLog, ICollection<LogEntry> logEntries)
        {
            var header = String.Format(
                "ADIF Export from K5BZI Logger version {0} {1}Written by: Ray Eckel{2}Log exported on: {3}{4}<EOH>{5}{6}",
                "1.0", //Appsettings.Get("Version")
                Environment.NewLine,
                Environment.NewLine,
                DateTime.UtcNow.ToString("g"),
                Environment.NewLine,
                Environment.NewLine,
                Environment.NewLine);

            var adifData = new StringBuilder(header);

            var eventProperties = from p in eventLog.GetType().GetProperties()
                        let attr = p.GetCustomAttributes(typeof(AdifAttribute), true)
                        where attr.Length == 1
                        select new { Property = p, Attribute = attr.First() as AdifAttribute };

            foreach (var entry in logEntries)
            {
                foreach (var eventProp in eventProperties)
                {
                    var value = eventProp.Property.GetValue(eventLog) as string;

                    if (value == null)
                    {
                        continue;
                    }

                    var line = String.Format("<{0}:{1}>{2}", eventProp.Attribute.FieldName, value.Length, value);

                    adifData.AppendLine(line);
                };

                var entryProperties = from p in entry.GetType().GetProperties()
                                      let attr = p.GetCustomAttributes(typeof(AdifAttribute), true)
                                      where attr.Length == 1
                                      select new { Property = p, Attribute = attr.First() as AdifAttribute };

                foreach (var entryProp in entryProperties)
                {
                    var value = entryProp.Property.GetValue(entry) as string;

                    if (value == null)
                    {
                        continue;
                    }

                    var line = String.Format("<{0}:{1}>{2}", entryProp.Attribute.FieldName, value.Length, value);

                    adifData.AppendLine(line);
                };

                var signalProperties = from p in entry.Signal.GetType().GetProperties()
                                       let attr = p.GetCustomAttributes(typeof(AdifAttribute), true)
                                       where attr.Length == 1
                                       select new { Property = p, Attribute = attr.First() as AdifAttribute };

                foreach (var signalProp in signalProperties)
                {
                    var value = signalProp.Property.GetValue(entry.Signal) as string;

                    if (value == null)
                    {
                        continue;
                    }

                    var line = String.Format("<{0}:{1}>{2}", signalProp.Attribute.FieldName, value.Length, value);

                    adifData.AppendLine(line);
                };

                adifData.AppendLine(String.Format("<eor>{0}", Environment.NewLine));
            }

            await _fileStoreService.WriteToFile(adifData.ToString(), eventLog.LogFileName, FileExtensions.Adif);

            _fileStoreService.OpenLogDirectory();
        }
    }
}
