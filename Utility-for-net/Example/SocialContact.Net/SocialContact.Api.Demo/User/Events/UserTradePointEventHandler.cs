using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Core.Users.Events
{
    public class UserTradePointEventHandler : INotificationHandler<UserTradePointEvent>
    {
        
        public Task Handle(UserTradePointEvent notification, CancellationToken cancellationToken)
        {
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                string sql = string.Empty;
                switch (notification.Freeze)
                {
                    case FreezeFlag.Freeze:
                        sql = "update tn_Users set FrozenTradePoints=FrozenTradePoints + @TradePoints,TradePoints=TradePoints -  @TradePoints where UserId = @UserId ";
                        break;
                    case FreezeFlag.NoFreeze:
                        sql = "update tn_Users set FrozenTradePoints=FrozenTradePoints - @TradePoints,TradePoints=TradePoints +  @TradePoints where UserId = @UserId ";
                        break;
                   case FreezeFlag.None:
                    default:
                        sql="update tn_Users set FrozenTradePoints=FrozenTradePoints - @TradePoints where UserId = @UserId ";
                        break;
                }
               return connection.ExecuteAsync(sql, new { notification.UserId, notification.TradePoints });
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }
    }
}
