using EventManagerAPI.DTO.Request;
using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using EventManagerAPI.Services.Interfaces;
using System;
using System.Reflection;
using System.Security.Cryptography;
using EventInfo = EventManagerAPI.DTO.Request.EventInfo;
namespace EventManagerAPI.Services.Implementations
{
    public class EventService : IEventService
    {

        private readonly IEventRepository _eventRepository;
        private readonly IEPRepository _epRepository;
        private readonly IAgendaRepository _agendaRepository;
        private readonly IEPService _epService;

        public EventService(IEventRepository eventRepository, IEPService epService, IEPRepository ePRepository, IAgendaRepository agendaRepository)
        {
            _eventRepository = eventRepository;
            _epRepository = ePRepository;
            _agendaRepository = agendaRepository;
            _epService = epService;
        }

        public async Task<(bool IsSuccess, string Message)> CreateEvent(EventInfo eInfo)
        {
            if (eInfo == null)
            {
                return (false, "Event Info null");
            }
            Event newE = new Event
            {
                EventName = eInfo.EventName,
                StartDate = eInfo.StartDate,
                EndDate = eInfo.EndDate,
                DressCode = eInfo.DressCode,
                Location = eInfo.Location,
                Concept = eInfo.Concept,
                CategoryId = eInfo.CategoryId,
                ParticipantCount = eInfo.ParticipantCount,
                OrganizerId = eInfo.OrganizerId,
                EventCode = GenerateShortUniqueCode(),

                Agendas = new List<Agenda>(),
                EventParticipants = new List<EventParticipant>()
            };
            
            if (!(await _eventRepository.CreateEvent(newE)))
                return (false, "Add event failed");

            var newEP = new EPInfo
            {
                EventId = newE.EventId,
                UserId = newE.OrganizerId,
                IsActive = true
            };

            var getEP = await _epService.CreateEP(newEP, 1);
            if(!getEP.IsSuccess)
            {
                return (false, getEP.Message);
            }

            return (true, "Add event successfully");
        }


        public async Task<(bool IsSuccess, string Message)> DeleteEvent(Guid eventId)
        {
            var getEvent = await _eventRepository.GetEventById(eventId);
            var getEPsByEventId = await _epRepository.GetEPByEventId(eventId);
            var getAgendasByEvent = await _agendaRepository.GetAgendasByEventId(eventId);

            if (getEvent == null) return (false, "Event get by Id is null");
            else if (getEPsByEventId == null) return (false, "getEPsByEventId get by Id is null");
             else if (getAgendasByEvent == null) return (false, "getAgendasByEvent get by Id is null");
          

            if(!(await _epRepository.DeleteMultiEP(getEPsByEventId)))
            
                return (false, "Delete getAgendasByEvent failed");
            
            
            if (!(await _agendaRepository.DeleteMultipleAgendas(getAgendasByEvent)))
            
                return (false, "Delete getAgendasByEvent failed");
            
            if (!(await _eventRepository.DeleteEvent(getEvent)))
                return (false, "Delete getAgendasByEvent failed");

            return (true, "Delete Event successfully");
         
        }

        public async Task<(Event?, string Message)> GetEventById(Guid eId)
        {
            var getEventById = await _eventRepository.GetEventById(eId);
            if (getEventById == null)
                return (null, "EventId not existed!");

            return (getEventById, "get EventById successfully");

        }

        public async Task<(IEnumerable<Event>, string Message)> GetEventByOrganizedId(string orId)
        {
            var getEventByOrganizedId = await _eventRepository.GetEventsByOrganizerId(orId);
            if (getEventByOrganizedId == null || !getEventByOrganizedId.Any())
                return (Enumerable.Empty<Event>(), "OrId not existed!");

            return (getEventByOrganizedId, "get GetEventByOrganizedId successfully");
        }

        public async Task<(bool IsSuccess, string Message)> UpdateEvent(DTO.Request.EventInfo eInfo)
        {
            if (eInfo == null)
            {
                return (false, "Event Info null");
            }
            var getEventById = await _eventRepository.GetEventById(eInfo.EventId);
            getEventById.EventName = eInfo.EventName;
            getEventById.StartDate = eInfo.StartDate;
            getEventById.EndDate = eInfo.EndDate;
            getEventById.DressCode = eInfo.DressCode;
            getEventById.Location = eInfo.Location;
            getEventById.Concept = eInfo.Concept;
            getEventById.CategoryId = eInfo.CategoryId;
            getEventById.ParticipantCount = eInfo.ParticipantCount;
            getEventById.OrganizerId = eInfo.OrganizerId;

            if (!(await _eventRepository.UpdateEvent(getEventById)))
                return (false, "Update event failed");

            return (true, "Update event successfully");
        }


        private string GenerateShortUniqueCode()
        {
            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            byte[] shortBytes = new byte[6];
            Array.Copy(bytes, 0, shortBytes, 0, 6); // Lấy 6 byte (48-bit)
            return Convert.ToBase64String(shortBytes).Substring(0, 8); // 8 ký tự
        }
    }
}
