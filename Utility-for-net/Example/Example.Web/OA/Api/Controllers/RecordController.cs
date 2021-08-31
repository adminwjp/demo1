using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OA.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Domain.Repositories;
using Utility.IO;
using OA.Api;
using N = NHibernate;
namespace OA.Api.Controllers
{
    [Area("oa")]
    [Route("oa/admin/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class RecordController : BaseController<RecordEntity>
    {
        //public RecordController(ILogger<RecordController> logger, IRepository<RecordEntity,long> repository) : base(logger, repository)
        //{

        //}
        public RecordController(ILogger<RecordController> logger) : base(logger, null)
        {

        }
        [HttpPost("add")]
        public override ResponseApi Insert([FromForm] RecordEntity obj)
        {
            //if (!ModelState.IsValid)
            //{
            //    return ResponseApi.Fail();
            //}
            if (Request.Form.Files.Count != 1)
            {
                return ResponseApi.Create( Language.Chinese,Code.UploadFileFail);
            }
            var file = Request.Form.Files[0];
            if (file.Name.ToLower() != "photo")
            {
                return ResponseApi.Create(Language.Chinese, Code.UploadFileFail);
            }
            using Stream stream = file.OpenReadStream();
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer,0,buffer.Length);
            FileHelper.WriteFile(  "wwwroot/imgs/" + file.FileName, buffer);
            obj.Photo = "imgs/" + file.FileName;
            obj.CreateDate = DateTime.Now;
            this.Repository.Insert(obj);
            return ResponseApi.CreateSuccess();
        }
        [HttpPost("edit")]
        public override ResponseApi Update([FromForm] RecordEntity obj)
        {
            if (Request.Form.Files.Count != 1)
            {
                return ResponseApi.Create(Language.Chinese, Code.UploadFileFail);
            }
            var file = Request.Form.Files[0];
            if (file.Name.ToLower() != "photo")
            {
                return ResponseApi.Create(Language.Chinese, Code.UploadFileFail);
            }
            using Stream stream = file.OpenReadStream();
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            FileHelper.WriteFile("wwwroot/imgs/" + file.FileName, buffer);
            obj.Photo = "imgs/" + file.FileName;
            obj.UpdateDate = DateTime.Now;
            this.Repository.Update(it=>it.Id==obj.Id,it=>obj);
            return ResponseApi.CreateSuccess();
        }
        [HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Query(null).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
		//批准人
		[HttpGet("ratifier")]
        public ResponseApi Ratifier()
        {
            var data = base.Repository.Query(null).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
    }
}