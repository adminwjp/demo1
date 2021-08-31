#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TakeOutFoot.Domain.Events;

namespace TakeOutFoot.Application.Services.Commands
{
    public class StockCommand : IRequest<bool>
    {
        public StockCommand()
        {

        }
        public StockCommand(long? id, int number, StockFlag flag)
        {
            Id = id;
            Number = number;
            Flag = flag;
        }

        /// <summary>
        /// 代理商 、 商家、 平台 、买家 等 id
        /// </summary>
        [DataMember]
        public long? Id { get;  set; }

        /// <summary>
        /// 数量 添加 + 移除 -
        /// </summary>
        [DataMember]
        public int Number { get;  set; }

        /// <summary>
        /// 库存 标识
        /// </summary>
        [DataMember]
        public StockFlag Flag { get;  set; }
    }
}
#endif