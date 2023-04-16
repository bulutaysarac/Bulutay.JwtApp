using MediatR;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class CreateCategoryCommandRequest : IRequest
    {
        public string? Definition { get; set; }
    }
}
