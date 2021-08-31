using Microsoft.AspNetCore.Mvc;
using Product.Application.Services.ProductCatagories;
using System.Collections.Generic;
using Utility;
using Utility.AspNetCore.Controllers;
using Utility.Domain.Services;

namespace Product.Api.Front
{
  //  [Area("product")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ProductCatagoryController: BaseController
    {
        //IProductCatagoryAppService
        private readonly IProductCatagoryAppService service;

        public ProductCatagoryController(IProductCatagoryAppService service)
        {
            this.service = service;
            this.Service = service as DomainService;
        }


        [HttpGet("get")]
        [ProducesResponseType(typeof(ResponseApi<IList<ProductCatagoryOutput>>), 200)]
        public virtual ResponseApi<IList<ProductCatagoryOutput>> Bottom()
        {
            var datas = service.Catagories();
            return ResponseApi<IList<ProductCatagoryOutput>>.Create(Language.Chinese,Code.QuerySuccess).SetData(datas);
        }

        [HttpGet("navgation")]
        [ProducesResponseType(typeof(ResponseApi<IList<ProductCatagoryOutput>>), 200)]
        public virtual ResponseApi<IList<NavgationOutput>> NavgationCatagories()
        {
            var datas = service.NavgationCatagories();
            return ResponseApi<IList<NavgationOutput>>.Create(Language.Chinese, Code.QuerySuccess).SetData(datas);
        }

       
        [ProducesResponseType(typeof(ResponseApi<IList<ProductCatagoryOutput>>), 200)]
        [HttpGet("bottom")]
        public virtual ResponseApi<IList<BottomOutput>> Catalogs()
        {
            var datas = service.Bottom();
            return ResponseApi<IList<BottomOutput>>.Create(Language.Chinese, Code.QuerySuccess).SetData(datas);
        }

        [ProducesResponseType(typeof(ResponseApi<IList<ProductCatagoryOutput>>), 200)]
        [HttpGet("bottom_link")]
        public virtual ResponseApi<IList<BottomNavgationOutput>> BottomLink()
        {
            var datas = service.BottomLink();
            return ResponseApi<IList<BottomNavgationOutput>>.Create(Language.Chinese, Code.QuerySuccess).SetData(datas);
        }
    }
}
