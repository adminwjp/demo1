using System.Collections.Generic;

namespace Utility.Demo.Application.Services.Dtos
{
#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
   // using AutoMapper;
    using Utility.Demo.Domain.Entities;
    using Utility;
    //[AutoMap(typeof(MenuEntity))]
#endif
    public class CreateEasyUIMenuInput
    {
        public string Id { get; set; }
        /// <summary>
        /// text：显示节点文本。
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// state：节点状态，'open' 或 'closed'，默认：'open'。如果为'closed'的时候，将不自动展开该节点。
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// checked：表示该节点是否被选中。
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// attributes: 被添加到节点的自定义属性。
        /// </summary>
        public IDictionary<string, object> Attributes { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Newtonsoft.Json.JsonProperty("IconCls",TypeNameHandling =Newtonsoft.Json.TypeNameHandling.None)]
        //[FromForm(Name ="icon_cls")]
        public string IconCls { get; set; }
       
    }
}
