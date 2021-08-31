using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace Company.Domain.Entities
{
	public class BaseEntity:DomainEvent<long>,IEntity<long>, IDomainEvent
	{
        public string Lanage { get; set; }
		[Newtonsoft.Json.JsonIgnore]
		public long CreateDate { get; set; } //创建时间
		[Newtonsoft.Json.JsonIgnore]
		public long ModifyDate { get; set; }//修改时间
		public bool? Enable { get; set; } = true; //是否启用

    }
}
