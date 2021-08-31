using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Users.Events
{
    /// <summary>
    /// 用户积分
    /// </summary>
    public class UserChangePointEvent : INotification
    {
        public UserChangePointEvent(long userId, int experiencePoints, int reputationPoints, int tradePoints, int tradePoints2, int tradePoints3, int tradePoints4)
        {
            UserId = userId;
            ExperiencePoints = experiencePoints;
            ReputationPoints = reputationPoints;
            TradePoints = tradePoints;
            TradePoints2 = tradePoints2;
            TradePoints3 = tradePoints3;
            TradePoints4 = tradePoints4;
        }

        public long UserId { get; }

        /// <summary>
        /// 经验积分值
        /// </summary>
        public virtual int ExperiencePoints { get;  }

        /// <summary>
        /// 威望积分值
        /// </summary>
        public virtual int ReputationPoints { get;  }

        /// <summary>
        /// 交易积分值
        /// </summary>
        public virtual int TradePoints { get;  }

        /// <summary>
        /// 交易积分值2
        /// </summary>
        public virtual int TradePoints2 { get;  }

        /// <summary>
        /// 交易积分值3
        /// </summary>
        public virtual int TradePoints3 { get;  }

        /// <summary>
        /// 交易积分值4
        /// </summary>
        public virtual int TradePoints4 { get;  }
    }
}
