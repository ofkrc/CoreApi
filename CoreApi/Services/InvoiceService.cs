using EmaAPI.Helpers;
using EmaAPI.Models.Request.Invoice;
using EmaAPI.Repositories;
using EmaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmaAPI.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<InvoiceLine> _invoiceLineRepository;

        public InvoiceService(IRepository<Invoice> invoiceRepository, IRepository<InvoiceLine> invoiceLineRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceLineRepository = invoiceLineRepository;
        }

        public Invoice Insert(InvoiceRequestModel request)
        {
            var newInvoice = new Invoice();
            GenericMappingHelper.Map(request, newInvoice);

            newInvoice = _invoiceRepository.Add(newInvoice);

            if (request.invoiceLineRequestModels != null && request.invoiceLineRequestModels.Any())
            {
                foreach (var lineRequest in request.invoiceLineRequestModels)
                {
                    var newLine = new InvoiceLine();
                    GenericMappingHelper.Map(lineRequest, newLine);
                    newLine.InvoiceId = newInvoice.RecordId;
                    newLine.UserId = newInvoice.UserId;

                    _invoiceLineRepository.Add(newLine);
                }
            }

            return newInvoice;
        }

        public void UpdateInvoice(Invoice existingInvoice, InvoiceRequestModel request)
        {
            GenericMappingHelper.Map(request, existingInvoice);

            _invoiceLineRepository.RemoveRange(existingInvoice.InvoiceLines);

            if (request.invoiceLineRequestModels != null && request.invoiceLineRequestModels.Any())
            {
                foreach (var lineRequest in request.invoiceLineRequestModels)
                {
                    var newLine = new InvoiceLine();
                    GenericMappingHelper.Map(lineRequest, newLine);
                    newLine.InvoiceId = existingInvoice.RecordId;
                    newLine.UserId = existingInvoice.UserId;

                    _invoiceLineRepository.Add(newLine);
                }
            }

            _invoiceRepository.Update(existingInvoice);
        }

        public Invoice GetInvoiceById(int id)
        {
            return _invoiceRepository.Include(i => i.InvoiceLines)
                .FirstOrDefault(i => i.RecordId == id);
        }

        public void DeleteInvoice(int invoiceId)
        {
            var invoiceToDelete = _invoiceRepository.Include(i => i.InvoiceLines)
        .FirstOrDefault(i => i.RecordId == invoiceId);

            if (invoiceToDelete != null)
            {
                foreach (var invoiceLine in invoiceToDelete.InvoiceLines)
                {
                    invoiceLine.Deleted = true;
                    _invoiceLineRepository.Update(invoiceLine);
                }

                invoiceToDelete.Deleted = true;
                _invoiceRepository.Update(invoiceToDelete);
            }
            else
            {
                throw new InvalidOperationException("Belirtilen ID'ye sahip fatura bulunamadı.");
            }
        }

        public IEnumerable<Invoice> Search()
        {
            return _invoiceRepository.List()
                .Where(i => !i.Deleted.HasValue || (i.Deleted.HasValue && i.Deleted.Value == false));
        }

        public IEnumerable<Invoice> SearchInvoices(string searchTerm)
        {
            var query = _invoiceRepository.List().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(i =>
                    i.Code.Contains(searchTerm) ||
                    i.InvoiceNumber.Contains(searchTerm) ||
                    i.Company.CompanyName.Contains(searchTerm) ||
                    i.Customer.Name.Contains(searchTerm)
                );
            }

            return query.Where(i => !i.Deleted.HasValue || (i.Deleted.HasValue && i.Deleted.Value == false)).ToList();
        }

        public IEnumerable<InvoiceLine> GetInvoiceLinesByInvoiceId(int invoiceId)
        {
            return _invoiceLineRepository.List()
                .Where(il => il.InvoiceId == invoiceId)
                .ToList();
        }
    }
}
