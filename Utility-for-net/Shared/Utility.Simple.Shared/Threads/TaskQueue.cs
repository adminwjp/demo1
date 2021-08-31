#if !(NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Utility.Logs;
using Utility.Helpers;

namespace Utility.Threads
{
    /// <summary>
    /// 任务 队列
    /// </summary>
    public  class TaskQueue
    {
#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0)
        /// <summary>
        /// 任务队列
        /// </summary>
        protected readonly System.Collections.Concurrent.ConcurrentQueue<Action> Queue = new System.Collections.Concurrent.ConcurrentQueue<Action>();
#else
         /// <summary>
        /// 任务队列
        /// </summary>
        protected readonly Utility.Collections.ConcurrentArray<Action> Queue = new Utility.Collections.ConcurrentArray<Action>();
#endif
        /// <summary>
        /// 日志 
        /// </summary>
        protected ILog<TaskQueue> Log;

        /// <summary>
        /// 任务 队列
        /// </summary>
        public TaskQueue()
        {
            this.Log = new DefaultLog<TaskQueue>();
        }

        /// <summary>
        /// 主 线程
        /// </summary>
        protected ThreadEntity MainTask;


        /// <summary>
        /// 主 线程 逻辑
        /// </summary>
        public Action MainTaskExecute;

        /// <summary>
        /// 线程 帮助类
        /// </summary>
        public ThreadHelper ThreadHelper { get; set; } = ThreadHelper.Instance;

        /// <summary>
        /// 信号量
        /// </summary>
        protected readonly AutoResetEvent WaitHandle = new AutoResetEvent(false);
#if !(NET10 || NET11 || NET20 || NET30 || NET35)
        /// <summary>
        /// 信号量 允许 线程 数量
        /// </summary>
        protected readonly SemaphoreSlim Semaphore = new SemaphoreSlim(10);
#endif
        /// <summary>
        /// 最小 任务
        /// </summary>
        public int MinTask { get; set; } = 5;

        /// <summary>
        ///最大 任务
        /// </summary>
        public int MaxTask { get; set; } = 10;

        /// <summary>
        /// 最小空闲 任务
        /// </summary>
        public int MinIdeaTask { get; set; } = 7;

        /// <summary>
        /// 最大 空闲 任务
        /// </summary>
        public int MaxIdeaTask { get; set; } = 7;

        /// <summary>
        /// 休眠 最小 时间
        /// </summary>
        public int MinSleep { get; set; } = 100;

        /// <summary>
        /// 休眠 最大 时间
        /// </summary>
        public int MaxSleep { get; set; } = 500;

        /// <summary>
        /// 是否 推送 任务 完成  用于 检测 任务 是否 完成 不然 无法确定 任务 完成
        /// </summary>
        public bool PullTaskEnd { get; set; }

        /// <summary>
        /// 任务 是否结束
        /// </summary>
        public virtual bool TaskComplete { get {
                if (this.PullTaskEnd&&this.ThreadHelper != null&&this.ThreadHelper.Complete)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 任务 数量
        /// </summary>
        public int Count
        {
            get
            {
                return this.Queue.Count;
            }
        }

        /// <summary>
        /// 获取 任务
        /// </summary>
        /// <returns></returns>
        private Action Pop()
        {
             this.Queue.TryDequeue(out Action action);
             return action;
        }

        /// <summary>
        /// 推送 任务
        /// </summary>
        /// <param name="action"></param>
        public virtual void Push(Action action)
        {
            this.Queue.Enqueue(action);
        }

        /// <summary>
        /// 启动 执行 任务
        /// </summary>
        public virtual void Start()
        {
#if !(NET20 || NET30 || NET35)
            if (MainTask!=null&&MainTask.CancellationToken != null && !MainTask.CancellationToken.IsCancellationRequested)
            {
                return;
            }
#else
            if (MainTask != null && !MainTask.IsCancellationRequested) return;
#endif
            Action execute = this.MainTaskExecute ?? this.Execute;
            this.MainTask = new ThreadEntity(true).Create(execute);
            this.MainTask.Start();
            Initial();
        }

        /// <summary>
        /// 停止 任务
        /// </summary>
        public virtual void Stop()
        {
            this.MainTask?.Abort();
            //foreach (var item in _threadHelper.Threads)
            //{
            //    if (!item.Value.Stop)
            //    {
            //        item.Value.Abort();
            //    }
            //}
        }

        /// <summary>
        /// 分配任务 
        /// </summary>
        protected virtual void Execute()
        {
   
#if !(NET20 || NET30 || NET35)
            while (!MainTask.CancellationToken.IsCancellationRequested)
#else
            while (!MainTask.IsCancellationRequested)
#endif
            {
                if (this.Queue.Count > 0)
                {
                    //分配任务
                    this.WaitHandle.Set();
                }
                Thread.Sleep(RandomHelper.Random.Next(this.MinSleep / 10, this.MaxSleep / 10));
            }
        }

        /// <summary>
        /// 任务执行 
        /// </summary>
        /// <param name="obj"></param>
        private void DefaultTask(object obj)
        {
            
            //semaphoreSlim.Wait();//有毛线用 这种场景 下 限制
            ThreadEntity thread = obj as ThreadEntity;
            if (thread == null)
            {
                throw new ArgumentNullException("thread is not null");
            }
#if !(NET20 || NET30 || NET35)
            while (!thread.CancellationToken.IsCancellationRequested)
#else
            while (!thread.IsCancellationRequested)
#endif
            {
                try
                {
                 
                    this.WaitHandle.WaitOne(10*500);//等待分配任务
                
                    var task = this.Pop();
                    if (task != null)
                    {
                        thread.Status = false;
                        task?.Invoke();
                        thread.Status = true;
                    }
                    else
                    {
                        thread.Status = true;
                    }
                    Thread.Sleep(RandomHelper.Random.Next(this.MinSleep, this.MaxSleep));
                }
                catch (Exception e)
                {
                    Log.LogException(LogLevel.Error, "execute task fail !", e);
                }
            }
            //semaphoreSlim.Release();
        }
        
        /// <summary>
        /// 启动 任务
        /// </summary>
        private void Initial()
        {
            if (this.ThreadHelper.Threads.Count == 0)
            {
                for (int i = 0; i < this.MaxTask; i++)
                {
                    string name = $"task{i + 1}";
                    this.ThreadHelper.Create(name, (it=> { this.DefaultTask(it); }));
                    this.ThreadHelper[name].Start(this.ThreadHelper[name]);
                }
            }
        }
    }
}
#endif