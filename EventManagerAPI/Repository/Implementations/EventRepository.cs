using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EventManagerAPI.Repository.Implementations
{
    public class EventRepository : IEventRepository
    {

        private readonly EventSetDbContext _context;

        public EventRepository(EventSetDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateEvent(Event eInfo)
        {
            try
            {
                await _context.AddAsync(eInfo); 
                await _context.SaveChangesAsync();
                return true; 
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEvent(Event eInfo)
        {
            try
            {
           

                _context.Events.Remove(eInfo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Event>> GetEvents()
        => await _context.Events
                .Include(x => x.Organizer)
                .Include(x => x.EventCategory)
                .ToListAsync();


        public async Task<bool> UpdateEvent(Event eInfo)
        {
            try
            {
                _context.Events.Update(eInfo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Event?> GetEventById(Guid eId)
        => await _context.Events.FindAsync(eId);

        public async Task<IEnumerable<Event>> GetEventsByOrganizerId(string orId)
        => await _context.Events.Where(x => x.OrganizerId == orId).ToListAsync();

        public async Task<Event?> GetEventByEventCode(string eventCode)
        => await _context.Events.FirstOrDefaultAsync(x => x.EventCode.Trim() == eventCode.Trim());
    }
}
