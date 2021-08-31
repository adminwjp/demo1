using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.Attributes;
using Utility.Domain;
using Utility.Domain.Repositories;
using Utility.Domain.Services;
using Utility.Domain.Uow;
using Utility.Helpers;

namespace Utility.Interceptors
{
  //  public class TranstationInterceptorReference: InterceptorReference
  //  {

  //  }
    public static class InterceptorHelper
    {
        
        public static Tuple<IUnitWork,bool> Get(IInvocation invocation)
        {
            var repository = invocation.InvocationTarget as IRepository;
            var unitWork = repository == null ? invocation.InvocationTarget as IUnitWork : repository.UnitWork;
            var domainService = invocation.InvocationTarget as DomainService;
            unitWork = unitWork ?? domainService?.UnitWork;
            if (unitWork == null)
            {
                //invocation.Proceed();
                return null;
            }
            var read = false;
            foreach (var item in IocTranstationAopInterceptor.actionNames)
            {
                if (invocation.Method.Name.ToLower().Contains(item))
                {
                    read = false;
                    break;
                }
            }
            return new Tuple<IUnitWork, bool>(unitWork, read);
        }
        public static void Execute(IInvocation invocation, IUnitWork unitWork,  bool read = false, bool end = false)
        {
            var tx = AttributeHelper.Get<TranstationAttribute>(invocation.TargetType.GetCustomAttributes(false));
            if (tx != null && tx.UseTranstation)
            {

                Execute(unitWork, read, end);
            }
            else
            {
                tx = AttributeHelper.Get<TranstationAttribute>(invocation.Method.GetCustomAttributes(false));
                if (tx != null && tx.UseTranstation)
                {
                    Execute(unitWork,  read, end);
                }
            }
        }
        public static void Execute(IUnitWork unitWork,  bool read = false, bool end = false)
        {
            if (unitWork != null)
            {
                if (!unitWork.UseTransaction)
                {
                    unitWork.UseTransaction = true;
                }
                if (!read)
                {
                    Execute(unitWork.WriteTransaction, end);
                }
                else
                {
                    Execute(unitWork.ReadTransaction,  end);
                }
            }
        }
        public static void Execute(TransactionManager transtionManager,  bool end = false)
        {
            //begin
            if (!end)
            {
                transtionManager.TaskCount++;
                return;
            }
            //第一次 任务 则 提交
            //第二次 ，，， 不应该提交 第一次 结束 提交 
            if (end && transtionManager.TaskCount == 1)
            {
                transtionManager.Commit();
                transtionManager.TaskCount = 0;
            }
        }

    }
    public class AsyncAopInterceptor<TInterceptor> : AsyncDeterminationInterceptor
         where TInterceptor : IAsyncInterceptor
    {
        public AsyncAopInterceptor(TInterceptor asyncInterceptor) : base(asyncInterceptor)
        { 
         
        }
        public override void Intercept(IInvocation invocation)
        {
            var tuple = InterceptorHelper.Get(invocation);
            if (tuple == null)
            {
                base.Intercept(invocation);
                return;
            }
            //一旦 使用 时 获取事务 这里  则 为 false
            //因为 有事务 了 则 永远 提交不了 只有该任务链 才能提交
            //class -> method 
            //2 task first false->true false->false  
            Console.WriteLine("TargetType{0} Method {1} aop start", invocation.TargetType.FullName, invocation.Method.Name);
            InterceptorHelper.Execute(invocation, tuple.Item1,  tuple.Item2);//事务开始

            //method1 ->method2,method3,...
            //method2->method4,method5...
            //method3->method5,method6...
            //... 层层 调用 会出现 异步阻塞 
            //异步 异步 阻塞

            base.Intercept(invocation);//异步 异步 阻塞
            if (invocation.Method.ReturnType !=typeof(Task)|| !(invocation.Method.ReturnType.IsGenericType
                    && invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)
                    ))
            {
                InterceptorHelper.Execute(invocation, tuple.Item1, tuple.Item2,true);//事务结束
                Console.WriteLine("TargetType{0} Method {1} aop end", invocation.TargetType.FullName, invocation.Method.Name);
            }
        }
    }
    internal class AsyncTransTask
    {
        public IInvocation Invocation { get; set; }
        public IUnitWork UnitWork { get; set; }
        public bool Read { get; set; }
    }
    /// <summary>
    /// autofac(ioc) 启动 时(使用时 di) 会 自动 创建 aop 代理
    /// 泛型 aop 注入 创建 实例 不好控制 最好 不要用泛型 未知类型 没法创建 实例
    /// autofac 执行 流程 需要 改变 可以 创建 具体 类型 泛型 实例 但 默认流程 泛型类型无法创建 泛型不知道具体类型
    /// Unable to cast object of type 'System.RuntimeType' to type 
    /// 包装 后 ioc 无法 获取(转换异常) 难道必须使用接口? as pass
    /// </summary>
    public class IocTranstationAopInterceptor : IInterceptor
    {
        public static readonly Type IocTranstationAopInterceptorType=typeof(IocTranstationAopInterceptor);
        public static bool Many = true;
        public static readonly List<string> actionNames = new List<string>(10) {
            "insert","add","update","modify","edit","remove","delete"
        };
    
        //主从 需要手动实现 有 读 有写 默认 应该 写 但 内部 未封装 需要手动调用
        //暂时 不考虑
        //单机 都是 同一个 对象 没影响
        //反射 不好 控制 这里 用 基类 控制
        // 如果自定义 需要 反射 查找 比较 麻烦  需要打日志 调试 才  方便 
        //不好找 问题 
        public   void Intercept(IInvocation invocation)
        {
            //DomainHelper.Execute();
            var tuple = InterceptorHelper.Get(invocation);
            if (tuple == null)
            {
                invocation.Proceed();
                return;
            }
           
            //一旦 使用 时 获取事务 这里  则 为 false
            //因为 有事务 了 则 永远 提交不了 只有该任务链 才能提交
            //class -> method 
            //2 task first false->true false->false  
            var hasFirst = false;//是否是第一次任务
            Console.WriteLine("TargetType{0} Method {1} aop start", invocation.TargetType.FullName, invocation.Method.Name);
            InterceptorHelper.Execute(invocation, tuple.Item1,  tuple.Item2);
            //异步嵌套 没法控制 这里需要控制逻辑 同步执行
            //要么不管 异步方法 逻辑处理要改变
            //要控制 异步任务链 记录下来 按顺序执行
            if (TypeHelper.IsAsyncMethod(invocation.Method))
            {
                //new Task 需要 改 过来 Task.FromResult
                //await new Task<string>(()=>"test async await")//error 阻塞
                //var t = await Task.FromResult("test async await");//pass 有些版本 不支持 坑嗲 啊低版本 就不用了
                //任务一直进来
                invocation.Proceed();
                //阻塞 怎么 用的啊
                if(invocation.Method.ReturnType.IsGenericType
                    && invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)
                    )
                    {
                    var m = HandAsyncMethod.MakeGenericMethod(invocation.Method.ReturnType.GetGenericArguments());
                    invocation.ReturnValue = m.Invoke(this,new object[] { invocation.ReturnValue,new AsyncTransTask() { 
                        Invocation=invocation,UnitWork=tuple.Item1,Read=tuple.Item2
                        }
                    });
                }
                else
                {
                    Func<Task> func = async () =>
                    {
                        await ((Task)invocation.ReturnValue);
                        InterceptorHelper.Execute(invocation, tuple.Item1,  tuple.Item2, true);
                        Console.WriteLine("TargetType{0} Method {1} aop end", invocation.TargetType.FullName, invocation.Method.Name);
                    };
                    invocation.ReturnValue = func();
                }
                    //Object reference not set to an instance of an object
                    //await (Task)invocation.Method.Invoke(invocation.InvocationTarget,
                    //   invocation.Arguments);
                    // ((Task)invocation.Method.Invoke(invocation.InvocationTarget,
                    //   invocation.Arguments)).GetAwaiter().GetResult();
                    //堆积任务过多  所以 假死
                    //((Task)invocation.ReturnValue).Wait();
                    //((Task)invocation.ReturnValue).GetAwaiter().GetResult();
               // await Task.Run(()=> { });
                //We should wait for finishing of the method execution
                //taks1.Wait(TimeSpan.FromSeconds(10));
              

                return;
                //假死 怎么办
                //var task2 = taks1.ContinueWith(task =>
                //   {
                //       Execute(invocation, tuple.Item1, ref hasFirst,  tuple.Item2,true);
                //   });
                //Task.WaitAll(new Task[] { taks1, task2 },TimeSpan.FromSeconds(10));
            }
            else
            {
                invocation.Proceed();
                InterceptorHelper.Execute(invocation, tuple.Item1, tuple.Item2, true);
                Console.WriteLine("TargetType{0} Method {1} aop end", invocation.TargetType.FullName, invocation.Method.Name);
            }
        }
        static readonly MethodInfo HandAsyncMethod = typeof(IocTranstationAopInterceptor)
            .GetMethod("HandAsync", BindingFlags.Instance | BindingFlags.NonPublic);
         async Task<T> HandAsync<T>(Task<T> task, AsyncTransTask asyncTrans)
        {
            var t = await task;
            InterceptorHelper.Execute(asyncTrans.Invocation, asyncTrans.UnitWork,  asyncTrans.Read, true);
            Console.WriteLine("TargetType{0} Method {1} aop end", asyncTrans.Invocation.TargetType.FullName, asyncTrans.Invocation.Method.Name);
            return t;
        }
    
    }
    public class IocTranstationAopInterceptorAsync : AsyncInterceptorBase, IAsyncInterceptor
    {

        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            await proceed(invocation,proceedInfo);
            var tuple = InterceptorHelper.Get(invocation);
            if (tuple != null)
            {
                InterceptorHelper.Execute(invocation, tuple.Item1, tuple.Item2, true);
                Console.WriteLine("TargetType{0} Method {1} aop end", invocation.TargetType.FullName, invocation.Method.Name);
            }
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            var t=await  proceed(invocation, proceedInfo);
            var tuple = InterceptorHelper.Get(invocation);
            if (tuple != null)
            {
                InterceptorHelper.Execute(invocation, tuple.Item1, tuple.Item2, true);
                Console.WriteLine("TargetType{0} Method {1} aop end", invocation.TargetType.FullName, invocation.Method.Name);
            }
            return t;
        }
    }
}
