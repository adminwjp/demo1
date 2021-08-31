using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialContact.Application.Services;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utility;
using Utility.Application.Services.Dtos;
using Utility.AspNetCore.Controllers;
using Utility.Attributes;
using Utility.Domain.Entities;
using Utility.Json;

namespace SocialContact.Admin
{
    [Area("social_contact")]
    [Route("social_contact/admin/api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SocialContactCatagoryController : BaseController
    {
        private CatagoryAppService catagoryAppService;
        private ILogger<SocialContactCatagoryController> logger;

        public SocialContactCatagoryController(CatagoryAppService catagoryAppService, ILogger<SocialContactCatagoryController> logger)
        {
            this.catagoryAppService = catagoryAppService;
            this.Service = catagoryAppService;
            this.logger = logger;
        }

        [HttpPost("catagory/list/{page}/{size}")]
        [HttpGet("catagory/list/{page}/{size}")]
        public virtual ResponseApi List(int page, int size)
        {
            var data = catagoryAppService.FindListByPage(null, page, size);
            var count = catagoryAppService.Count((CatagoryEntity)null);
            return ResponseApi.CreateSuccess().SetData(new ResultDto<CatagoryEntity>(data, page, size, count));
        }

        [HttpPost("role/add")]
        public virtual ResponseApi AddRole([FromForm]CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Role;
            catagoryAppService.Insert(catagory);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("role/update")]
        public virtual ResponseApi UpdateRole([FromForm] CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Role;
            catagoryAppService.Update(catagory);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("role/remove/{id}")]
        public virtual ResponseApi RemoveRole(long id)
        {
            catagoryAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("role/remove")]
        public virtual ResponseApi RemoveRole([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            catagoryAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("role/list/{page}/{size}")]
        public virtual ResponseApi ListRole(int page, int size)
        {
            var data = catagoryAppService.List(CatalogFlag.Role,page, size);
            return ResponseApi.CreateSuccess().SetData(data);
        }
        [HttpGet("role")]
        public virtual ResponseApi Role()
        {
            var data = catagoryAppService.Query(it=>it.Flag==CatalogFlag.Role).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }

        [HttpPost("file_catagory/add")]
        public virtual ResponseApi AddFileCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Flag = CatalogFlag.File;
            catagoryAppService.Insert(catagory);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("file_catagory/update")]
        public virtual ResponseApi UpdateFileCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Flag = CatalogFlag.File;
            catagoryAppService.Update(catagory);
            return ResponseApi.OkByEnglish;
        }
        [HttpGet("file_catagory/remove/{id}")]
        public virtual ResponseApi RemoveFileCatagory(long id)
        {
            catagoryAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("file_catagory/remove")]
        public virtual ResponseApi RemoveFileCatagory([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            catagoryAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }
        [HttpGet("file_catagory/list/{page}/{size}")]
        public virtual ResponseApi ListFileCatagory(int page, int size)
        {
            var data = catagoryAppService.List(CatalogFlag.File, page, size);
            return ResponseApi.CreateSuccess().SetData(data);
        }

        [HttpPost("job_catagory/add")]
        public virtual ResponseApi AddJobCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Job;
            catagoryAppService.Insert(catagory);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("job_catagory/update")]
        public virtual ResponseApi UpdateJobCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Job;
            catagoryAppService.Update(catagory);
            return ResponseApi.OkByEnglish;
        }
        [HttpGet("job_catagory/remove/{id}")]
        public virtual ResponseApi RemoveJobCatagory(long id)
        {
            catagoryAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("job_catagory/remove")]
        public virtual ResponseApi RemoveJobCatagory([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            catagoryAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }
        [HttpGet("job_catagory/list/{page}/{size}")]
        public virtual ResponseApi ListJobCatagory(int page, int size)
        {
            var data = catagoryAppService.List(CatalogFlag.Job, page, size);
            return ResponseApi.CreateSuccess().SetData(data);
        }

        [HttpPost("skill_catagory/add")]
        public virtual ResponseApi AddSkillCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Job;
            catagoryAppService.Insert(catagory);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("skill_catagory/update")]
        public virtual ResponseApi UpdateSkillCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Job;
            catagoryAppService.Update(catagory);
            return ResponseApi.OkByEnglish;
        }
        [HttpGet("skill_catagory/remove/{id}")]
        public virtual ResponseApi RemoveSkillCatagory(long id)
        {
            catagoryAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("skill_catagory/remove")]
        public virtual ResponseApi RemoveSkillCatagory([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            catagoryAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }
        [HttpGet("skill_catagory/list/{page}/{size}")]
        public virtual ResponseApi ListSkillCatagory(int page, int size)
        {
            var data = catagoryAppService.List(CatalogFlag.Skill, page, size);
            return ResponseApi.CreateSuccess().SetData(data);
        }

        [HttpPost("like_catagory/add")]
        public virtual ResponseApi AddLikeCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Like;
            catagoryAppService.Insert(catagory);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("like_catagory/update")]
        public virtual ResponseApi UpdateLikeCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Like;
            catagoryAppService.Update(catagory);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("like_catagory/remove/{id}")]
        public virtual ResponseApi RemoveLikeCatagory(long id)
        {
            catagoryAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("like_catagory/remove")]
        public virtual ResponseApi RemoveLikeCatagory([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            catagoryAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }
        [HttpGet("like_catagory/list/{page}/{size}")]
        public virtual ResponseApi ListLikeCatagory(int page, int size)
        {
            var data = catagoryAppService.List(CatalogFlag.Like, page, size);
            return ResponseApi.CreateSuccess().SetData(data);
        }

        [HttpPost("tag_catagory/add")]
        public virtual ResponseApi AddTagCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Tag;
            catagoryAppService.Insert(catagory);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("tag_catagory/update")]
        public virtual ResponseApi UpdateTagCatagory([FromForm] CatagoryEntity catagory)
        {
            catagory.Accept = null;
            catagory.Flag = CatalogFlag.Tag;
            catagoryAppService.Update(catagory);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("tag_catagory/remove/{id}")]
        public virtual ResponseApi RemoveTagCatagory(long id)
        {
            catagoryAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("tag_catagory/remove")]
        public virtual ResponseApi RemoveTagCatagory([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            catagoryAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("tag_catagory/list/{page}/{size}")]
        public virtual ResponseApi ListTagCatagory(int page, int size)
        {
            var data = catagoryAppService.List(CatalogFlag.Tag, page, size);
            return ResponseApi.CreateSuccess().SetData(data);
        }
       
    }
}
