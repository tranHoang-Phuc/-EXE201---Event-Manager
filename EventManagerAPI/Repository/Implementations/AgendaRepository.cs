using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.Repository.Implementations
{
    public class AgendaRepository : IAgendaRepository
    {

        private readonly EventSetDbContext _context;

        public AgendaRepository(EventSetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Agenda>> GetAgendas()
        {
            return await _context.Agendas
                .Include(x => x.Event)
                .ToListAsync();
        }

        public async Task<Agenda?> GetAgendaById(Guid agendaId)
        => await _context.Agendas.FindAsync(agendaId);


        public async Task<bool> CreateAgenda(Agenda agenda)
        {
            try
            {
                await _context.Agendas.AddAsync(agenda);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAgenda(Agenda agenda)
        {
            try
            {
                _context.Agendas.Update(agenda);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAgenda(Agenda agenda)
        {
            try
            {

                _context.Agendas.Remove(agenda);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> CreateMultipleAgendas(List<Agenda> agendas)
        {
            try
            {
                await _context.Agendas.AddRangeAsync(agendas);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> UpdateMultipleAgendas(List<Agenda> agendas)
        {
            try
            {
                _context.Agendas.UpdateRange(agendas);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> DeleteMultipleAgendas(List<Agenda> agendas)
        {
            try
            {
                _context.Agendas.RemoveRange(agendas);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Agenda>> GetAgendasByEventId(Guid eventId)
        => await _context.Agendas.Where(x => x.EventId == eventId).OrderBy(x => x.TimeEnd).ToListAsync();

        public async Task<List<Agenda>> GetAgendasByIds(List<Guid> agendaIds)
        {
            return await _context.Agendas
                .Where(a => agendaIds.Contains(a.AgendaId))
                .ToListAsync();
        }
    }


}

