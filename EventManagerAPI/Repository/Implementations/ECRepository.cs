using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.Repository.Implementations
{
    public class ECRepository : IECRepository
    {
        private readonly EventSetDbContext _context;

        public ECRepository(EventSetDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EventCategory>> GetECs()
        => await _context.EventCategory.ToListAsync();
    }
}
