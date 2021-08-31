using System;

namespace Utility.Demo.Application.Services.Dtos
{
#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
    //using AutoMapper;
    using Utility.Demo.Domain.Entities;
    using Utility;
   // [AutoMap(typeof(MenuEntity))]
#endif
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
    [Serializable]
#endif
    public class ElementMenuDto
    {
        public string Id { get; set; }
        /// <summary>名称</summary>
        public string Name { get; set; }
        /// <summary>图标</summary>
        public string Icon { get; set; }
        /// <summary>地址</summary>
        public string Href { get; set; }
        /// <summary>html id选择器名称</summary>
        /// <summary>子菜单 </summary>
        public System.Collections.Generic.ICollection<ElementMenuDto> Children { get; set; }
    }
}
