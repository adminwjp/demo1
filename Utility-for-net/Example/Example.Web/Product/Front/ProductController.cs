using Microsoft.AspNetCore.Mvc;
using Product.Application.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.AspNetCore.Controllers;
using Utility.Domain.Services;

namespace Product.Api.Front
{
   // [Area("product")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ProductController: BaseController
    {
        private readonly IProductAppService service;

        public ProductController(IProductAppService service)
        {
            this.service = service;
            this.Service = service as DomainService;
        }
        public class Msg
        {
            public string Str { get; set; }
        }
        [HttpPost("test")]
        public virtual string Test([FromBody]Msg msg)
        {
            return msg.Str.Replace("\"","\\\"");
        }

        [HttpGet("left_hot")]
        [ProducesResponseType(typeof(ResponseApi<IList<LeftHotProductOutput>>), 200)]
        public virtual ResponseApi<IList<LeftHotProductOutput>> LeftHotProducts()
        {
            var datas = service.LeftHotProducts();
            return ResponseApi<IList<LeftHotProductOutput>>.Create(Language.Chinese, Code.QuerySuccess).SetData(datas);
        }

        [HttpGet("hot")]
        [ProducesResponseType(typeof(ResponseApi<IList<LeftHotProductOutput>>), 200)]
        public virtual ResponseApi<IList<LeftHotProductOutput>> HotProducts()
        {
            var datas = service.HotProducts();
            return ResponseApi<IList<LeftHotProductOutput>>.Create(Language.Chinese, Code.QuerySuccess).SetData(datas);
        }

        [HttpGet("new")]
        [ProducesResponseType(typeof(ResponseApi<IList<LeftHotProductOutput>>), 200)]
        public virtual ResponseApi<IList<LeftHotProductOutput>> NewProducts()
        {
            var datas = service.NewProducts();
            return ResponseApi<IList<LeftHotProductOutput>>.Create(Language.Chinese, Code.QuerySuccess).SetData(datas);
        }

        [HttpGet("special")]
        [ProducesResponseType(typeof(ResponseApi<IList<LeftHotProductOutput>>), 200)]
        public virtual ResponseApi<IList<LeftHotProductOutput>> SpecialPriceProducts()
        {
            var datas = service.SpecialPriceProducts();
            return ResponseApi<IList<LeftHotProductOutput>>.Create(Language.Chinese, Code.QuerySuccess).SetData(datas);
        }
    }
}
