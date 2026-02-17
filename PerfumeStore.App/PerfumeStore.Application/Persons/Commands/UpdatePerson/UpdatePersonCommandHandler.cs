using AutoMapper;
using MediatR;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Exceptions;
using PerfumeStore.Domain.Repositories;

namespace PerfumeStore.Application.Persons.Commands.UpdatePerson {
    public class UpdatePersonCommandHandler(IPersonsRepository personsRepository, IMapper mapper) : IRequestHandler<UpdatePersonCommand> {
        public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken) {
            var person = await personsRepository.GetByPhoneNoAsync(request.Phone);
            if (person is null) 
                throw new NotFoundException(nameof(Person), request.Phone.ToString());
            mapper.Map(request, person);
            await personsRepository.SaveChangesAsync();
        }
    }
}
