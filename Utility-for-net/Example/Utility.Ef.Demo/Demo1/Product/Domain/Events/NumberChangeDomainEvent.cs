#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Domain.Events
{
    public class NumberChangeDomainEvent : INotification
    {
        public NumberChangeDomainEvent()
        {

        }
        public NumberChangeDomainEvent(long productId, long spceId, int number)
        {
            ProductId = productId;
            SpceId = spceId;
            Number = number;
        }
        public NumberEventFlag Flag { get; set; } = NumberEventFlag.SellCount;
        public long ProductId { get;  set; }
        public long SpceId { get;  set; }
        public int Number { get;  set; }
    }
}
#endif