using AutoMapper;
using PerfumeStore.Application.SalesInvoices.Commands.CreateSalesInvoice;
using PerfumeStore.Application.SalesInvoices.Commands.UpdateSalesInvoice;
using PerfumeStore.Domain.Entities;
using System.Data;

namespace PerfumeStore.Application.SalesInvoices.Dtos {
    public class SalesInvoicesProfile : Profile {
        public SalesInvoicesProfile() {
            CreateMap<SalesInvoice, SalesInvoiceDto>()
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer != null ? s.Customer.Name : null))
                .ForMember(d => d.DebtAmount, opt => opt.MapFrom(s => s.Debt != null ? s.Debt.Amount : 0));
            
            CreateMap<CreateSalesInvoiceCommand, SalesInvoice>()
                .ForMember(d => d.ID, opt => opt.Ignore())
                .ForMember(d => d.Customer, opt => opt.Ignore())
                .ForMember(d => d.ReceiptVouchers, opt => opt.Ignore())
                .ForMember(d => d.SalesInvoiceItems, opt => opt.Ignore())
                .ForMember(d => d.Debt, opt => opt.MapFrom((src, dest) =>
                    (src.DebtAmount.HasValue && src.DebtAmount.Value > 0)
                        ? new Debt {
                            Amount = src.DebtAmount.Value,
                            Notes = src.DebtNotes,
                            PersonId = src.CustomerId,
                            SalesInvoice = dest
                        }
                        : null
                ));
            CreateMap<UpdateSalesInvoiceCommand, SalesInvoice>();
        }
    }
}
