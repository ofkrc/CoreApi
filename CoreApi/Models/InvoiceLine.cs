using EmaAPI.Core;
using EmaAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class InvoiceLine : EntityBase
{
	[Key]
	public int? RecordId { get; set; }

	[StringLength(1000)]
	public string? Description { get; set; }
	public decimal? Quantity { get; set; }
	public decimal? UnitPrice { get; set; }
	public decimal? DiscountRate { get; set; }
	public decimal? VatRate { get; set; }
	public decimal? TotalAmount { get; set; }

	[ForeignKey(nameof(UserId))]
	public User? User { get; set; }

	public int? InvoiceId { get; set; }

	[ForeignKey(nameof(InvoiceId))]
	public Invoice? Invoice { get; set; }

	public int? ItemId { get; set; }

	[ForeignKey(nameof(ItemId))]
	public Item? Item { get; set; }
}
