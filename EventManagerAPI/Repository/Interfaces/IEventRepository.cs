using EventManagerAPI.Models;
using System.Reflection;

namespace EventManagerAPI.Repository.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<IEnumerable<Event>> GetEventsByOrganizerId(string orId);
        Task<Event?> GetEventById(Guid eId);
        Task<Event?> GetEventByEventCode(string eventCode);
        Task<bool> CreateEvent(Event eInfo);
        Task<bool> UpdateEvent(Event eInfo);
        Task<bool> DeleteEvent(Event eInfo);
    }
}
