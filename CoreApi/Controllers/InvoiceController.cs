using EmaAPI.Models.Request.Invoice;
using EmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmaAPI.Controllers
{
    [Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class InvoiceController : ControllerBase
	{
		private readonly IInvoiceService _invoiceService;

		public InvoiceController(IInvoiceService invoiceService)
		{
			_invoiceService = invoiceService;
		}

		[HttpPost("Insert")]
		public ActionResult<Invoice> Insert([FromBody] InvoiceRequestModel request)
		{
			var newUser = _invoiceService.Insert(request);

			return CreatedAtAction(nameof(Insert), new { id = newUser.RecordId }, newUser);
		}

		[HttpPut("Update/{id}")]
		public IActionResult Update(int id, [FromBody] InvoiceRequestModel request)
		{
			try
			{
				var existingInvoice = _invoiceService.GetInvoiceById(id);

				if (existingInvoice == null)
				{
					return NotFound($"Invoice with ID {id} not found");
				}

				_invoiceService.UpdateInvoice(existingInvoice, request);

				return Ok(existingInvoice);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				_invoiceService.DeleteInvoice(id);
				return Ok("Fatura ve bağlı satırlar başarıyla silindi.");
			}
			catch (InvalidOperationException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				// Diğer olası hataları ele alabilirsiniz.
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

		[HttpGet("Get")]
		public IActionResult Get()
		{
			try
			{
				var invoices = _invoiceService.Search();
				return Ok(invoices);
			}
			catch (Exception ex)
			{
				// Diğer olası hataları ele alabilirsiniz.
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

		[HttpGet("SearchInvoices")]
		public IActionResult SearchInvoices(string searchTerm)
		{
			try
			{
				var invoices = _invoiceService.SearchInvoices(searchTerm);
				return Ok(invoices);
			}
			catch (Exception ex)
			{
				// Diğer olası hataları ele alabilirsiniz.
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

		[HttpGet("{invoiceId}/InvoiceLines")]
		public IActionResult GetInvoiceLinesByInvoiceId(int invoiceId)
		{
			try
			{
				var invoiceLines = _invoiceService.GetInvoiceLinesByInvoiceId(invoiceId);

				if (invoiceLines == null || !invoiceLines.Any())
				{
					return NotFound($"InvoiceId {invoiceId}'ye bağlı invoice lines bulunamadı.");
				}

				return Ok(invoiceLines);
			}
			catch (Exception ex)
			{
				// Diğer olası hataları ele alabilirsiniz.
				return StatusCode(500, "Bir hata oluştu.");
			}
		}

	}
}

