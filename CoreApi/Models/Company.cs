using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EmaAPI.Core;

namespace EmaAPI.Models
{
	public class Company : EntityBase
	{
		[Key]
		public int? RecordId { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(100)]
		[Required(ErrorMessage = "This field cannot be empty!")]
		public string? CompanyName { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(100)]
		public string? Address { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(15)]
		public string? PhoneNumber { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(200)]
		public string? Website { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(50)]
		public string? Email { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(50)]
		public string? TaxOffice { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(50)]
		public string? TaxNo { get; set; }

		[ForeignKey(nameof(UserId))]
		public User? User { get; set; }


		public ICollection<Customer>? Customer { get; set; }
		public ICollection<Invoice>? Invoice { get; set; }



	}
}
