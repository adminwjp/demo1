using System.Collections.Generic;

namespace Utility.Demo.Application.Services.Dtos
{
#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
    //using AutoMapper;
    using Utility.Demo.Domain.Entities;
    using Utility;
   // [AutoMap(typeof(MenuEntity))]
#endif
    public class CreateElementMenuInput
    {
        public string Id { get; set; }
        /// <summary>名称</summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否折叠
        /// </summary>
        public virtual bool Collpse { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public virtual string Groups { get; set; }
        /// <summary>图标</summary>
        public virtual string Icon { get; set; }
        /// <summary>地址</summary>
        public virtual string Href { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        //form 绑定模型 必须 要使用这 否则绑定失败 body可以需要也可以不需要
        //[FromForm(Name = "parent_id")]

        public string ParentId { get; set; }

    }
}
