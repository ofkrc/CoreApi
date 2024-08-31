using CoreApi.Core.Base.Model;
using CoreApi.Models;
using CoreApi.Models.Request.Invoice;
using CoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("Insert")]
        public ActionResult<BaseResponse<Invoice>> Insert([FromBody] InvoiceRequestModel request)
        {
            try
            {
                var newInvoice = _invoiceService.Insert(request);
                return SuccessResponse(newInvoice, "Fatura başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Invoice>(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public ActionResult<BaseResponse<Invoice>> Update(int id, [FromBody] InvoiceRequestModel request)
        {
            try
            {
                var existingInvoice = _invoiceService.GetInvoiceById(id);

                if (existingInvoice == null)
                {
                    return ErrorResponse<Invoice>($"Invoice with ID {id} not found");
                }

                _invoiceService.UpdateInvoice(existingInvoice, request);

                return SuccessResponse(existingInvoice, "Fatura başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<Invoice>(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<BaseResponse<string>> Delete(int id)
        {
            try
            {
                _invoiceService.DeleteInvoice(id);
                return SuccessResponse("Fatura ve bağlı satırlar başarıyla silindi.");
            }
            catch (InvalidOperationException ex)
            {
                return ErrorResponse<string>(ex.Message);
            }
            catch (Exception ex)
            {
                return ErrorResponse<string>("Bir hata oluştu.");
            }
        }

        [HttpGet("Get")]
        public ActionResult<BaseResponse<IEnumerable<Invoice>>> Get()
        {
            try
            {
                var invoices = _invoiceService.Search();
                return SuccessResponse(invoices, "Faturalar başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<Invoice>>(ex.Message);
            }
        }

        [HttpGet("SearchInvoices")]
        public ActionResult<BaseResponse<IEnumerable<Invoice>>> SearchInvoices(string searchTerm)
        {
            try
            {
                var invoices = _invoiceService.SearchInvoices(searchTerm);
                return SuccessResponse(invoices, "Arama sonuçları başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<Invoice>>(ex.Message);
            }
        }

        [HttpGet("{invoiceId}/InvoiceLines")]
        public ActionResult<BaseResponse<IEnumerable<InvoiceLine>>> GetInvoiceLinesByInvoiceId(int invoiceId)
        {
            try
            {
                var invoiceLines = _invoiceService.GetInvoiceLinesByInvoiceId(invoiceId);

                if (invoiceLines == null || !invoiceLines.Any())
                {
                    return ErrorResponse<IEnumerable<InvoiceLine>>($"InvoiceId {invoiceId}'ye bağlı invoice lines bulunamadı.");
                }

                return SuccessResponse(invoiceLines, "Faturaya bağlı satırlar başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ErrorResponse<IEnumerable<InvoiceLine>>(ex.Message);
            }
        }
    }
}
