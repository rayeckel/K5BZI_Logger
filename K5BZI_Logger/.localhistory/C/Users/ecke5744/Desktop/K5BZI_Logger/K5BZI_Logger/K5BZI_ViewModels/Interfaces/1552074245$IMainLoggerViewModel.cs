using K5BZI_Models.Main;

namespace K5BZI_ViewModels.Interfaces
{
    public interface IMainLoggerViewModel
    {
        IMainModel Model { get; }

        void CreateMockLogEntry();

        void SaveLogEntry();
    }
}
