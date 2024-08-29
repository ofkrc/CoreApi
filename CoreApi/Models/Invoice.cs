using EmaAPI.Core;
using EmaAPI.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Invoice : EntityBase
{
	[Key]
	public int? RecordId { get; set; }
	public string? InvoiceNumber { get; set; }
	public decimal? OrderNumber { get; set; }
	public DateTime? InvoiceDate { get; set; }
	public DateTime? OrderDate { get; set; }
	public decimal? TotalAmount { get; set; }
	public int? CompanyId { get; set; }
	public int? CustomerId { get; set; }

	[ForeignKey(nameof(CompanyId))]
	public Company? Company { get; set; }

	[ForeignKey(nameof(CustomerId))]
	public Customer? Customer { get; set; }

	[ForeignKey(nameof(UserId))]
	public User? User { get; set; }

	public ICollection<InvoiceLine> InvoiceLines { get; set; }
}
