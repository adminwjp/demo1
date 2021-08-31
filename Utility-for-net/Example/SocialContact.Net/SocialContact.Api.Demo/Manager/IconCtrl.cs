using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialContact.Application.Services.Icons;
using SocialContact.Application.Services.Icons.Dtos;
using SocialContact.Domain.Entities;
using SocialContact.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enums;
using Utility.Response;

namespace SocialContact.Manager
{

    [Area("admin")]
    [Route("admin/api/v1/icon")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]

    public class IconCtrl : BaseController<IconAppService, IRepository<IconEntity>, IconEntity, CreateIconInput, UpdateIconInput, IconInput, IconOutput>
    {
        public IconCtrl(IconAppService service) : base(service)
        {
            //base.ObjectMapper = objectMapper;
            //base.IsCustomValidator = true;
            //base.PageName = "user_level";
        }

        [HttpGet("category")]
        public override ResponseApi Category()
        {
            var data = service.Find();
            return ResponseApi.Create(GetLanguage(), Code.QuerySuccess).SetData(data);
        }
    }
}
