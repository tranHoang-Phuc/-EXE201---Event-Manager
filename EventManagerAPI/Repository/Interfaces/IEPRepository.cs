using EventManagerAPI.DTO.Request;
using EventManagerAPI.Models;

namespace EventManagerAPI.Repository.Interfaces
{
    public interface IEPRepository
    {
        Task<IEnumerable<EventParticipant>> GetEPs();
        Task<EventParticipant?> GetEPById(Guid id);
        Task<List<EventParticipant>> GetEPByEventId(Guid id);
        Task<EventParticipant> GetEPByEventIdAndUserId(Guid eventId, string userId);
        Task<IEnumerable<EPSendInfo>> GetEventsByUserId(string userId);
        Task<bool> CreateEP(EventParticipant eventParticipant);
        Task<bool> UpdateEP(EventParticipant eventParticipant);
        Task<bool> DeleteEP(EventParticipant eventParticipant);
        Task<bool> DeleteMultiEP(List<EventParticipant> eps);
    }
}
