using Microsoft.AspNetCore.Mvc;
using Product.Application.Services.ProductCatagoryAttributes;
using Product.Domain.Entities;
using System.Threading.Tasks;
using Utility;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using Product.Application.Services.ProductCatagoryAttributes.Dtos;
using System.Collections.Generic;
using Utility.AspNetCore.Controllers;
using Utility.Domain.Services;

namespace Product.Api.Manager
{
    [Route("api/admin/v{version:apiVersion}/catalog_attribute")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ProductCatagoryAttributeController : BaseController
    {
        protected IProductCatagoryAttributeAppService ApiService { get; set; }
        public ProductCatagoryAttributeController(IProductCatagoryAttributeAppService service)
        {
            this.ApiService = service;
            this.Service = service as DomainService;
        }

       
        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost("Add")]
        public virtual async Task<ResponseApi> Add([FromForm, FromBody] CreateCatagoryAttributeInput obj)
        {
            var res = await ApiService.InsertAsync(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }



        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost("Modify")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Modify([FromForm, FromBody] UpdateCatagoryAttributeInput obj)
        {
            var res = await ApiService.UpdateAsync(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }


        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet("Remove")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Remove(long id)
        {
            var res = await ApiService.DeleteAsync(id);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }



        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost("RemoveList")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> RemoveList([FromForm, FromBody] DeleteEntity<long> ids)
        {
            var res = await ApiService.DeleteListAsync(ids.Ids);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<ResultDto<GetCatagoryAttributeOutput>>> GetResultByPage([FromForm, FromBody] GetCatagoryAttributeInput obj, int page, int size)
        {
            var data = await ApiService.FindListByPageAsync(obj, page, size);
            var count = await ApiService.CountAsync(obj);
            var res = new ResultDto<GetCatagoryAttributeOutput>(data, page, size, count);
            return ResponseApi<ResultDto<GetCatagoryAttributeOutput>>.CreateSuccess().SetData(res);
        }

        [HttpGet("catalog")]
        public virtual ResponseApi<List<GetCatagoryAttributeOutput>> FindCatalog()
        {
            var res = ApiService.FindList(null);
            return ResponseApi<List<GetCatagoryAttributeOutput>>.CreateSuccess().SetData(res);
        }

    }
}
