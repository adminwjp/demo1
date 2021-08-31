using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.Application.Services.Dtos;
using Utility.Attributes;
using Utility.Demo.Application.Services;
using Utility.Demo.Domain.Entities;
using Utility.Domain.Entities;

namespace Utility.Demo
{
    [Route("api/v{version:apiVersion}/manufacturer/address")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(IResponseApi), 200)]
    [Transtation]
    public class ManufacturerAddressController : IsDefaultController<ManufacturerAddressEntity>
    {
        public ManufacturerAddressController(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
            ApiService = IocManager.Get<IIsDefaultAppService<ManufacturerAddressEntity>>("IsDefaultAppService");
        }
    }
    [Route("api/v{version:apiVersion}/agent/address")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(IResponseApi), 200)]
    [Transtation]
    public class AgentAddressController : IsDefaultController<AgentAddressEntity>
    {
        public AgentAddressController(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
            ApiService = IocManager.Get<IIsDefaultAppService<AgentAddressEntity>>("IsDefaultAppService");
        }
    }

    [Route("api/v{version:apiVersion}/seller/address")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(IResponseApi), 200)]
    [Transtation]
    public class SellerAddressController : IsDefaultController<SellerAddressEntity>
    {
        public SellerAddressController(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
            ApiService = IocManager.Get<IIsDefaultAppService<SellerAddressEntity>>("IsDefaultAppService");
        }
    }

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(IResponseApi), 200)]
    [Transtation]
    public class AddressController : IsDefaultController<AddressEntity>
    {
        public AddressController(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
            ApiService = IocManager.Get<IIsDefaultAppService<AddressEntity>>("IsDefaultAppService");
        }
    }
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(IResponseApi), 200)]
    [Transtation]
    public  class BankController: IsDefaultController<BankEntity>
    {
        public BankController(IHttpContextAccessor contextAccessor)
        {
            ContextAccessor = contextAccessor;
            ApiService=IocManager.Get<IIsDefaultAppService<BankEntity>>("IsDefaultAppService");
        }
    }
   
    public abstract class IsDefaultController<Bank>: Utility.AspNetCore.Controllers.BaseController<IIsDefaultAppService<Bank>, Bank,long>
        where Bank:class, IEntity<long>, IIsDefault, new()
    {
        [HttpPost("{userType}/Add")]
        public virtual async Task<ResponseApi> Add(string userType, [FromForm, FromBody] Bank obj)
        {
            return await Add(obj);
        }
        [HttpPost("{userType}/Modify")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Modify(string userType, [FromForm, FromBody] Bank obj)
        {
            return await Modify(obj);
        }
        [HttpGet("{userType}/Remove/{id}")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> Remove(string userType, long id)
        {
            return await Remove(id);
        }

        [HttpPost("{userType}/RemoveList")]
        public virtual async System.Threading.Tasks.Task<ResponseApi> RemoveList(string userType, [FromForm, FromBody] DeleteEntity<long> ids)
        {
            return await RemoveList(ids);
        }

        [HttpPost("{userType}/list/{page}/{size}")]
        public virtual async System.Threading.Tasks.Task<ResponseApi<ResultDto<Bank>>> GetResultByPage(string userType, [FromForm, FromBody] Bank obj, int page, int size)
        {
            return await GetResultByPage(obj,page,size);
        }
    }
}
