using Company.Domain.Entities;
using Company.Ef;
using Example.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility;
using Utility.Application.Services.Dtos;
using Utility.AspNetCore.Controllers;
using Utility.Attributes;
using Utility.Domain.Entities;
using Utility.Ef;
using Utility.Ef.Repositories;
using Utility.Extensions;

namespace Company.Api.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Transtation]
    public class BaseController<Entity> : BaseControllerByRepository<BaseEfRepository<CompanyDbContext, Entity, long>, Entity, long>
        where Entity : BaseEntity ,new()
    {
        //create autofac ex
        //protected BaseEfRepository<CompanyDbContext, T, long> Repository;
        public BaseController(IHttpContextAccessor contextAccessor
            //BaseEfRepository<CompanyDbContext, Entity, long> repository
            ) :base(null)
        {
            ContextAccessor = contextAccessor;
            //ex 哪里 冲突 排查麻烦 要么 手动注册 ioc 不要用泛型
            //Repository = IocManager.Get<BaseEfRepository<CompanyDbContext, Entity, long>>("CompanyRepository");
            
            Repository = new BaseEfRepository<CompanyDbContext, Entity, long>(IocManager.Get<DbContextProvider<CompanyDbContext>>());
            // Repository = repository;
        }
        [HttpGet("test")]
        public virtual bool Test()
        {
            return true;
        }
        //public override ResponseApi Insert([FromBody, FromForm] Entity obj)
        //{
        //    throw new NotImplementedException();
        //}
        //public override Task<ResponseApi> Modify([FromBody, FromForm] Entity obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task<ResponseApi> Remove(long id)
        //{
        //    throw new NotImplementedException();
        //}
        //public override Task<ResponseApi> RemoveList([FromBody, FromForm] DeleteEntity<long> ids)
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task<ResponseApi<List<Entity>>> GetList([FromBody, FromForm] Entity obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public override Task<ResponseApi<long>> GetCount([FromBody, FromForm] Entity obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public override ResponseApi<List<Entity>> FindList([FromBody, FromForm] Entity obj)
        //{
        //    throw new NotImplementedException();
        //}
        //public override ResponseApi<long> Count([FromBody, FromForm] Entity obj)
        //{
        //    throw new NotImplementedException();
        //}
        //public override Task<ResponseApi<ResultDto<Entity>>> GetResultByPage([FromBody, FromForm] Entity obj, int page, int size)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpGet("status/{id}/{enable}")]
        public virtual ResponseApi Status(long id,int enable)
        {
            Repository.Update(it => it.Id==id, it => new Entity() {Enable=enable==1 });
            return ResponseApi.OkByEnglish;
        }
        [HttpPost("status")]
        public virtual ResponseApi Status1([FromForm,FromBody]UpdateStatues updateStatues)
        {
            if (updateStatues.Ids.Count > 0)
            {
                return ResponseApi.FailByEnglish;
            }
          
            Expression<Func<Entity, bool>> where =it=> it.Id == updateStatues.Ids[0];
            for (int i = 1; i < updateStatues.Ids.Count; i++)
            {
                where =where.Or(it => it.Id == updateStatues.Ids[i]);
            }
            Repository.Update(where, it => new Entity() { Enable =updateStatues.Enable  });
            return ResponseApi.OkByEnglish;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost("list/{page}/{size}")]
        public virtual ResponseApi<ResultDto<Entity>> FindResult([FromForm, FromBody] Entity obj,int page,int size)
        {
            var data = Repository.Query().ToList();
            var count = Repository.Count();
            var res = new ResultDto<Entity>() { Data=data,Result=new PageResultDto() { Records=count} };
            return ResponseApi<ResultDto<Entity>>.CreateSuccess().SetData(res);
        }
    }
    public class UpdateStatues
    {
        public List<long> Ids { get; set; }
        public bool Enable { get; set; }
    }
}
