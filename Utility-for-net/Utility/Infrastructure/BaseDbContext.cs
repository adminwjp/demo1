﻿#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
#endif
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utility.Domain.Entities;
using Utility.Ef;

namespace Utility.Infrastructure
{

    /// <summary>
    /// IMediator实现 未任何实现
    /// </summary>
    public class NoMediator : IMediator
    {
        /// <summary>
        /// 未任何实现
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
        {
               return Task.CompletedTask;
        }

        /// <summary>
        /// 未任何实现
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task Publish(object notification, CancellationToken cancellationToken = default(CancellationToken)) 
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 未任何实现
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(default(TResponse));
        }

        /// <summary>
        /// 未任何实现
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<object> Send(object request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult((object)null);
        }
    }

    /// <summary>
    /// 数据库 上下文
    /// </summary>
    public class BaseDbContext<Context,Entity> : BaseDbContext<Context>
        where Context : BaseDbContext<Context,Entity>
        where Entity:class,IDomainEvent
    {
        private IMediator mediator;

#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
        private BaseDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
#else
        private BaseDbContext(DbContextOptions<Context> options) : base(options) { }
#endif
  

    

#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
        /// <summary>
        /// 数据库 上下文
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="nameOrConnectionString"></param>
        public BaseDbContext(string nameOrConnectionString, IMediator mediator) : base(nameOrConnectionString) {
            this.mediator = mediator;
        }
#else
        /// <summary>
        /// 数据库 上下文
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="options"></param>
        public BaseDbContext(DbContextOptions<Context> options, IMediator mediator) :base(options)
        {
            this.mediator = mediator;
        }
#endif

        /// <summary>
        /// 处理 事件完成 再数据库持久化
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await mediator.DispatchDomainEventsAsync<Context,Entity>(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync();

            return result>0;
        }
    }
}
#endif