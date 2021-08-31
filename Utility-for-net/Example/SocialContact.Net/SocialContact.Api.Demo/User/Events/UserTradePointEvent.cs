using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users.Events
{
    /// <summary>
    /// 用户的交易积分 事件 
    /// </summary>
    public class UserTradePointEvent: INotification
    {
     

        public UserTradePointEvent(long userId, int tradePoints, FreezeFlag freeze)
        {
            UserId = userId;
            TradePoints = tradePoints;
            Freeze = freeze;
        }

        public long UserId { get; }//用户id

        public int TradePoints { get; }//交易积分

        public FreezeFlag Freeze { get; } //冻结
    }
    public enum FreezeFlag
    {
        None,
        Freeze,
        NoFreeze
    }
}
