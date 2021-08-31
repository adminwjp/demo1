//#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0|| NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
//using Autofac;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Utility.EventBus.Abstractions;
//using Utility.EventBus.Events;
//using Utility.EventBus.Extensions;
//using Utility.Logs;
//using Utility.MessageQueue;
//using System.ServiceModel;
//using System.Messaging;
//using System.ServiceModel.Channels;
//using System.ServiceModel.Web;

////wcf 消息 队列 怎么 控制的
////wcf 通信 消息 队列 本地 处理?
////多台 机器 怎么 控制  wcf  难道 手动 控制 根据 ip 区分 
//namespace Utility.EventBus
//{
//    /// <summary>
//    /// wcf 消息 队列
//    /// </summary>
//    [Serializable]
//    public class WcfMqMsg
//    {
//        /// <summary>
//        /// 消息
//        /// </summary>
//        public string Msg { get; set; }
//        /// <summary>
//        /// 标签
//        /// </summary>
//        public string Label { get; set; } = "测试消息";
//        /// <summary>
//        /// 队列名称
//        /// </summary>
//        public string QueueName { get; set; } = @".\Private$\SampleQueue";
//    }
//    /// <summary>
//    /// wcf mq 服务
//    /// </summary>
//    [ServiceContract]//wcf 契约标识 接口上必须有 否则wcf不支持
//    public interface WcfService
//    {

//        /// <summary>
//        /// 发送消息
//        /// </summary>
//        /// <param name="msg"></param>
//        [OperationContract(Name = "Send")]
//        [WebInvoke(UriTemplate = "Send", Method = "POST", RequestFormat = WebMessageFormat.Json,
//        ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
//        void Send(WcfMqMsg msg);

//        /// <summary>
//        /// 读取消息
//        /// </summary>
//        /// <param name="queueName"></param>
//        [OperationContract(Name = "Read")]
//        [WebInvoke(UriTemplate = "Read", Method = "GET",
//       ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
//        WcfMqMsg Read(string queueName = @".\Private$\SampleQueue");
//    }

//    /// <summary>
//    /// wcf mq 服务 实现 应代码 配置需要注解
//    /// </summary>
//    [System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
//    public class WcfServiceImpl
//    {
//        /// <summary>
//        /// 发送消息
//        /// </summary>
//        /// <param name="msg"></param>
//        public void Send(WcfMqMsg msg)
//        {
//            WcfMessageQueue.SendMessage(msg.Msg, msg.Label, msg.QueueName);
//        }

//        /// <summary>
//        /// 读取消息
//        /// </summary>
//        /// <param name="queueName"></param>
//        public WcfMqMsg Read(string queueName = @".\Private$\SampleQueue")
//        {
//            var tuple = WcfMessageQueue.ReadMessage(queueName);
//            return new WcfMqMsg() { Label= tuple.Item2,Msg= tuple.Item1,QueueName= queueName };
//        }
//    }

//    /// <summary>
//    /// wcf mq 代理 端
//    /// </summary>
//    [ServiceContract]//wcf 契约标识 接口上必须有 否则wcf不支持
//    public interface WcfServiceClient: WcfService
//    {

//    }
//    /// <summary>
//    /// wcf mq 代理 端 实现 
//    /// </summary>
//    public class WcfServiceClientImpl: ClientBase<WcfService>, WcfServiceClient
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        public WcfServiceClientImpl()
//        {

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="endpointConfigurationName"></param>
//        public WcfServiceClientImpl(string endpointConfigurationName) : base(endpointConfigurationName)
//        {

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="endpointConfigurationName"></param>
//        /// <param name="remoteAddress"></param>
//        public WcfServiceClientImpl(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
//        {

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="endpointConfigurationName"></param>
//        /// <param name="remoteAddress"></param>
//        public WcfServiceClientImpl(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
//        {

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="binding"></param>
//        /// <param name="remoteAddress"></param>
//        public WcfServiceClientImpl(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : base(binding, remoteAddress)
//        {

//        }

//        /// <summary>
//        /// 发送消息
//        /// </summary>
//        /// <param name="msg"></param>
//        public void Send(WcfMqMsg msg)
//        {
//            base.Channel.Send(msg);
//        }

//        /// <summary>
//        /// 读取消息
//        /// </summary>
//        /// <param name="queueName"></param>
//        public WcfMqMsg Read(string queueName = @".\Private$\SampleQueue")
//        {
//           return base.Channel.Read(queueName);
//        }
//    }


//    /// <summary>
//    /// 基于 window wcf mq  事件 总线
//    /// </summary>
//    public class EventBusWcfMQ : IEventBus, IDisposable
//    {
//        private readonly ILog<EventBusWcfMQ> _logger;
//        private readonly IEventBusSubscriptionsManager _subsManager;
//        private readonly ILifetimeScope _autofac;
//        private readonly string AUTOFAC_SCOPE_NAME = "event_bus";
//        private  string _queueName;
//        private CancellationTokenSource cancellationToken;
//        private Dictionary<string, ServiceHost> serviceHosts = new Dictionary<string, ServiceHost>();
//        public static ISet<string> Ips = new HashSet<string>(new string[] { "net.msmq://localhost/Private", "net.msmq://127.0.0.1/Private" });
//        /// <summary>
//        /// 基于 window wcf mq 事件 总线
//        /// </summary>
//        /// <param name="logger"></param>
//        /// <param name="autofac"></param>
//        /// <param name="subsManager"></param>
//        /// <param name="ips">["net.msmq://localhost/Private", "net.msmq://127.0.0.1/Private"]</param>
//        public EventBusWcfMQ(ILog<EventBusWcfMQ> logger,
//            ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager, string[] ips)
//        {
//            if (ips != null)
//            {
//                foreach (var item in ips)
//                {
//                    Ips.Add(item);
//                }
//            }
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
//            _autofac = autofac;
//            _subsManager.OnEventRemoved -= SubsManager_OnEventRemoved;
//            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
//             Task.Factory.StartNew(HandlerData);
//        }
//        /// <summary>
//        /// 取消订阅
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="eventName"></param>
//        private void SubsManager_OnEventRemoved(object sender, string eventName)
//        {
//            if (_subsManager.IsEmpty)
//            {
//                Dispose();
//            }
//        }

//        /// <summary>
//        ///订阅  处理 数据
//        /// </summary>
//        void HandlerData()
//        {
//            while (!cancellationToken.IsCancellationRequested)
//            {
                
//            }
//        }

//        /// <summary>
//        /// 发布事件
//        /// </summary>
//        /// <param name="event">集成事件</param>
//        public virtual void Publish(IntegrationEvent @event)
//        {
//            var message = JsonConvert.SerializeObject(@event);
//            var eventName = @event.GetType().Name;

//        }

//        /// <summary>
//        /// 订阅动态事件
//        /// </summary>
//        /// <typeparam name="TH">动态事件处理<see cref="IDynamicIntegrationEventHandler"/></typeparam>
//        /// <param name="eventName">事件类型名称</param>
//        public virtual void SubscribeDynamic<TH>(string eventName)
//            where TH : IDynamicIntegrationEventHandler
//        {
//            _logger.Log(LogLevel.Info,$"Subscribing to dynamic event {eventName} with {typeof(TH).GetGenericTypeName()}");

//            DoInternalSubscription(eventName);
//            _subsManager.AddDynamicSubscription<TH>(eventName);
//        }

//        /// <summary>
//        /// 订阅动态事件
//        /// </summary>
//        /// <typeparam name="TH">动态事件处理<see cref="IDynamicIntegrationEventHandlerAsync"/></typeparam>
//        /// <param name="eventName">事件类型名称</param>
//        public virtual void SubscribeDynamicAsync<TH>(string eventName)
//            where TH : IDynamicIntegrationEventHandlerAsync
//        {
//            _logger.Log(LogLevel.Info, $"Subscribing to dynamic event {eventName} with {typeof(TH).GetGenericTypeName()}");

//            DoInternalSubscription(eventName);
//            _subsManager.AddDynamicSubscriptionAsync<TH>(eventName);
//        }

//        /// <summary>
//        /// 订阅事件
//        /// </summary>
//        /// <typeparam name="T">集成事件<see cref="IntegrationEvent"/></typeparam>
//        /// <typeparam name="TH">集成事件处理 <see cref="IIntegrationEventHandler{T}"/></typeparam>
//        public virtual void Subscribe<T, TH>()
//            where T : IntegrationEvent
//            where TH : IIntegrationEventHandler<T>
//        {
//            var eventName = _subsManager.GetEventKey<T>();
//            DoInternalSubscription(eventName);

//            _logger.Log(LogLevel.Info, $"Subscribing to  event {eventName} with {typeof(TH).GetGenericTypeName()}");

//            _subsManager.AddSubscription<T, TH>();
//        }

//        /// <summary>
//        /// 订阅事件
//        /// </summary>
//        /// <typeparam name="T">集成事件<see cref="IntegrationEvent"/></typeparam>
//        /// <typeparam name="TH">集成事件处理 <see cref="IIntegrationEventHandlerAsync{T}"/></typeparam>
//        public virtual void SubscribeAsync<T, TH>()
//            where T : IntegrationEvent
//            where TH : IIntegrationEventHandlerAsync<T>
//        {
//            var eventName = _subsManager.GetEventKey<T>();
//            DoInternalSubscription(eventName);

//            _logger.Log(LogLevel.Info, $"Subscribing to  event {eventName} with {typeof(TH).GetGenericTypeName()}");

//            _subsManager.AddSubscriptionAsync<T, TH>();
//        }

//        /// <summary>
//        /// 订阅事件
//        /// </summary>
//        /// <param name="eventName">事件类型名称</param>
//        private void DoInternalSubscription(string eventName)
//        {
//            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
//            if (!containsKey)
//            {
               
//            }
//        }

//        /// <summary>
//        /// 取消订阅事件
//        /// </summary>
//        /// <typeparam name="T">集成事件 <see cref="IntegrationEvent"/></typeparam>
//        /// <typeparam name="TH">事件处理 <see cref="IIntegrationEventHandler{T}"/></typeparam>
//        public virtual void Unsubscribe<T, TH>()
//            where T : IntegrationEvent
//            where TH : IIntegrationEventHandler<T>
//        {
//            var eventName = _subsManager.GetEventKey<T>();
//            _logger.Log(LogLevel.Info, $"Unsubscribing from event {eventName} ");

//            _subsManager.RemoveSubscription<T, TH>();
//        }

//        /// <summary>
//        /// 取消订阅事件
//        /// </summary>
//        /// <typeparam name="T">集成事件 <see cref="IntegrationEvent"/></typeparam>
//        /// <typeparam name="TH">事件处理 <see cref="IIntegrationEventHandlerAsync{T}"/></typeparam>
//        public virtual void UnsubscribeAsync<T, TH>()
//            where T : IntegrationEvent
//            where TH : IIntegrationEventHandlerAsync<T>
//        {
//            var eventName = _subsManager.GetEventKey<T>();
//            _logger.Log(LogLevel.Info, $"Unsubscribing from event {eventName} ");
//           // _confluentKafkaHelper.Consumer.Unsubscribe();
//            _subsManager.RemoveSubscriptionAsync<T, TH>();
//        }

    

//        /// <summary>
//        /// 取消订阅动态事件
//        /// </summary>
//        /// <typeparam name="TH"></typeparam>
//        /// <param name="eventName"></param>
//        public virtual void UnsubscribeDynamic<TH>(string eventName)
//            where TH : IDynamicIntegrationEventHandler
//        {
//            _subsManager.RemoveDynamicSubscription<TH>(eventName);
//        }

//        /// <summary>
//        /// 取消订阅动态事件
//        /// </summary>
//        /// <typeparam name="TH"></typeparam>
//        /// <param name="eventName"></param>
//        public virtual void UnsubscribeDynamicAsync<TH>(string eventName)
//            where TH : IDynamicIntegrationEventHandlerAsync
//        {
//            _subsManager.RemoveDynamicSubscriptionAsync<TH>(eventName);
//        }

//        /// <summary>
//        /// 释放资源 消费者关闭 订阅信息清理
//        /// </summary>
//        public virtual void Dispose()
//        {
//            cancellationToken.Cancel();

//            _subsManager.Clear();
//        }

//        /// <summary>
//        /// 基于 kafka 消息对应 事件处理
//        /// </summary>
//        /// <param name="eventName"></param>
//        /// <param name="message"></param>
//        /// <returns></returns>
//        private async Task ProcessEvent(string eventName, string message)
//        {
//             await EventProcessHelper.ProcessEvent(_subsManager, _autofac, AUTOFAC_SCOPE_NAME, eventName, message);
//        }
//    }
//}
//#endif