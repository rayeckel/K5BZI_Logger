using K5BZI_Models;
using System.IO;

namespace K5BZI_Services.Interfaces
{
    public interface ILogListingService
    {
        LogListing CreateNewLogListing(FileInfo fileInfo);
    }
}
