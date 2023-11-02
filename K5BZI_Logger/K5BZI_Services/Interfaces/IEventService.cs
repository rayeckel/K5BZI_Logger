using System.Collections.Generic;
using System.Threading.Tasks;
using K5BZI_Models;

namespace K5BZI_Services.Interfaces
{
    public interface IEventService
    {
        void OpenEventList();

        Task<Event> CreateNewEventAsync(string eventName);

        Task<Event> UpdateEventAsync(Event editEvent, List<Operator> operators);

        Task<List<Event>> GetAllEventsAsync();
    }
}
