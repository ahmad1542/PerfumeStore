using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PerfumeStore.Domain.Entities
{
    public class Debt : BaseEntity
    {
        public int Id { get; set; }

        public int Amount { get; set; }
        public string? Notes { get; set; }

        public long? SalesInvoiceId { get; set; }
        public SalesInvoice? SalesInvoice { get; set; }

        public long? PurchaseInvoiceId { get; set; }
        public PurchaseInvoice? PurchaseInvoice { get; set; }

        public string? PersonPhone { get; set; }
        public Person? Person { get; set; }
    }

}
