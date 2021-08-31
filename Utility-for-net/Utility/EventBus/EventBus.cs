#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0|| NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Utility.EventBus.Abstractions;
using Utility.EventBus.Events;
using Utility.EventBus.Extensions;
using Utility.Logs;

namespace Utility.EventBus
{
    /// <summary>
    /// 基于 内存 事件 总线
    /// </summary>
    public class EventBus : IEventBus, IDisposable
    {
        private readonly ILog<IEventBus> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;
        private readonly string AUTOFAC_SCOPE_NAME = "event_bus";
        private CancellationTokenSource cancellationToken;
        readonly ConcurrentDictionary<string, LocalEventBus> eventBus =
            new ConcurrentDictionary<string, LocalEventBus>(1,100);

        class LocalEventBus
        {
            /// <summary>
            /// 消息 队列
            /// </summary>
            public readonly BlockingCollection<string> Msgs = new BlockingCollection<string>(10 * 10000);

            /// <summary>
            /// 是否订阅
            /// </summary>
            public bool Subscribe = false;
        }
        /// <summary>
        /// 基于 内存 事件 总线
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="autofac"></param>
        /// <param name="subsManager"></param>
        public EventBus(ILog<IEventBus> logger,
            ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _autofac = autofac;
            _subsManager.OnEventRemoved -= SubsManager_OnEventRemoved;
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
            Task.Factory.StartNew(HandlerData);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventName"></param>
        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if(eventBus.TryGetValue(eventName, out LocalEventBus local))
            {
                local.Subscribe = false;
            }
            if (_subsManager.IsEmpty)
            {
                Dispose();
            }
        }

        /// <summary>
        ///订阅  处理 数据
        /// </summary>
        void HandlerData()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var item in eventBus)
                {
                    if (item.Value.Subscribe&&!item.Value.Msgs.IsCompleted)
                    {
                        ProcessEvent(item.Key, item.Value.Msgs.Take()).GetAwaiter().GetResult();
                    }
                }
            }
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event">集成事件</param>
        public virtual void Publish(IntegrationEvent @event)
        {
            var message = JsonConvert.SerializeObject(@event);
            var eventName = @event.GetType().Name;
            LocalEventBus local;
            if (eventBus.ContainsKey(eventName))
            {
                local = eventBus[eventName];
            }
            else
            {
                local = new LocalEventBus();
                eventBus.TryAdd(eventName, local);
            }
            local.Msgs.Add(message);
        }

        /// <summary>
        /// 订阅动态事件
        /// </summary>
        /// <typeparam name="TH">动态事件处理<see cref="IDynamicIntegrationEventHandler"/></typeparam>
        /// <param name="eventName">事件类型名称</param>
        public virtual void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _logger.Log(LogLevel.Info,$"Subscribing to dynamic event {eventName} with {typeof(TH).GetGenericTypeName()}");

            DoInternalSubscription(eventName);
            _subsManager.AddDynamicSubscription<TH>(eventName);
        }

        /// <summary>
        /// 订阅动态事件
        /// </summary>
        /// <typeparam name="TH">动态事件处理<see cref="IDynamicIntegrationEventHandlerAsync"/></typeparam>
        /// <param name="eventName">事件类型名称</param>
        public virtual void SubscribeDynamicAsync<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandlerAsync
        {
            _logger.Log(LogLevel.Info, $"Subscribing to dynamic event {eventName} with {typeof(TH).GetGenericTypeName()}");

            DoInternalSubscription(eventName);
            _subsManager.AddDynamicSubscriptionAsync<TH>(eventName);
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T">集成事件<see cref="IntegrationEvent"/></typeparam>
        /// <typeparam name="TH">集成事件处理 <see cref="IIntegrationEventHandler{T}"/></typeparam>
        public virtual void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);

            _logger.Log(LogLevel.Info, $"Subscribing to  event {eventName} with {typeof(TH).GetGenericTypeName()}");

            _subsManager.AddSubscription<T, TH>();
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T">集成事件<see cref="IntegrationEvent"/></typeparam>
        /// <typeparam name="TH">集成事件处理 <see cref="IIntegrationEventHandlerAsync{T}"/></typeparam>
        public virtual void SubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandlerAsync<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);

            _logger.Log(LogLevel.Info, $"Subscribing to  event {eventName} with {typeof(TH).GetGenericTypeName()}");

            _subsManager.AddSubscriptionAsync<T, TH>();
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventName">事件类型名称</param>
        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (eventBus.ContainsKey(eventName))
                {
                    LocalEventBus local = eventBus[eventName];
                    local.Subscribe = true;
                }
            }
        }

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <typeparam name="T">集成事件 <see cref="IntegrationEvent"/></typeparam>
        /// <typeparam name="TH">事件处理 <see cref="IIntegrationEventHandler{T}"/></typeparam>
        public virtual void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            _logger.Log(LogLevel.Info, $"Unsubscribing from event {eventName} ");

            _subsManager.RemoveSubscription<T, TH>();
        }

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <typeparam name="T">集成事件 <see cref="IntegrationEvent"/></typeparam>
        /// <typeparam name="TH">事件处理 <see cref="IIntegrationEventHandlerAsync{T}"/></typeparam>
        public virtual void UnsubscribeAsync<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandlerAsync<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            _logger.Log(LogLevel.Info, $"Unsubscribing from event {eventName} ");
           // _confluentKafkaHelper.Consumer.Unsubscribe();
            _subsManager.RemoveSubscriptionAsync<T, TH>();
        }

    

        /// <summary>
        /// 取消订阅动态事件
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        public virtual void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        /// <summary>
        /// 取消订阅动态事件
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        public virtual void UnsubscribeDynamicAsync<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandlerAsync
        {
            _subsManager.RemoveDynamicSubscriptionAsync<TH>(eventName);
        }

        /// <summary>
        /// 释放资源 消费者关闭 订阅信息清理
        /// </summary>
        public virtual void Dispose()
        {
            cancellationToken.Cancel();
            foreach (var item in eventBus)
            {
                item.Value.Subscribe = false;
            }
            _subsManager.Clear();
        }

        /// <summary>
        /// 基于 kafka 消息对应 事件处理
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ProcessEvent(string eventName, string message)
        {
             await EventProcessHelper.ProcessEvent(_subsManager, _autofac, AUTOFAC_SCOPE_NAME, eventName, message);
        }
    }
}
#endif