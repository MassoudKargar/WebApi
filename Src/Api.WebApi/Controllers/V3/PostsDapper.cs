
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Api.Data.Contracts;
using Api.Data.Contracts.DapperInterfaces;
using Api.Entities;
using Api.Entities.Posts;
using Api.WebApi.Models;
using Api.WebFramework.Api;

using AutoMapper;

using Ccms.Common.WebSetting;

using Microsoft.AspNetCore.Mvc;

namespace Api.WebApi.Controllers.V3
{
    [ApiVersion("3")]
    public class PostsDapper : V1.PostsDapper
    {
        public PostsDapper(IRepository<Post> repository, IMapper mapper, IDapperInterface<IEntity> dapperInterface)
            : base(repository, mapper, dapperInterface)
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

        public override Task<ActionResult<IEnumerable<PostSelectDto>>> Get(CancellationToken cancellationToken)
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
