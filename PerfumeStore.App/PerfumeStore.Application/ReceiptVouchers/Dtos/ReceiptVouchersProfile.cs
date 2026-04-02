using AutoMapper;
using PerfumeStore.Application.ReceiptVouchers.Commands.CreateReceiptVoucher;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.ReceiptVouchers.Dtos;

public class ReceiptVouchersProfile : Profile {
    public ReceiptVouchersProfile() {
        CreateMap<CreateReceiptVoucherCommand, ReceiptVoucher>()
            .ForMember(d => d.ID, opt => opt.Ignore())
            .ForMember(d => d.Customer, opt => opt.Ignore())
            .ForMember(d => d.Person, opt => opt.Ignore())
            .ForMember(d => d.MoneyAccount, opt => opt.Ignore())
            .ForMember(d => d.AppliedSalesInvoices, opt => opt.Ignore())
            .ForMember(d => d.AppliedPersonDebts, opt => opt.Ignore());

        CreateMap<CreateReceiptVoucherSalesApplicationDto, ReceiptVoucherSalesInvoice>()
            .ForMember(d => d.ID, opt => opt.Ignore())
            .ForMember(d => d.ReceiptVoucherId, opt => opt.Ignore())
            .ForMember(d => d.ReceiptVoucher, opt => opt.Ignore())
            .ForMember(d => d.SalesInvoice, opt => opt.Ignore());

        CreateMap<CreateReceiptVoucherDebtApplicationDto, ReceiptVoucherDebt>()
            .ForMember(d => d.ID, opt => opt.Ignore())
            .ForMember(d => d.ReceiptVoucherId, opt => opt.Ignore())
            .ForMember(d => d.ReceiptVoucher, opt => opt.Ignore())
            .ForMember(d => d.Debt, opt => opt.Ignore());

        CreateMap<ReceiptVoucher, ReceiptVoucherDto>()
            .ForMember(d => d.PartyName, opt => opt.MapFrom(s => s.Customer != null ? s.Customer.Name : (s.Person != null ? s.Person.Name : null)))
            .ForMember(d => d.ReceiptForType, opt => opt.MapFrom(s => s.CustomerId.HasValue ? "customer" : "person"))
            .ForMember(d => d.MoneyAccountName, opt => opt.MapFrom(s => s.MoneyAccount.AccountName))
            .ForMember(d => d.AppliedInvoicesCount, opt => opt.MapFrom(s => s.AppliedSalesInvoices.Count))
            .ForMember(d => d.AppliedDebtsCount, opt => opt.MapFrom(s => s.AppliedPersonDebts.Count));

        CreateMap<ReceiptVoucher, ReceiptVoucherDetailsDto>()
            .ForMember(d => d.PartyName, opt => opt.MapFrom(s => s.Customer != null ? s.Customer.Name : (s.Person != null ? s.Person.Name : null)))
            .ForMember(d => d.ReceiptForType, opt => opt.MapFrom(s => s.CustomerId.HasValue ? "customer" : "person"))
            .ForMember(d => d.MoneyAccountName, opt => opt.MapFrom(s => s.MoneyAccount.AccountName))
            .ForMember(d => d.Applications, opt => opt.Ignore())
            .AfterMap((src, dest) => {
                dest.Applications = src.AppliedSalesInvoices
                    .Select(s => new ReceiptVoucherApplicationDetailsDto {
                        ApplicationType = "salesInvoice",
                        SalesInvoiceId = s.SalesInvoiceId,
                        ItemDate = s.SalesInvoice.Date,
                        AppliedAmount = s.AppliedAmount,
                        Notes = s.SalesInvoice.Notes
                    })
                    .Concat(src.AppliedPersonDebts.Select(d => new ReceiptVoucherApplicationDetailsDto {
                        ApplicationType = "personDebt",
                        DebtId = d.DebtId,
                        ItemDate = d.Debt.CreatedAt,
                        AppliedAmount = d.AppliedAmount,
                        Notes = d.Debt.Notes
                    }))
                    .OrderBy(x => x.ItemDate)
                    .ToList();
            });

        CreateMap<SalesInvoice, OpenSalesInvoiceDto>()
            .ForMember(d => d.SalesInvoiceId, opt => opt.MapFrom(s => s.ID))
            .ForMember(d => d.InvoiceDate, opt => opt.MapFrom(s => s.Date))
            .ForMember(d => d.RemainingAmount, opt => opt.MapFrom(s => s.Debt != null ? s.Debt.Amount : 0));

        CreateMap<Debt, OpenPersonDebtDto>()
            .ForMember(d => d.DebtId, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
            .ForMember(d => d.RemainingAmount, opt => opt.MapFrom(s => s.Amount))
            .ForMember(d => d.PersonName, opt => opt.MapFrom(s => s.Person != null ? s.Person.Name : null));
    }
}
