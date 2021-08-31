using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialContact.Application.Services.Works;
using SocialContact.Application.Services.Works.Dtos;
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
    [Route("admin/api/v1/Work")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]

    public class WorkCtrl : BaseController<WorkAppService, IRepository<WorkEntity>, WorkEntity, CreateWorkInput, UpdateWorkInput, WorkInput, WorkOutput>
    {
        public WorkCtrl(WorkAppService service) : base(service)
        {
            //base.ObjectMapper = objectMapper;
            //base.IsCustomValidator = true;
            //base.PageName = "user_level";
        }
      
    }
}
