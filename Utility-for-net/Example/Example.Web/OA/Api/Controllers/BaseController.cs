using System;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Serialization;
using OA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility.Domain.Repositories;
using Utility.Extensions;
using Utility.Json;
using Utility;
using N = NHibernate;
using Utility.Nhibernate.Repositories;
using Utility.Nhibernate;
using Utility.AspNetCore.Controllers;
using Utility.Domain.Entities;
// For more Entityrmation on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.Api.Controllers
{
    [Area("oa")]
    [Route("oa/admin/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class BaseController<T> : BaseControllerByRepository<IRepository<T,long>,T,long> where T: OA.Domain.Entities.BaseEntity, new()
    {
        protected readonly ILogger<BaseController<T>> Logger;
        public BaseController(ILogger<BaseController<T>> logger, IRepository<T,long> repository)
        {
            this.Logger = logger;
            if (repository == null)
            {
                var session = Utility.Ioc.AutofacIocManager.Instance.Get<SessionProvider>("OASession");
                this.Repository = new BaseNhibernateRepository<T, long>(session);
            }
            else
            {
                this.Repository = repository;
            }
        }
        [HttpPost("add")]
        public override ResponseApi Insert([FromForm] T obj)
        {
            //if (!ModelState.IsValid)
            //{
            //    return ResponseApi.Fail();
            //}
            obj.CreateDate = DateTime.Now;
            this.Repository.Insert(obj);
            return ResponseApi.CreateSuccess();
        }
    
        [HttpPost("edit")]
        public override ResponseApi Update([FromForm] T obj)
        {
            //if (!ModelState.IsValid)
            //{
            //    return ResponseApi.Fail();
            //}
            return Edited(obj);
        }
        protected virtual ResponseApi Edited(T obj)
        {
            var old = this.Repository.FindSingle(it => it.Id == obj.Id);
            obj.CreateDate = old.CreateDate;
            obj.UpdateDate = DateTime.Now;
            this.Repository.Update(obj);
            return ResponseApi.CreateSuccess();
        }
        [HttpPost("delete")]
        public virtual ResponseApi Delete([FromForm]DeleteEntity<long> delEntry)
        {
            Expression<Func<T, bool>> where = null;
            if (delEntry.Id>0)
            {
                where = where.Or(it => it.Id == delEntry.Id);
            }
            else if (delEntry.Ids != null && delEntry.Ids.Length > 0)
            {
                //多条件不支持 删除操作
                //NHibernate.ISession session = HttpContext.RequestServices.GetService<NHibernate.ISession>();
                //NHibernate.ICriteria criteria= session.CreateCriteria<T>();
                //NHibernate.Criterion.AbstractCriterion abstractCriterion=null;
                //bug
                foreach (var item in delEntry.Ids)
                {
                    // where = where.Or(it => it.Id == item);
                    this.Repository.Delete(it=>it.Id==item);
                    //if(abstractCriterion==null)
                    //{
                    //    abstractCriterion = NHibernate.Criterion.Expression.IdEq(item);
                    //}
                    //else
                    //{
                    //    abstractCriterion |= NHibernate.Criterion.Expression.IdEq(item);
                    //}
                }
                //criteria.Add(abstractCriterion);
                return ResponseApi.CreateSuccess();
            }
            else
            {
                return ResponseApi.CreateFail();
            }
            this.Repository.Delete(where);
            return ResponseApi.CreateSuccess();
        }
        [HttpPost("get")]
        public virtual ResponseApi Get()
        {
            var data = this.Repository.Query(null).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
    }

         
}
