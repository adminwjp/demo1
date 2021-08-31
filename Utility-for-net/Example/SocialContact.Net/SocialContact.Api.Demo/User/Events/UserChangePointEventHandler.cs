using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Tunynet.Common;

namespace Core.Users.Events
{
    public class UserChangePointEventHandler : INotificationHandler<UserChangePointEvent>
    {
        public async Task Handle(UserChangePointEvent notification, CancellationToken cancellationToken)
        {
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                User user = connection.QueryFirstOrDefault<User>("select * from tn_Users where UserId = @UserId ", notification);
                if (user == null)
                    return;
                if (await connection.ExecuteAsync("update tn_Users set ExperiencePoints=ExperiencePoints+@ExperiencePoints,ReputationPoints=ReputationPoints+@ReputationPoints,TradePoints=TradePoints+@TradePoints,TradePoints2=TradePoints2+@tradePoints2,TradePoints3=TradePoints3+@TradePoints3,TradePoints4=TradePoints4+@TradePoints4 where UserId = @UserId", 
                    notification) > 0)
                {

                }
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }
    }
}
