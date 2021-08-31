#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TakeOutFoot.Activties;
using TakeOutFoot.Prizes;
using Utility.Cache;

namespace TakeOutFoot.Interceptors
{
    public class ServiceInterceptor : IInterceptor
    {
       
        protected ICacheContent cache;
       // protected ILogger logger;
        public ServiceInterceptor(ICacheContent cache)
        {
            this.cache = cache;
        }
        public void Intercept(IInvocation invocation)
        {
            //逻辑暂时写死
            //After method execution
            if (invocation.MethodInvocationTarget.Name.Equals("CheckStock"))
            {
                Activty activty = cache.Get<Activty>("cache");
                var argumens = (Tuple<long, long, int>[])invocation.Arguments[0];
                //检测库存
                foreach (var it in argumens)
                {
                    //库存 暂时 这样处理 还需要移除无限的优惠券(库存没了)
                    var setting = activty.ActivtySettings.Find(it1 => it1.gift_id == it.Item1);
                    //库存不够
                    if (setting.Number < it.Item3)
                    {
                        Console.WriteLine("CheckStock 库存不够!");
                        invocation.ReturnValue = false;
                        return;
                    }
                }
                invocation.ReturnValue = true;
                return;
            }
            else if (invocation.MethodInvocationTarget.Name.Equals("GetPrize"))
            {
                invocation.Proceed();

                //更新缓存 值 怎么传过来
                var obj = (PrizeAppService)invocation.InvocationTarget;
                if(obj.Cache!=null)
                {
                    Console.WriteLine("GetPrize 库存更新!");
                    Activty activty = cache.Get<Activty>("cache");
                    int total = 0;
                    //优惠券 库存 活动库存
                    obj.Cache.ToList().ForEach(it => {
                            total += it.Item3;
                            var setting = activty.ActivtySettings.Find(it1 => it1.gift_id.Value == it.Item1);
                            setting.Gift.Stocks -= it.Item3;
                            setting.Number -= it.Item3;
                        });
                    cache.Set("cache", activty,DateTime.Now.AddDays(1));

                }
                return;
            }

            //Before method execution
            var stopwatch = Stopwatch.StartNew();
         

            //Executing the actual method
            invocation.Proceed();
            return;
            //添加修改
            if (IsAsyncMethod(invocation.Method))
            {
                var taks1 = ((Task)invocation.ReturnValue);
                try
                {
                    //We should wait for finishing of the method execution
                    var task2 = ((Task)invocation.ReturnValue)
                       .ContinueWith(task =>
                       {
                           Execute(invocation, stopwatch);
                       });
                    Task.WaitAll(taks1, task2);
                }
                catch (System.Exception e)
                {
                   // logger.LogError(e, "aop reutrn task exception");
                }

            }
            else
            {
                Execute(invocation, stopwatch);
            }

        }
        public void Execute(IInvocation invocation, Stopwatch stopwatch)
        {
            //After method execution
            if (invocation.MethodInvocationTarget.Name.Equals(""))
            {

            }else
            {

            }
            stopwatch.Stop();
            //logger.LogInformation(
            //    "MeasureDurationInterceptor: {0} executed in {1} milliseconds.",
            //    invocation.MethodInvocationTarget.Name,
            //    stopwatch.Elapsed.TotalMilliseconds.ToString("0.000")
            //    );
        }
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }
    }
}
#endif