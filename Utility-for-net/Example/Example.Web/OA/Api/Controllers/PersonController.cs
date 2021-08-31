using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Domain.Repositories;
using OA.Api;
using N = NHibernate;
namespace OA.Api.Controllers
{
    [Area("oa")]
    [Route("oa/admin/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class PersonController : BaseController<PersonEntity>
    {
        //public PersonController(ILogger<PersonController> logger, IRepository<PersonEntity,long> repository) : base(logger, repository)
        //{

        //}
        public PersonController(ILogger<PersonController> logger) : base(logger, null)
        {

        }
        protected override ResponseApi Edited(PersonEntity obj)
        {
            this.Repository.Update(it => it.Id == obj.Id, it => new PersonEntity() { UpdateDate = DateTime.Now, Address=obj.Address, ComputerGrate=obj.ComputerGrate, Email=obj.Email,
             GraduateDate=obj.GraduateDate, GraduateSchool=obj.GraduateSchool, Handset=obj.Handset, Likes=obj.Likes, OnesStrongSuit=obj.OnesStrongSuit, PartyMemberDate=obj.PartyMemberDate,
             Postlacode=obj.Postlacode, QQ=obj.QQ, SecondSchoolAge= obj.SecondSchoolAge, SecondSpeciaity=obj.SecondSpeciaity, Telphone= obj.Telphone, UserId=obj.UserId});
            return ResponseApi.CreateSuccess();
        }
    }
}