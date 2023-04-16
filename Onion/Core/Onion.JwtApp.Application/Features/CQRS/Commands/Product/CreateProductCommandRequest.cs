using MediatR;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class CreateProductCommandRequest : IRequest
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
