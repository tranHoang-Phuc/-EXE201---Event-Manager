using EventManagerAPI.DTO.Request;
using EventManagerAPI.Models;

namespace EventManagerAPI.Services.Interfaces
{
    public interface IEventService
    {
        Task<(IEnumerable<Event>, string Message)> GetEventByOrganizedId(string orId);
        Task<(Event?, string Message)> GetEventById(Guid eId);
        Task<(bool IsSuccess, string Message)> CreateEvent(EventInfo eInfo);
        Task<(bool IsSuccess, string Message)> UpdateEvent(EventInfo eInfo);
        Task<(bool IsSuccess, string Message)> DeleteEvent(Guid eventId);
    }
}
