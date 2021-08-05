using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Api.Data.Contracts;
using Api.Entities.Posts;
using Api.WebApi.Models;
using Api.WebFramework.Api;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace Api.WebApi.Controllers.V2
{
    [ApiVersion("2")]
    public class Posts : V1.Posts
    {
        public Posts(IRepository<Post> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }

        public override Task<ApiResult<PostSelectDto>> Create(PostDto dto, CancellationToken cancellationToken)
        {
            return base.Create(dto, cancellationToken);
        }

        public override Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }

        public override Task<ActionResult<List<PostSelectDto>>> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }

        public override Task<ApiResult<PostSelectDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }

        public override Task<ApiResult<PostSelectDto>> Update(Guid id, PostDto dto, CancellationToken cancellationToken)
        {
            return base.Update(id, dto, cancellationToken);
        }
    }
}
