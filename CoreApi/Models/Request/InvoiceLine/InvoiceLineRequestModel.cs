using EmaAPI.Core;
using System.ComponentModel.DataAnnotations;

namespace EmaAPI.Models.Request.InvoiceLine
{
	public class InvoiceLineRequestModel : EntityBase
    {
		public int? RecordId { get; set; }
		public string? Description { get; set; }
		public decimal? Quantity { get; set; }
		public decimal? UnitPrice { get; set; }
		public decimal? DiscountRate { get; set; }
		public decimal? VatRate { get; set; }
		public decimal? TotalAmount { get; set; }
		public int? InvoiceId { get; set; }
		public int? ItemId { get; set; }
	}
}
