
using Api.Data.Contracts;
using Api.Entities.Posts;
using Api.WebApi.Models;
using Api.WebFramework.Api;

using AutoMapper;

namespace Api.WebApi.Controllers.V1
{
    public class Categories : CrudController<CategoryDto, Category>
    {
        public Categories(IRepository<Category> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}
