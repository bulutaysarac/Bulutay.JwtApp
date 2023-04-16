using MediatR;
using Onion.JwtApp.Application.Dto;

namespace Onion.JwtApp.Application.Features.CQRS
{
    public class GetCategoriesQueryRequest : IRequest<List<CategoryListDto>>
    {

    }
}
