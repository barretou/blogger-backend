
using AutoMapper;
using Blogger.Domain.Models;
using Blogger.Services.DTO;

namespace Blogger.Domain.Mapper
{
    public class PostProfile : Profile
    {
        public PostProfile() 
        {
            CreateMap<Post, PostDto>();
            CreateMap<Author, AuthorDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
