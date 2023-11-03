using System.Collections.Generic;
using System.Threading.Tasks;
using K5BZI_Models;

namespace K5BZI_Services.Interfaces
{
    public interface IEventService
    {
        void OpenEventDirectory();

        Task SaveEventsAsync(List<Event> eventList);

        Task<List<Event>> GetEventsAsync();
    }
}
