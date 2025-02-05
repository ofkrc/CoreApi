﻿using CoreApi.Models.Request.Invoice;

namespace CoreApi.Services.Interfaces
{
    public interface IInvoiceService
    {
        Invoice Insert(InvoiceRequestModel request);
        void UpdateInvoice(Invoice existingInvoice, InvoiceRequestModel request);
        Invoice GetInvoiceById(int id);
        void DeleteInvoice(int invoiceId);
        IEnumerable<Invoice> Search();
        IEnumerable<Invoice> SearchInvoices(string searchTerm);
        IEnumerable<InvoiceLine> GetInvoiceLinesByInvoiceId(int invoiceId);
    }
}
