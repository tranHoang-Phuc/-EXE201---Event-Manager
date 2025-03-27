using EventManagerAPI.DTO.Request;
using EventManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskJobController : ControllerBase
    {
        private readonly EventSetDbContext _context;

        public TaskJobController(EventSetDbContext context)
        {
            _context = context;
        }

        [HttpGet("/GetCategoryById/{cateId}")]
        public async Task<IActionResult> GetCategoryById(Guid cateId)
        {
            var data = await _context.EventCategory.FirstOrDefaultAsync(x => x.CategoryId == cateId);
            return Ok(data);
        }

        // Lấy tất cả TaskJob của một Event
        [HttpGet("getByEventId/{eventId}")]
        public async Task<IActionResult> GetTaskJobsByEventId(Guid eventId)
        {
            try
            {
                var taskJobs = await _context.TaskJobs
                    .Where(tj => tj.EventId == eventId)
                    .ToListAsync();

                if (taskJobs == null || !taskJobs.Any())
                {
                    return Ok(new { Data = new List<TaskJob>(), Message = "No TaskJobs found for this EventId" });
                }

                return Ok(new { Data = taskJobs, Message = "Get list TaskJobs by EventId successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error retrieving TaskJobs: {ex.Message}" });
            }
        }


        // Handle "Save All" (Add, Update, Delete list TaskJob)
        [HttpPost("saveAll/{eventId}")]
        public async Task<IActionResult> SaveAllTaskJobs(Guid eventId, [FromBody] List<TaskJobInfo> updatedTaskJobsDTO)
        {
            try
            {
                if (updatedTaskJobsDTO == null || !updatedTaskJobsDTO.Any())
                {
                    return BadRequest(new { Message = "TaskJob list cannot be empty" });
                }

                // Ánh xạ DTO thành TaskJob
                var updatedTaskJobs = updatedTaskJobsDTO.Select(dto => new TaskJob
                {
                    Id = dto.Id ?? Guid.NewGuid(), // Nếu Id null thì tạo mới
                    Task = dto.Task,
                    Assignee = dto.Assignee,
                    Priority = dto.Priority,
                    Description = dto.Description,
                    RelatedDocuments = dto.RelatedDocuments,
                    Notes = dto.Notes,
                    EventId = dto.EventId
                }).ToList();

                var existingTaskJobs = await _context.TaskJobs
                    .Where(tj => tj.EventId == eventId)
                    .ToListAsync();

                // Delete TaskJobs if not exist in new list
                var taskJobsToDelete = existingTaskJobs
                    .Where(a => !updatedTaskJobs.Any(u => u.Id == a.Id))
                    .ToList();
                if (taskJobsToDelete.Any())
                {
                    _context.TaskJobs.RemoveRange(taskJobsToDelete);
                }

                // Add new TaskJobs (Id is null or empty)
                var taskJobsToAdd = updatedTaskJobs
                    .Where(u => u.Id == Guid.Empty || u.Id == null || !existingTaskJobs.Any(a => a.Id == u.Id))
                    .ToList();
                if (taskJobsToAdd.Any())
                {
                    foreach (var taskJob in taskJobsToAdd)
                    {
                        taskJob.Id = Guid.NewGuid();
                        taskJob.EventId = eventId;
                        taskJob.CreatedAt = DateTime.UtcNow;
                        taskJob.UpdatedAt = DateTime.UtcNow;
                    }
                    _context.TaskJobs.AddRange(taskJobsToAdd);
                }

                // Update existing TaskJobs
                var taskJobsToUpdate = updatedTaskJobs
                    .Where(u => u.Id != Guid.Empty && u.Id != null && existingTaskJobs.Any(a => a.Id == u.Id))
                    .ToList();
                if (taskJobsToUpdate.Any())
                {
                    var taskJobIds = taskJobsToUpdate.Select(tj => tj.Id).ToList();
                    var existingTaskJobsToUpdate = await _context.TaskJobs
                        .Where(tj => taskJobIds.Contains(tj.Id))
                        .ToListAsync();

                    foreach (var existingTaskJob in existingTaskJobsToUpdate)
                    {
                        var updatedTaskJob = taskJobsToUpdate.FirstOrDefault(tj => tj.Id == existingTaskJob.Id);
                        if (updatedTaskJob != null)
                        {
                            existingTaskJob.Task = updatedTaskJob.Task;
                            existingTaskJob.Assignee = updatedTaskJob.Assignee;
                            existingTaskJob.Priority = updatedTaskJob.Priority;
                            existingTaskJob.Description = updatedTaskJob.Description;
                            existingTaskJob.RelatedDocuments = updatedTaskJob.RelatedDocuments;
                            existingTaskJob.Notes = updatedTaskJob.Notes;
                            existingTaskJob.UpdatedAt = DateTime.UtcNow;
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { IsSuccess = true, Message = "Save all TaskJobs successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { IsSuccess = false, Message = $"Failed to save TaskJobs: {ex.Message}" });
            }
        }

        // Update multiple TaskJobs
        [HttpPut("updateMultiple")]
        public async Task<IActionResult> UpdateMultipleTaskJobs([FromBody] List<TaskJobInfo> taskJobsDTO)
        {
            try
            {
                if (taskJobsDTO == null || !taskJobsDTO.Any())
                {
                    return BadRequest(new { Message = "TaskJobs data is required" });
                }

                // Ánh xạ DTO thành TaskJob
                var taskJobs = taskJobsDTO.Select(dto => new TaskJob
                {
                    Id = dto.Id ?? Guid.NewGuid(),
                    Task = dto.Task,
                    Assignee = dto.Assignee,
                    Priority = dto.Priority,
                    Description = dto.Description,
                    RelatedDocuments = dto.RelatedDocuments,
                    Notes = dto.Notes,
                    EventId = dto.EventId
                }).ToList();

                var taskJobIds = taskJobs.Select(tj => tj.Id).ToList();
                var existingTaskJobs = await _context.TaskJobs
                    .Where(tj => taskJobIds.Contains(tj.Id))
                    .ToListAsync();

                if (!existingTaskJobs.Any())
                {
                    return NotFound(new { Message = "No TaskJobs found to update" });
                }

                foreach (var existingTaskJob in existingTaskJobs)
                {
                    var updatedTaskJob = taskJobs.FirstOrDefault(tj => tj.Id == existingTaskJob.Id);
                    if (updatedTaskJob != null)
                    {
                        existingTaskJob.Task = updatedTaskJob.Task;
                        existingTaskJob.Assignee = updatedTaskJob.Assignee;
                        existingTaskJob.Priority = updatedTaskJob.Priority;
                        existingTaskJob.Description = updatedTaskJob.Description;
                        existingTaskJob.RelatedDocuments = updatedTaskJob.RelatedDocuments;
                        existingTaskJob.Notes = updatedTaskJob.Notes;
                        existingTaskJob.UpdatedAt = DateTime.UtcNow;
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(new { IsSuccess = true, Message = "TaskJobs updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { IsSuccess = false, Message = $"Error updating TaskJobs: {ex.Message}" });
            }
        }
    }
}