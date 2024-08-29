
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EmaAPI.Core;

namespace EmaAPI.Models.Request.Customer
{
	public class CustomerRequestModel : EntityBase
    {
		public int RecordId { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public int CompanyId { get; set; }
	}
}
