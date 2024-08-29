using System.ComponentModel.DataAnnotations;

namespace EmaAPI.Models.Request.User
{
	public class UserRequestModel
	{
		public decimal RecordId { get; set; }
		public string UserName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public string CompanyName { get; set; }

		public DateTime CreatedDatetime { get; set; } = DateTime.UtcNow;

		public bool IsActive { get; set; }
		public bool Deleted { get; set; }
	}
}
