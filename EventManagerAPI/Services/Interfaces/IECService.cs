using EventManagerAPI.Models;

namespace EventManagerAPI.Services.Interfaces
{
    public interface IECService
    {
        Task<(IEnumerable<EventCategory>, string Message)> GetCategories();
    }
}
