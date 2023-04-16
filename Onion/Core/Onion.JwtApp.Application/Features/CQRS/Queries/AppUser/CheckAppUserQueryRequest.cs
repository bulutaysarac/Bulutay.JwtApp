using MediatR;
using Onion.JwtApp.Application.Dto;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class CheckAppUserQueryRequest : IRequest<CheckAppUserResponseDto>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
