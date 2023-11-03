using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_Models.EntityModels;

namespace K5BZI_Services.Interfaces
{
    public interface IDefaultsService
    {
        Task<Defaults> GetDefaultsAsync();

        void UpdateDefaults(Defaults editDefaults);

        void SetDefaults(LogEntry logEntry);
    }
}
