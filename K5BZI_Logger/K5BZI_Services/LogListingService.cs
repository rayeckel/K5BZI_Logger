using K5BZI_Models;
using K5BZI_Services.Interfaces;
using System.IO;

namespace K5BZI_Services
{
    public class LogListingService : ILogListingService
    {
        public LogListing CreateNewLogListing(FileInfo fileInfo)
        {
            var fileName = System.IO.Path.ChangeExtension(fileInfo.Name, null);

            return new LogListing
            {
                FileName = fileName,
                CreatedDate = fileInfo.CreationTimeUtc
            };
        }
    }
}
