using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using K5BZI_Models;
using K5BZI_Models.Attributes;
using K5BZI_Models.Enums;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services.Services
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
            switch (logType)
            {
                case LogType.ADIF:
                    ExportToAdif(evntLog, logEntries);
                    break;
                case LogType.CABRILLO:
                    ExportToCabrillo(evntLog, logEntries);
                    break;
            }
        }

        private async void ExportToAdif(Event currentEvent, ICollection<LogEntry> logEntries)
        {
            var header = String.Format(
                "ADIF Export from K5BZI Logger version {0} {1}Copyright (C) 2020 - Ray Eckel K5BZI{2}Log exported on: {3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}",
                "1.0", //Appsettings.Get("Version")
                Environment.NewLine,
                Environment.NewLine,
                DateTime.UtcNow.ToString("g"),
                Environment.NewLine,
                Environment.NewLine,
                "<ADIF_VER:5>2.2.1",
                Environment.NewLine,
                "<PROGRAMID:6>K5BZI Logger",
                Environment.NewLine,
                "<PROGRAMVERSION:11>1.0",
                Environment.NewLine,
                "<EOH>",
                Environment.NewLine);

            var adifData = new StringBuilder(header);

            var eventProperties = from p in currentEvent.GetType().GetProperties()
                                  let attr = p.GetCustomAttributes(typeof(AdifAttribute), true)
                                  where attr.Length == 1
                                  select new { Property = p, Attribute = attr.First() as AdifAttribute };

            foreach (var entry in logEntries)
            {
                var recordLine = String.Empty;

                foreach (var eventProp in eventProperties)
                {
                    var value = eventProp.Property.GetValue(currentEvent)?.ToString();

                    if (value == null)
                    {
                        continue;
                    }

                    var line = String.Format("<{0}:{1}>{2}", eventProp.Attribute.PropertyName, value.Length, value);

                    recordLine += line;
                };

                var entryProperties = from p in entry.GetType().GetProperties()
                                      let attr = p.GetCustomAttributes(typeof(AdifAttribute), true)
                                      where attr.Length == 1
                                      select new { Property = p, Attribute = attr.First() as AdifAttribute };

                foreach (var entryProp in entryProperties)
                {
                    var value = entryProp.Property.GetValue(entry)?.ToString();

                    if (value == null)
                    {
                        continue;
                    }

                    var line = String.Format("<{0}:{1}>{2}", entryProp.Attribute.PropertyName, value.Length, value);

                    recordLine += line;
                };

                var signalProperties = from p in entry.Signal.GetType().GetProperties()
                                       let attr = p.GetCustomAttributes(typeof(AdifAttribute), true)
                                       where attr.Length == 1
                                       select new { Property = p, Attribute = attr.First() as AdifAttribute };

                foreach (var signalProp in signalProperties)
                {
                    var value = signalProp.Property.GetValue(entry.Signal)?.ToString();

                    if (value == null)
                    {
                        continue;
                    }

                    var line = String.Format("<{0}:{1}>{2}", signalProp.Attribute.PropertyName, value.Length, value);

                    recordLine += line;
                };

                var signalReport = from p in entry.SignalReport.GetType().GetProperties()
                                   let attr = p.GetCustomAttributes(typeof(AdifAttribute), true)
                                   where attr.Length == 1
                                   select new { Property = p, Attribute = attr.First() as AdifAttribute };

                foreach (var signalRep in signalReport)
                {
                    var value = signalRep.Property.GetValue(entry.SignalReport)?.ToString();

                    if (value == null)
                    {
                        continue;
                    }

                    var line = String.Format("<{0}:{1}>{2}", signalRep.Attribute.PropertyName, value.Length, value);

                    recordLine += line;
                };


                adifData.AppendLine(String.Format("{0}{1}<EOR>", recordLine, Environment.NewLine));
            }

            var logFileName = currentEvent.EventType != EventType.PARKSONTHEAIR ?
                currentEvent.LogFileName :
                $"{currentEvent.Operators.First(_ => _.IsActive)}@{currentEvent.Designator}-{currentEvent.CreatedDate}";

            await _fileStoreService.WriteToFile(adifData.ToString(), logFileName, FileExtensions.Adif);

            _fileStoreService.OpenEventDirectory();
        }

        private async void ExportToCabrillo(Event eventLog, ICollection<LogEntry> logEntries)
        {
            var headerString = String.Format(
                "START-OF-LOG: {0}{1}CREATED-BY: K5BZI Logger version {2}{3}{3}{3}",
                "3.0", //TODO Appsettings.Get("CabrilloVersion")
                Environment.NewLine,
                "1.0", //TODO: AppSettings.Get("AppVersion") 
                Environment.NewLine);

            var cabrilloData = new StringBuilder(headerString);

            var eventProperties = from p in eventLog.GetType().GetProperties()
                                  let attr = p.GetCustomAttributes(typeof(CabrilloAttribute), true)
                                  where attr.Length == 1
                                  select new { Property = p, Attribute = attr.First() as CabrilloAttribute };

            foreach (var eventProp in eventProperties)
            {
                var value = eventProp.Property.GetValue(eventLog) as string;

                if (value == null)
                {
                    continue;
                }

                var line = String.Format("{0}:{1}", eventProp.Attribute.PropertyName, value);

                cabrilloData.AppendLine(line);
            };

            foreach (var entry in logEntries)
            {
                var line = new StringBuilder("QSO:");

                line.Append(String.Format("{0} ", entry.Signal.Frequency));
                line.Append(String.Format("{0} ", entry.Signal.Mode));
                line.Append(String.Format("{0} ", entry.QsoDate));
                line.Append(String.Format("{0} ", entry.Operator.CallSign));
                line.Append(String.Format("{0} ", entry.SignalReport.Sent));
                //line.Append(String.Format("{0} ", entry.SignalReport.Received)); EXCHANGE
                line.Append(String.Format("{0} ", entry.CallSign));
                line.Append(String.Format("{0} ", entry.SignalReport.Received));
                //line.Append(String.Format("{0} ", entry.SignalReport.Received)); TRANSMITTER

                cabrilloData.AppendLine(line.ToString());
            }

            cabrilloData.AppendLine(String.Format("{0}END-OF-LOG:{0}", Environment.NewLine));

            await _fileStoreService.WriteToFile(cabrilloData.ToString(), eventLog.LogFileName, FileExtensions.Cabrillo);

            _fileStoreService.OpenEventDirectory();
        }
    }
}
