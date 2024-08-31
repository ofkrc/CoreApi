using CoreApi.Core;
using CoreApi.Models.Request.InvoiceLine;
using CoreApi.Models.Request.Item;

namespace CoreApi.Models.Request.Invoice
{
	public class InvoiceRequestModel : EntityBase
    {
		public int? RecordId { get; set; }
		public string? InvoiceNumber { get; set; }
		public DateTime? InvoiceDate { get; set; }
		public decimal? OrderNumber { get; set; }
		public DateTime? OrderDate { get; set; }
		public decimal? TotalAmount { get; set; }
		public int? CompanyId { get; set; } 
		public int? CustomerId { get; set; }

		public List<InvoiceLineRequestModel> invoiceLineRequestModels { get; set; }
	}
}
