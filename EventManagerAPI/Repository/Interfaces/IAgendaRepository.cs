using EventManagerAPI.Models;

namespace EventManagerAPI.Repository.Interfaces
{

        public interface IAgendaRepository
        {
            Task<IEnumerable<Agenda>> GetAgendas();
            Task<List<Agenda>> GetAgendasByEventId(Guid eventId);
            Task<Agenda?> GetAgendaById(Guid agendaId);
            Task<bool> CreateAgenda(Agenda agenda);
            Task<bool> UpdateAgenda(Agenda agenda);
            Task<bool> DeleteAgenda(Agenda agenda);

            Task<bool> CreateMultipleAgendas(List<Agenda> agendas);
            Task<bool> UpdateMultipleAgendas(List<Agenda> agendas);
            Task<bool> DeleteMultipleAgendas(List<Agenda> agendas);
            Task<List<Agenda>> GetAgendasByIds(List<Guid> agendaIds);

    }
}
