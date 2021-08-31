using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialContact.Application.Services;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utility;
using Utility.Attributes;
using Utility.Domain.Entities;
using Utility.Json;

namespace SocialContact.Front
{
    [Area("social_contact")]
    [Route("social_contact/front/api/v{version:apiVersion}")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class SocialContactRelationController : ControllerBase
    {
        private RelationAppService relationAppService;

        public SocialContactRelationController(RelationAppService relationAppService)
        {
            this.relationAppService = relationAppService;
        }

        [HttpPost("job/add")]
        public virtual ResponseApi AddJob([FromForm] RelationEntity relation)
        {
            relation.Flag = CatalogFlag.Job;
            relationAppService.Insert(relation);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("job/update")]
        public virtual ResponseApi UpdateJob([FromForm] RelationEntity relation)
        {
            relation.Flag = CatalogFlag.Job;
            relationAppService.Update(relation);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("job/remove/{id}")]
        public virtual ResponseApi RemoveJob(long id)
        {
            relationAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("job/remove")]
        public virtual ResponseApi RemoveJob([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            relationAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("job/list")]
        public virtual ResponseApi ListJob()
        {
            var data = relationAppService.Catagory(CatalogFlag.Job);
            return ResponseApi.CreateSuccess().SetData(data);
        }

        [HttpPost("tag/add")]
        public virtual ResponseApi AddTag([FromForm] RelationEntity relation)
        {
            relation.Flag = CatalogFlag.Tag;
            relationAppService.Insert(relation);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("tag/update")]
        public virtual ResponseApi UpdateTag([FromForm] RelationEntity relation)
        {
            relation.Flag = CatalogFlag.Tag;
            relationAppService.Update(relation);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("tag/remove/{id}")]
        public virtual ResponseApi RemoveTag(long id)
        {
            relationAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("tag/remove")]
        public virtual ResponseApi RemoveTag([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            relationAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("tag/list")]
        public virtual ResponseApi ListTag()
        {
            var data = relationAppService.Catagory(CatalogFlag.Tag);
            return ResponseApi.CreateSuccess().SetData(data);
        }

        [HttpPost("skill/add")]
        public virtual ResponseApi AddSkill([FromForm] RelationEntity relation)
        {
            relation.Flag = CatalogFlag.Skill;
            relationAppService.Insert(relation);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("skill/update")]
        public virtual ResponseApi UpdateSkill([FromForm] RelationEntity relation)
        {
            relation.Flag = CatalogFlag.Skill;
            relationAppService.Update(relation);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("skill/remove/{id}")]
        public virtual ResponseApi RemoveSkill(long id)
        {
            relationAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("skill/remove")]
        public virtual ResponseApi RemoveSkill([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            relationAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("skill/list")]
        public virtual ResponseApi ListSkill()
        {
            var data = relationAppService.Catagory(CatalogFlag.Tag);
            return ResponseApi.CreateSuccess().SetData(data);
        }

        [HttpPost("like/add")]
        public virtual ResponseApi AddLike([FromForm] RelationEntity relation)
        {
            relation.Flag = CatalogFlag.Like;
            relationAppService.Insert(relation);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("like/update")]
        public virtual ResponseApi UpdateLike([FromForm] RelationEntity relation)
        {
            relation.Flag = CatalogFlag.Like;
            relationAppService.Update(relation);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("like/remove/{id}")]
        public virtual ResponseApi RemoveLike(long id)
        {
            relationAppService.Delete(id);
            return ResponseApi.OkByEnglish;
        }

        [HttpPost("like/remove")]
        public virtual ResponseApi RemoveLike([FromForm] DeleteEntity<long> delete)
        {
            if (delete == null || delete.Ids == null || delete.Ids.Length == 0)
            {
                return ResponseApi.FailByEnglish;
            }
            relationAppService.DeleteList(delete.Ids);
            return ResponseApi.OkByEnglish;
        }

        [HttpGet("like/list")]
        public virtual ResponseApi ListLike()
        {
            var data = relationAppService.Catagory(CatalogFlag.Like);
            return ResponseApi.CreateSuccess().SetData(data);
        }
    }
}
