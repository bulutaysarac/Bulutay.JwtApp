using Onion.JwtApp.Application.Enums;
using MediatR;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class RegisterAppUserCommandRequest : IRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int AppRoleId { get; set; } = (int)AppRoleType.Member;
    }
}
