using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using System.Xml.Serialization;
using Utility.Domain.Repositories;
using OA.Api;
using Microsoft.AspNetCore.Builder;
using N = NHibernate;
using Utility.AspNetCore.Controllers;
using Utility.Application.Services;

namespace OA.Api.Controllers
{
	[Area("oa")]
	[Route("oa/admin/api/v{version:apiVersion}/account_item")]
	[ApiController]
	[ApiVersion("1.0")]
	[ProducesResponseType(typeof(ResponseApi), 200)]
	public class AccountItemController : BaseController<AccountItemEntity>
	{
		//public AuthorityController(ILogger<AuthorityController> logger, IRepository<AuthorityOperatorEntity,long> repository) : base(logger, repository)
		//{

		//}
		public AccountItemController(ILogger<AccountItemController> logger) : base(logger, null)
		{

		}

        public override ResponseApi Update([FromForm,FromBody ] AccountItemEntity obj)
        {
			base.Repository.Update(it => it.Id == obj.Id, it => new AccountItemEntity() { Name = obj.Name, Type = obj.Type, Utit = obj.Utit, UpdateDate = DateTime.Now });
			return ResponseApi.CreateSuccess();
		}
		[HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Query(null).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
	}
}