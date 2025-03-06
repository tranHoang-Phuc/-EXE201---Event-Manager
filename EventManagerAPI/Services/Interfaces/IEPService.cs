using EventManagerAPI.DTO.Request;
using EventManagerAPI.Models;
namespace EventManagerAPI.Services.Interfaces
{
    public interface IEPService
    {
        Task<(bool IsSuccess, string Message)> CreateEP(EPInfo epInfo, int action);
        Task<(bool IsSuccess, string Message)> JoinEvent(JoinEventInfo jeInfo);
        Task<(IEnumerable<EPSendInfo>, string Message)> getEventsByUserId(string userId);
        Task<(bool IsSuccess, string Message)> DeleteMultiEPByEventId(Guid eventId);
        Task<(bool IsSuccess, string Message)> CheckMakeEvent(Guid eventId, string userId);


    }
}
