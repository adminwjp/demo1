//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using SignalRChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat
{
    /// <summary>
    /// 通知hub
    /// </summary>
    //[HubName("NoticeHub")]
    public class NoticeHub : Hub
    {
        private static Dictionary<string, string> UserConnectionIdQueue = new Dictionary<string, string>();

        /// <summary>
        /// 开始链接时执行的任务
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {

            var userid = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(userid))
            {
                if (!UserConnectionIdQueue.Keys.Contains(userid))
                {
                    UserConnectionIdQueue.Add(userid, Context.ConnectionId);
                }
                else
                {
                    //如果队列已经存在则关闭当前链接
                    //SignalrHub.Instance().Clients.User(userid).(Context.Items["randomSignalr"]);
                }

            }
            return base.OnConnectedAsync();
        }
        /// <summary>
        /// 如果用户 断开则移除字典
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            //if (stopCalled)
            //{
            //     var userid = Context.User.Identity.Name;
            //    if (!string.IsNullOrEmpty(userid))
            //    {
            //        UserConnectionIdQueue.Remove(userid);
            //    }
            //}
            return base.OnDisconnectedAsync(exception);
        }

    }
}

/// <summary>
/// 自定义用户Id提供器
/// </summary>
public class CustomUserIdProvider : IUserIdProvider
{
    /// <summary>
    /// 获取用户UserId
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public string GetUserId(HubConnectionContext request)
    {
        if (request.User != null && request.User.Identity != null)
            return request.User.Identity.Name;
        //return HttpContext.Current.User.Identity.Name;
        else
            return null;
    }
}

/// <summary>
///Hub的单例模式
/// </summary>
public class SignalrHub
{
    private static volatile IHubContext<NoticeHub> _instance = null;
    private static readonly object lockObject = new object();

    /// <summary>
    /// 创建主页实体
    /// </summary>
    /// <returns></returns>
    public static IHubContext<NoticeHub> Instance()
    {
        if (_instance == null)
        {
            lock (lockObject)
            {
                if (_instance == null)
                {
                    //_instance = GlobalHost.ConnectionManager.GetHubContext<NoticeHub>();
                }
            }
        }
        return _instance;
    }

    private SignalrHub()
    { }
}