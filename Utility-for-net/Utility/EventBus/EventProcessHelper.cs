#if !(NET20 || NET30 ||  NET35  || NET10 || NET11)
using Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.EventBus.Abstractions;

namespace Utility.EventBus
{
    /// <summary>
    /// 统一 事件 处理
    /// </summary>
    public class EventProcessHelper
    {
        /// <summary>
        /// 基于  消息对应 事件处理
        /// </summary>
        /// <param name="_subsManager"></param>
        /// <param name="_autofac"></param>
        /// <param name="AUTOFAC_SCOPE_NAME"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<bool> ProcessEvent(IEventBusSubscriptionsManager _subsManager, ILifetimeScope _autofac,string AUTOFAC_SCOPE_NAME,
            string eventName, string message)
        {
            var process = false;
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var dynamicHandler = scope.ResolveOptional(subscription.HandlerType);
                            var handler = dynamicHandler as IDynamicIntegrationEventHandler;
                            if (handler == null)
                            {
                                var handlerAsync = dynamicHandler as IDynamicIntegrationEventHandlerAsync;
                                if (handlerAsync == null) continue;
                                dynamic eventData = JObject.Parse(message);
                                await handlerAsync.HandleAsync(eventData);
                            }
                            else
                            {
                                dynamic eventData = JObject.Parse(message);
                                handler.Handle(eventData);
                            }
                        }
                        else
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            if (handler == null) continue;
                            var eventType = _subsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                            if (eventType.IsAssignableFrom(typeof(IIntegrationEventHandler<>)))
                            {
                                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                                concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                            }
                            else
                            {
                                var concreteType = typeof(IIntegrationEventHandlerAsync<>).MakeGenericType(eventType);
                                await (Task)concreteType.GetMethod("HandleAsync").Invoke(handler, new object[] { integrationEvent });
                            }
                        }
                    }
                    process = true;
                }
            }
            
            return process;
        }
    }
}
#endif