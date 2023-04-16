using AutoMapper;
using Onion.JwtApp.Application.Dto;
using Onion.JwtApp.Application.Features.CQRS;
using Onion.JwtApp.Domain.Entities;

namespace Onion.JwtApp.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<Category, CreateCategoryCommandRequest>().ReverseMap();
        }
    }
}
