using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.Models
{
	public class EventCategory
	{
		[Key]
		public Guid CategoryId { get; set; } = Guid.NewGuid();

		[Required]
		[StringLength(255)]
		public string CategoryName { get; set; }

		// Quan hệ 1-N với Event
		public ICollection<Event> Events { get; set; }

	}
}
