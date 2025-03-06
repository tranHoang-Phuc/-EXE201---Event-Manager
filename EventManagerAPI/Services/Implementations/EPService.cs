using EventManagerAPI.DTO.Request;
using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using EventManagerAPI.Services.Interfaces;
using System.Security.Cryptography;

namespace EventManagerAPI.Services.Implementations
{
    public class EPService : IEPService
    {

        private readonly IEPRepository _epRepository;
        private readonly IEventRepository _eventRepository;

        public EPService(IEPRepository epRepository, IEventRepository eventRepository)
        {
            _epRepository = epRepository;
            _eventRepository = eventRepository;
        }

        public async Task<(bool IsSuccess, string Message)> CheckMakeEvent(Guid eventId, string userId)
        {
            var getEPByEAU = await _epRepository.GetEPByEventIdAndUserId(eventId, userId);
            if (getEPByEAU == null) return (false, "Not found eventParticipants");

            if (getEPByEAU.IsActive == false) return (false, "Not make event");

            return (true, "Make event");
        }

        public async Task<(bool IsSuccess, string Message)> CreateEP(EPInfo epInfo, int action)
        {
            if (epInfo == null)
            {
                return (false, "epInfo null");
            }
            var newEP = new EventParticipant
            {
                EventId = epInfo.EventId,
                UserId = epInfo.UserId,
                IsActive = false
            };
            if (action == 1)
            {
                newEP.IsActive = true;
            }

            if(!(await _epRepository.CreateEP(newEP)))
            {
                return (false, "Create EP failed");
            }
            return (true, "Create EP successfully");
        }

        public async Task<(bool IsSuccess, string Message)> DeleteMultiEPByEventId(Guid eventId)
        {
            List<EventParticipant> getListsEpByEventId = await _epRepository.GetEPByEventId(eventId);

            if (getListsEpByEventId.Count() == 0) return (false, "Not have any EP by EventId");

            if (!(await _epRepository.DeleteMultiEP(getListsEpByEventId)))
                return (false, "Delete EP by EventId failed");

            return (true, "Delete EP by EventId Successfully:" + getListsEpByEventId.Count());
        }


        public async Task<(IEnumerable<EPSendInfo>, string Message)> getEventsByUserId(string userId)
        {
            var userEvents = await _epRepository.GetEventsByUserId(userId);
            if (userEvents == null || !userEvents.Any())
                return (Enumerable.Empty<EPSendInfo>(), "userId not existed!");


            return (userEvents, "Get events by userId successfully");
        }

        public async Task<(bool IsSuccess, string Message)> JoinEvent(JoinEventInfo jeInfo)
        {
            if (jeInfo == null) return (false, "join event null");
            var getEventByEventCode = await _eventRepository.GetEventByEventCode(jeInfo.EventCode);

            if(getEventByEventCode == null) return (false, "not found eventCode");

            if ((await _epRepository.GetEPByEventIdAndUserId(getEventByEventCode.EventId, jeInfo.UserId)) != null)
            {
                return (false, "You are already in this event!");
            }

            var newEP = new EPInfo
            {
                EventId = getEventByEventCode.EventId,
                UserId = jeInfo.UserId,
                IsActive = false
            };
            var checkCreate = await CreateEP(newEP, 0);

            if (!checkCreate.IsSuccess) return (false, "Create EP failed");

            return (true, "Create EP successfully");
        }
    }
}
