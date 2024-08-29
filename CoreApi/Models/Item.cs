using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EmaAPI.Core;

namespace EmaAPI.Models
{
	
	public class Item : EntityBase
    {
		[Key]
		public int? RecordId { get; set; }

		[Required]
		[StringLength(255)]
		public string? Name { get; set; }

		[StringLength(1000)]
		public string? Description { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public decimal? SalesPrice { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public decimal? PurchasePrice { get; set; }

		[Required]
		[Range(0, int.MaxValue)]
		public int? StockQuantity { get; set; }
		public decimal? DiscountRate { get; set; }
		public decimal? VatRate { get; set; }

		[ForeignKey(nameof(UserId))]
		public User? User { get; set; }

		public ICollection<InvoiceLine> InvoiceLine { get; set; }

	}

}
