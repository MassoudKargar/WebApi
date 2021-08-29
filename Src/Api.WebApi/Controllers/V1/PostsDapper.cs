using System;

using Api.Data.Contracts;
using Api.Data.Contracts.DapperInterfaces;
using Api.Entities;
using Api.Entities.Posts;
using Api.WebApi.Models;
using Api.WebFramework.Api;

using AutoMapper;

namespace Api.WebApi.Controllers.V1
{
    public class PostsDapper : CrudDapperController<PostDto, PostSelectDto, Post, Guid>
    {
        public PostsDapper(IRepository<Post> repository, IMapper mapper, IDapperInterface<IEntity> dapperInterface)
            : base(repository, mapper, dapperInterface)
        {
        }
    }
}
