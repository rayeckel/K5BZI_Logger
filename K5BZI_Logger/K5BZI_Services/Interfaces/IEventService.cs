using K5BZI_Models;
using System.Collections.Generic;

namespace K5BZI_Services.Interfaces
{
    public interface IEventService
    {
        Event CreateNewEvent(string eventName);

        Event UpdateEvent(Event editEvent);

        List<Event> GetAllEvents();
    }
}
