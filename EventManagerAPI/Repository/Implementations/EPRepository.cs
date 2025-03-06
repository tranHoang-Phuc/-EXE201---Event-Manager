using EventManagerAPI.DTO.Request;
using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventManagerAPI.Repository.Implementations
{
    public class EPRepository : IEPRepository
    {
        private readonly EventSetDbContext _context;

        public EPRepository(EventSetDbContext context)
        {
            _context = context;
        }

        // Get all EventParticipants
        public async Task<IEnumerable<EventParticipant>> GetEPs()
        => await _context.EventParticipants.Include(ep => ep.Event).Include(ep => ep.User).ToListAsync();


        // Get EventParticipant by Id
        public async Task<EventParticipant?> GetEPById(Guid id)
        => await _context.EventParticipants.Include(ep => ep.Event).Include(ep => ep.User).FirstOrDefaultAsync(ep => ep.Id == id);

        public async Task<List<EventParticipant>> GetEPByEventId(Guid eventId)
        => await _context.EventParticipants.Where(ep => ep.EventId == eventId).ToListAsync();

        // Create new EventParticipant
        public async Task<bool> CreateEP(EventParticipant eventParticipant)
        {
            try
            {
                await _context.EventParticipants.AddAsync(eventParticipant);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update EventParticipant
        public async Task<bool> UpdateEP(EventParticipant eventParticipant)
        {
            try
            {
                _context.EventParticipants.Update(eventParticipant);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Delete EventParticipant by Id
        public async Task<bool> DeleteEP(EventParticipant eventParticipant)
        {
            try
            {
                _context.EventParticipants.Remove(eventParticipant);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
        // Delete multiple EventParticipants by EventId
        public async Task<bool> DeleteMultiEP(List<EventParticipant> eps)
        {
            try
            {
                _context.EventParticipants.RemoveRange(eps);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<EPSendInfo>> GetEventsByUserId(string userId)
        {
            return await _context.EventParticipants
                .Include(x => x.User)
                .Include(x => x.Event)
                .Where(x => x.UserId == userId)
                .Select(x => new EPSendInfo
                {
                    Id = x.Id,
                    EventId = x.EventId,
                    UserId = x.UserId,
                    EventName = x.Event.EventName,
                    StartDate = x.Event.StartDate,
                    CategoryName = x.Event.EventCategory.CategoryName
                })
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();
        }

        public async Task<EventParticipant> GetEPByEventIdAndUserId(Guid eventId, string userId)
        => await _context.EventParticipants.FirstOrDefaultAsync(x => x.EventId == eventId && x.UserId == userId);
    }
}
