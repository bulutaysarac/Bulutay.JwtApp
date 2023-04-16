using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using MediatR;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;

        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            //Connected
            var entity = await _repository.GetByIdAsync(request.Id);
            if(entity !=  null)
            {
                entity.Definition = request.Definition;
                await _repository.CommitAsync();
            }
        }
    }
}
