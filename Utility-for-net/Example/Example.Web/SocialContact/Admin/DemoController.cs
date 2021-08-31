using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialContact.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utility;
using Utility.Json;
using Utility.Demo.Application.Services.Dtos;
using Utility.Extensions;
using SocialContact.Domain.Entities;
using Utility.Attributes;
using Utility.AspNetCore;
using Utility.Ioc;
using Utility.Demo.Application.Services;
using Utility.Demo;
using Utility.Application.Services.Dtos;
using Utility.Application.Services;
using Utility.Demo.Domain.Entities;
using UserEntity = SocialContact.Domain.Entities.UserEntity;
using Utility.Security.Extensions;
using Utility.IO;
using Utility.Cache;
using Example.Web;
using Utility.Domain.Repositories;
using Utility.AspNetCore.Controllers;

namespace SocialContact.Admin
{


    [Area("social_contact")]
    [Route("social_contact/admin/api/v{version:apiVersion}/menu")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SocialContactMenuController : MenuController
    {
        public SocialContactMenuController(MenuService service):base(service)
        {

        }
    }

    [Area("social_contact")]
    [Route("social_contact/admin/api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SourceMaterial1Controller : Utility.Demo.SourceMaterialController
    {
        
        static string Path = "E:/work/utility/Utility-for-go/static/s/img/";
        public SourceMaterial1Controller(
            CrudAppService<SourceMaterialEntity, long> service,
            ICacheContent cache) : base(service)
        {
            //必须要 用 ioc 不然 aop 事务 失效  要 么 自定义 aop
            //ex 
            //ApiService = Program.IocManager.Get<CrudApplicationService<SourceMaterialEntity, long>>();
           // ApiService = new CrudApplicationService<SourceMaterialEntity, long>(Program.IocManager.Get<IRepository<SourceMaterialEntity, long>>());
            
            this.Cache = cache;
        }
        [HttpPost("upload")]
        public virtual dynamic Upload()
        {
            if (Request.Form != null && Request.Form.Files != null && Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];

                var id = $"{file.FileName}_{Guid.NewGuid().ToString()}".Sha1();
                var src = id + "." + file.FileName.Split('.').LastOrDefault();
                FileHelper.WriteFile(Path + src, StreamHelper.GetBuffer(file.OpenReadStream()));
                var s = new SourceMaterialEntity()
                {
                    Src = src,
                    Key = id,
                };
                ApiService.Insert(s); 
                Cache.Set(id, s,DateTime.Now.AddDays(365));
                return new { status = true, id };
            }
            return new { status = false };
        }

        [HttpGet("source/{id}")]
        public virtual dynamic Get(string id)
        {
            var s = Cache.Get<SourceMaterialEntity>(id);
            if (s == null)
            {
                Console.WriteLine("cache img is null ");
                s = ApiService.FindSingle(it => it.Key == id);
            }
            if (s == null)
            {
                return NotFound();
            }
            return new FileContentResult(System.IO.File.ReadAllBytes(Path + s.Src), "*/*");
        }
    }
}
