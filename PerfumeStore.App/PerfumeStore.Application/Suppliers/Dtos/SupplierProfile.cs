using AutoMapper;
using PerfumeStore.Application.Customers.Commands.CreateCustomer;
using PerfumeStore.Application.Customers.Commands.UpdateCustomer;
using PerfumeStore.Application.Suppliers.Commands.CreateSupplier;
using PerfumeStore.Application.Suppliers.Commands.UpdateSupplier;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.Suppliers.Dtos {
    public class SupplierProfile : Profile {
        public SupplierProfile() {
            CreateMap<Supplier, SupplierDto>()
                .ForMember(dest => dest.TotalPurchaseInvoices, opt => opt.MapFrom(src => src.PurchaseInvoices.Count))
                .ForMember(d => d.TotalDebt, opt => opt.MapFrom(s => s.Debts.Sum(x => x.Amount)));
            CreateMap<CreateSupplierCommand, Supplier>();
            CreateMap<UpdateSupplierCommand, Supplier>();
        }
    }
}
