using AutoMapper;
using PerfumeStore.Application.ExpenseTypes.Commands.CreateExpenseType;
using PerfumeStore.Application.ExpenseTypes.Commands.UpdateExpenseType;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.Application.ExpenseTypes.Dtos {
    public class ExpenseTypesProfile : Profile {
        public ExpenseTypesProfile() {
            CreateMap<ExpenseType, ExpenseTypeDto>();
            CreateMap<CreateExpenseTypeCommand, ExpenseType>();
            CreateMap<UpdateExpenseTypeCommand, ExpenseType>();
        }
    }
}
