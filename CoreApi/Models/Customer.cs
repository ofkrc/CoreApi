using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EmaAPI.Core;

namespace EmaAPI.Models
{
	public class Customer : EntityBase
	{
		[Key]
		public int? RecordId { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(20)]
		[Required(ErrorMessage = "Bu alan boş geçilemez!")]
		public string? Name { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(20)]
		[Required(ErrorMessage = "Bu alan boş geçilemez!")]
		public string? Surname { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(100)]
		public string? Address { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(50)]
		public string? Email { get; set; }

		[Column(TypeName = "Varchar")]
		[StringLength(15)]
		public string? PhoneNumber { get; set; }

		public int? CompanyId { get; set; }

		[ForeignKey(nameof(CompanyId))]
		public Company? Company { get; set; } 

		[ForeignKey(nameof(UserId))]
		public User? User { get; set; }
		public ICollection<Invoice> Invoice { get; set; }

	}

}
