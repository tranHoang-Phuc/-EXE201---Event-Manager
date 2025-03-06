using EventManagerAPI.Models;
using EventManagerAPI.Repository.Interfaces;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.Services.Implementations
{
    public class AgendaService : IAgendaService
    {
        private readonly IAgendaRepository _agendaRepository;

        public AgendaService(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }


        // Lấy tất cả Agenda của một Event 
        public async Task<(IEnumerable<Agenda>, string Message)> GetAgendasByEventId(Guid eventId)
        {
            var agendaByEventId = await _agendaRepository.GetAgendasByEventId(eventId);
            if (agendaByEventId != null && !(agendaByEventId.Any()))
            {
                agendaByEventId = new List<Agenda>();
                return (agendaByEventId, "Not have any Agenda by EventId");
            }
            return (agendaByEventId, "Get list Agenda by EventId Successfully");
        }



        // Tạo một Agenda mới
        public async Task<(bool IsSuccess, string Message)> CreateAgenda(Agenda agenda)
        {
            if (await _agendaRepository.CreateAgenda(agenda))
            {
                return (false, "Create Agenda failed");

            }
            return (true, "Create Agenda successfully");

        }
        // Xóa một Agenda


        // Handle "Save All" (Add, Update, Delete list Agenda)
        public async Task<(bool IsSuccess, string Message)> SaveAllAgendas(Guid eventId, List<Agenda> updatedAgendas)
        {
           
            try
            {
                var existingAgendas = await _agendaRepository.GetAgendasByEventId(eventId);

                // Delete AGENDA if not exist in new list
                var agendasToDelete = existingAgendas
                    .Where(a => !updatedAgendas.Any(u => u.AgendaId == a.AgendaId))
                    .ToList();
                if (agendasToDelete.Any())
                {
                    await _agendaRepository.DeleteMultipleAgendas(agendasToDelete);
                }

                // Add new AGENDA (AgendaId is null or empty)
                var agendasToAdd = updatedAgendas
                    .Where(u => u.AgendaId == Guid.Empty || u.AgendaId == null || !existingAgendas.Any(a => a.AgendaId == u.AgendaId))
                    .ToList();
                if (agendasToAdd.Any())
                {
                    // Tạo mới AgendaId cho các bản ghi mới
                    foreach (var agenda in agendasToAdd)
                    {
                        agenda.AgendaId = Guid.NewGuid(); // Tạo AgendaId mới
                        agenda.EventId = eventId; // Đảm bảo EventId được gán
                    }
                    await _agendaRepository.CreateMultipleAgendas(agendasToAdd);
                }

                // Update AGENDA existed
                var agendasToUpdate = updatedAgendas
                    .Where(u => u.AgendaId != Guid.Empty && u.AgendaId != null && existingAgendas.Any(a => a.AgendaId == u.AgendaId))
                    .ToList();
                if (agendasToUpdate.Any())
                {
                  if(!(await UpdateMultipleAgendas(agendasToUpdate))) {
                        return (false, "Update failed");
                    }
                }

                return (true, "Save all successfully");
            }
            catch (Exception)
            {
                // Log exception details

                return (false, "Failed to save agendas. Please try again.");
            }
        }

      
            public async Task<bool> UpdateMultipleAgendas(List<Agenda> agendas)
        {
            if (agendas == null || !agendas.Any())
                return false;

            // Lấy danh sách các Agenda từ database dựa trên danh sách gửi lên
            var agendaIds = agendas.Select(a => a.AgendaId).ToList();
            var existingAgendas = await _agendaRepository.GetAgendasByIds(agendaIds);

            if (!existingAgendas.Any())
                return false; // Không tìm thấy agenda nào để update

            // Cập nhật giá trị từng mục trong danh sách cũ
            foreach (var existingAgenda in existingAgendas)
            {
                var updatedAgenda = agendas.FirstOrDefault(a => a.AgendaId == existingAgenda.AgendaId);
                if (updatedAgenda != null)
                {
                    existingAgenda.Description = updatedAgenda.Description;
                    existingAgenda.TimeStart = updatedAgenda.TimeStart;
                    existingAgenda.TimeEnd = updatedAgenda.TimeEnd;
                }
            }

            // Cập nhật danh sách vào DB
            if(!(await _agendaRepository.UpdateMultipleAgendas(existingAgendas)))
            {
                return false;
            }

            return true;
        }

    }

}

