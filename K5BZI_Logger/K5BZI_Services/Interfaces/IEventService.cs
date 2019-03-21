using K5BZI_Models;

namespace K5BZI_Services.Interfaces
{
    public interface IEventService
    {
        Event CreateNewEvent(string eventName);
    }
}
