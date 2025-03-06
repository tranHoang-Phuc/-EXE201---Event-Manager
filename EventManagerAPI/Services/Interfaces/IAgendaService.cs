using EventManagerAPI.DTO.Request;
using EventManagerAPI.Models;

namespace EventManagerAPI.Services.Interfaces
{
    public interface IAgendaService
    {
        Task<(bool IsSuccess, string Message)> CreateAgenda(Agenda agenda);
        Task<(IEnumerable<Agenda>, string Message)> GetAgendasByEventId(Guid eventId);
        
        Task<(bool IsSuccess, string Message)> SaveAllAgendas(Guid eventId, List<Agenda> updatedAgendas);
        Task<bool> UpdateMultipleAgendas(List<Agenda> agendas);

    }
}
