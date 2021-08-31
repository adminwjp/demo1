using Company.Domain.Entities;
using Company.Ef;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.AspNetCore.Controllers;
using Utility.Ef.Repositories;

namespace Company.Api.Front
{
    [Area("company")]
    [Route("company/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ImageController : BaseController
    {
      
    }
}
