using System;

using Api.Data.Contracts;
using Api.Entities.Posts;
using Api.WebApi.Models;
using Api.WebFramework.Api;

using AutoMapper;

namespace Api.WebApi.Controllers.V1
{
    public class Posts : CrudController<PostDto, PostSelectDto, Post, Guid>
    {
        public Posts(IRepository<Post> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
