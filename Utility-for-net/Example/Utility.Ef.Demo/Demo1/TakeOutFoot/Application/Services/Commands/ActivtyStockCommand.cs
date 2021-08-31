#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TakeOutFoot.Application.Services.Commands
{
    public class ActivtyStockCommand
    {
        public List<Stock> Stocks { get; set; }

        public ActivtyStockCommand()
        {

        }

        public ActivtyStockCommand(List<Stock> stocks)
        {
            Stocks = stocks;
        }
    }

    [Serializable]
    public class Stock
    {
        public Stock()
        {

        }
        public Stock(long? id, int number)
        {
            Id = id;
            Number = number;
        }

        /// <summary>
        /// 代理商 、 商家、 平台 、买家 等 id
        /// </summary>
        [DataMember]
        public long? Id { get; set; }
        /// <summary>
        /// 数量 添加 + 移除 -
        /// </summary>
        [DataMember]
        public int Number { get; set; }
    }
}
#endif