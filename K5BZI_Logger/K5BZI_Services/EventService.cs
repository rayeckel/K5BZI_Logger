using K5BZI_Models;
using K5BZI_Services.Interfaces;

namespace K5BZI_Services
{
    public class EventService : IEventService
    {
        public Event CreateNewEvent(string eventName)
        {
            return new Event
            {
                EventName = eventName
            };
        }
    }
}
