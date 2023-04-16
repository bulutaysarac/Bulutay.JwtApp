using AutoMapper;
using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using MediatR;
using Onion.JwtApp.Application.Dto;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class CheckUserRequestHandler : IRequestHandler<CheckAppUserQueryRequest, CheckAppUserResponseDto>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppRole> _roleRepository;
        private readonly IMapper _mapper;

        public CheckUserRequestHandler(IRepository<AppUser> appUserRepository, IRepository<AppRole> roleRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<CheckAppUserResponseDto> Handle(CheckAppUserQueryRequest request, CancellationToken cancellationToken)
        {
            var dto = new CheckAppUserResponseDto();
            var user = await _appUserRepository.GetByFilterAsync(x => x.Username == request.Username && x.Password == request.Password);
            if(user == null)
            {
                dto.IsExist = false;
            }
            else
            {
                dto = _mapper.Map<CheckAppUserResponseDto>(user);
                dto.IsExist = true;
                var role = await _roleRepository.GetByFilterAsync(x => x.Id == user.AppRoleId);
                dto.Role = role?.Definition;
            }
            return dto;
        }
    }
}
