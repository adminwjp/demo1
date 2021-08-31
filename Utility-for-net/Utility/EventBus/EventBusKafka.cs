#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0|| NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility.EventBus.Abstractions;
using Utility.EventBus.Events;
using Utility.EventBus.Extensions;
using Utility.Logs;
using Utility.MessageQueue;

namespace Utility.EventBus
{
    /// <summary>
    /// 基于 kafka 事件 总线
    /// </summary>
    public class EventBusKafka : IEventBus, IDisposable
    {
        private readonly ConfluentKafkaHelper _confluentKafkaHelper;
        private readonly ILog<EventBusKafka> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;
        private readonly string AUTOFAC_SCOPE_NAME = "event_bus";
        private CancellationTokenSource cancellationToken;
        /// <summary>
        /// 基于 kafka 事件 总线
        /// </summary>
        /// <param name="confluentKafkaHelper"></param>
        /// <param name="logger"></param>
        /// <param name="autofac"></param>
        /// <param name="subsManager"></param>
        public EventBusKafka(ConfluentKafkaHelper confluentKafkaHelper, ILog<EventBusKafka> logger,
            ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager)
        {
            _confluentKafkaHelper = confluentKafkaHelper;
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
            _confluentKafkaHelper.Unsubscribe(eventName);
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
                var data = _confluentKafkaHelper.Consumer.Consume(100);//每次请求一次 
                //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                //data=consumer.Consume(cancellationTokenSource.Token);//为 null 一直循环  找到为止
                if (data != null)
                {
                    //consumer.Assign(data.TopicPartition);//客户端保存自己的当前偏移
                    //自动 处理 (用不同 的包(kafka-core) 怎么 还重复 读脏数据)
                    //手动提交 稳定些 (不管他  自动 提交)
                    data.Offset = data.TopicPartitionOffset.Offset;//已处理偏移
                    _confluentKafkaHelper.Consumer.Commit(data); //连同提交到服务端对应的偏移中进行更改
                    ProcessEvent(data.Topic, data.Message.Value).GetAwaiter().GetResult();
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
            _confluentKafkaHelper.Push(eventName, message);
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
                _confluentKafkaHelper.Consumer.Subscribe(eventName);
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
            _confluentKafkaHelper.Consumer.Dispose();
            _confluentKafkaHelper.Producer.Dispose();
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