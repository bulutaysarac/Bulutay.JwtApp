using AutoMapper;
using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using MediatR;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class RegisterAppUserCommandHandler : IRequestHandler<RegisterAppUserCommandRequest>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IMapper _mapper;

        public RegisterAppUserCommandHandler(IRepository<AppUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(RegisterAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(_mapper.Map<AppUser>(request));
        }
    }
}
