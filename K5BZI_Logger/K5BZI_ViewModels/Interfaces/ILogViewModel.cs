using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_Models.ViewModelModels;

namespace K5BZI_ViewModels.Interfaces
{
    public interface ILogViewModel
    {
        LogModel LogModel { get; }

        void CreateNewLog(Event newEvent);

        Task GetLog(Event selectedEvent);
    }
}
