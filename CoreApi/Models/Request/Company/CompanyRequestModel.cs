using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EmaAPI.Core;

namespace EmaAPI.Models.Request.Company
{
	public class CompanyRequestModel : EntityBase
	{
		public int RecordId { get; set; }
		public string? CompanyName { get; set; }
		public string? Address { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Website { get; set; }
		public string? Email { get; set; }
		public string? TaxOffice { get; set; }
		public string? TaxNo { get; set; }
	}
}
