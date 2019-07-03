using K5BZI_Models.EntityModels;

namespace K5BZI_Services.Interfaces
{
    public interface IDefaultsService
    {
        Defaults GetDefaults();

        void UpdateDefaults(Defaults editDefaults);
    }
}
