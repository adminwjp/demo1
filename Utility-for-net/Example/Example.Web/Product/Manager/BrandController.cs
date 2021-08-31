using Microsoft.AspNetCore.Mvc;
using Product.Application.Services.Brands;
using Product.Domain.Entities;
using Product.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.Application.Services.Dtos;
using Utility.AspNetCore.Controllers;
using Utility.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Domain.Services;
using Utility.Ef;
using Utility.Ef.Uow;

namespace Product.Api.Manager
{ 
    // [Area("product")]
    [Route("admin/api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class BrandController : BaseController
    {
        IBrandAppService service;

        public BrandController(IBrandAppService service)
        {
            this.service = service;
            this.Service = service as DomainService;
        
        }


        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost("Add")]
        public virtual async Task<ResponseApi> Add([FromForm, FromBody] BrandDto obj)
        {
            var res = await service.InsertAsync(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost("Add_many")]
        public virtual async Task<ResponseApi> AddMany([FromForm, FromBody] ListDto<BrandDto> obj)
        {
            var res = await service.BatchInsertAsync(obj.Data.ToArray());
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost("Modify")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Modify([FromForm, FromBody] BrandDto obj)
        {
            var res = await service.UpdateAsync(obj);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }


        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet("Remove")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Remove(long id)
        {
            var res = await service.DeleteAsync(id);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }



        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost("RemoveList")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> RemoveList([FromForm, FromBody] DeleteEntity<long> ids)
        {
            var res = await service.DeleteListAsync(ids.Ids);
            return res > 0 ? ResponseApi.OkByEnglish : ResponseApi.FailByEnglish;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost("list/{page}/{size}")]
        public virtual async Task<ResponseApi<ResultDto<BrandDto>>> GetResultByPage([FromForm, FromBody] BrandDto obj, int page, int size)
        {
            var data = await service.FindResultByPageAsync(obj, page, size);
            return ResponseApi<ResultDto<BrandDto>>.CreateSuccess().SetData(data);
        }
        //方法名字 不能 相同 不然阻塞
        [HttpGet("list1/{page}/{size}")]
        public virtual async Task<ResponseApi<ResultDto<BrandDto>>> GetResultByPage(int page, int size)
        {
            //aop 阻塞 怎么执行的啊 
            var data = await service.FindResultByPageAsync(new BrandDto(), page, size);
            return ResponseApi<ResultDto<BrandDto>>.CreateSuccess().SetData(data);
        }
    }
}
