using K5BZI_Models;
using K5BZI_Models.Attributes;
using K5BZI_Services.Interfaces;
using System.Linq;

namespace K5BZI_Services
{
    public class ExportService : IExportService
    {
        public ExportService()
        {

        }

        public void ExportLog(Event evntLog, LogType logType)
        {
            switch(logType)
            {
                case LogType.Adif:
                    ExportToAdif(evntLog);
                    break;
            }
        }

        private void ExportToAdif(Event eventLog)
        {
            var eventProperties = from p in eventLog.GetType().GetProperties()
                        let attr = p.GetCustomAttributes(typeof(AdifAttribute), true)
                        where attr.Length == 1
                        select new { Property = p, Attribute = attr.First() as AdifAttribute };

            eventProperties.GetType();
        }
    }
}
