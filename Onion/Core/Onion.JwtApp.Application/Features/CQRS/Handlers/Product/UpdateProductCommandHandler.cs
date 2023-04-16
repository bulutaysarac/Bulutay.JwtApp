using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using MediatR;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if(entity != null)
            {
                entity.Price = request.Price;
                entity.CategoryId = request.CategoryId;
                entity.Stock = request.Stock;
                entity.Name = request.Name;
                await _repository.CommitAsync();
            }
        }
    }
}
