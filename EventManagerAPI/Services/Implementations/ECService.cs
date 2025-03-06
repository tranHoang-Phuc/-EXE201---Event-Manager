using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using EventManagerAPI.Services.Interfaces;

namespace EventManagerAPI.Services.Implementations
{
    public class ECService : IECService
    {

        private readonly IECRepository _ecRepository;

        public ECService(IECRepository eCRepository)
        {
            _ecRepository = eCRepository;
        }
        public async Task<(IEnumerable<EventCategory>, string Message)> GetCategories()
        {

            var getECs = await _ecRepository.GetECs();
            if (!(getECs.Any())) return (Enumerable.Empty<EventCategory>(), "Event Category Empty");

            return (getECs, "getEC successfully");
        }
    }
}
