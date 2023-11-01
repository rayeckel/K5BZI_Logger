using System.Collections.Generic;
using K5BZI_Models;

namespace K5BZI_Services.Interfaces
{
    public interface IEventService
    {
        void OpenEventList();

        Event CreateNewEvent(string eventName);

        Event UpdateEvent(Event editEvent, List<Operator> operators);

        List<Event> GetAllEvents();
    }
}
