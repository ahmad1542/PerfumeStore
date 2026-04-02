using AutoMapper;
using PerfumeStore.Application.PaymentVouchers.Commands.CreatePaymentVoucher;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.PaymentVouchers.Dtos;

public class PaymentVouchersProfile : Profile {
    public PaymentVouchersProfile() {
        CreateMap<CreatePaymentVoucherCommand, PaymentVoucher>()
            .ForMember(d => d.ID, opt => opt.Ignore())
            .ForMember(d => d.Supplier, opt => opt.Ignore())
            .ForMember(d => d.MoneyAccount, opt => opt.Ignore())
            .ForMember(d => d.AppliedPurchaseInvoices, opt => opt.Ignore());

        CreateMap<CreatePaymentVoucherApplicationDto, PaymentVoucherPurchaseInvoice>()
            .ForMember(d => d.ID, opt => opt.Ignore())
            .ForMember(d => d.PaymentVoucherId, opt => opt.Ignore())
            .ForMember(d => d.PaymentVoucher, opt => opt.Ignore())
            .ForMember(d => d.PurchaseInvoice, opt => opt.Ignore());

        CreateMap<PaymentVoucher, PaymentVoucherDto>()
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.Name))
            .ForMember(d => d.MoneyAccountName, opt => opt.MapFrom(s => s.MoneyAccount.AccountName))
            .ForMember(d => d.AppliedInvoicesCount, opt => opt.MapFrom(s => s.AppliedPurchaseInvoices.Count));

        CreateMap<PaymentVoucher, PaymentVoucherDetailsDto>()
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.Name))
            .ForMember(d => d.MoneyAccountName, opt => opt.MapFrom(s => s.MoneyAccount.AccountName))
            .ForMember(d => d.Applications, opt => opt.MapFrom(s => s.AppliedPurchaseInvoices));

        CreateMap<PaymentVoucherPurchaseInvoice, PaymentVoucherApplicationDetailsDto>()
            .ForMember(d => d.PurchaseInvoiceId, opt => opt.MapFrom(s => s.PurchaseInvoiceId))
            .ForMember(d => d.InvoiceDate, opt => opt.MapFrom(s => s.PurchaseInvoice.Date))
            .ForMember(d => d.InvoiceNotes, opt => opt.MapFrom(s => s.PurchaseInvoice.Notes));

        CreateMap<PurchaseInvoice, OpenPurchaseInvoiceDto>()
            .ForMember(d => d.PurchaseInvoiceId, opt => opt.MapFrom(s => s.ID))
            .ForMember(d => d.InvoiceDate, opt => opt.MapFrom(s => s.Date))
            .ForMember(d => d.RemainingAmount, opt => opt.MapFrom(s => s.Debt != null ? s.Debt.Amount : 0));
    }
}
