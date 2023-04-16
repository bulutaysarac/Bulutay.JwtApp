using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using MediatR;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public DeleteProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if(entity != null)
            {
                await _repository.RemoveAsync(entity);
            }
        }
    }
}
