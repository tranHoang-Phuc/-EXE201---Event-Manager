using EventManagerAPI.Models;

namespace EventManagerAPI.Repository.Interfaces
{
    public interface IECRepository
    {

        Task<IEnumerable<EventCategory>> GetECs();

    }
}
