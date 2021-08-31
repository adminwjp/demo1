using System;
using System.Collections;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading;
using System.Windows;
using Utility.Ioc;

namespace Utility.Wpf
{
    /// <summary>
    /// wpf 启动类
    /// </summary>
    public  class WpfApplication : System.Windows.Application
    {
        public static IIocManager IocManager { get; protected set; }
        /// <summary>
        /// wpf 启动类
        /// </summary>
        public WpfApplication()
        {
            this.Activated -= Application_Activated;
            this.Deactivated -= Application_Deactivated;
            this.DispatcherUnhandledException -= Application_DispatcherUnhandledException;
            this.SessionEnding -= Application_SessionEnding;
            this.Exit -= Application_Exit;

            this.Activated += Application_Activated;
            this.Deactivated += Application_Deactivated;
            this.DispatcherUnhandledException += Application_DispatcherUnhandledException;
            this.SessionEnding += Application_SessionEnding;
            this.Exit += Application_Exit;
        }
        /// <summary>
        /// 进程 锁
        /// </summary>
        protected Mutex Mutex { get; set; }
        /// <summary>
        /// 当应用程序的顶层窗被激活触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected virtual void Application_Activated(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 当应用程序的顶层窗失去焦点触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_Deactivated(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 当应用程序的未经处理异常触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }
        /// <summary>
        /// 当windows会话被终止时触发事件 例如 用户注销或关闭计算机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            if (this.Mutex != null)
            {
                this.Mutex.ReleaseMutex();
            }
        }
        /// <summary>
        /// 当应用程序因为某种原因而被关闭时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Application_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                //向应用程序日志写入项
                if (e.ApplicationExitCode == (int)ApplicationExitCode.Failure)
                {
                    WriteApplicationLogEntry("Failure", e.ApplicationExitCode);
                }
                else
                {
                    WriteApplicationLogEntry("Success", e.ApplicationExitCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //向应用程序日志写入失败时,更新退出代码以反映写入失败
                e.ApplicationExitCode = (int)ApplicationExitCode.CanWriteToApplicationLog;
            }
            try
            {
                //保存用户程序状态
                PersistApplicationState();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //向应用程序日志写入失败时,更新退出代码以反映写入失败
                e.ApplicationExitCode = (int)ApplicationExitCode.CanpersistApplicationState;
            }
            if (this.Mutex != null)
            {
                this.Mutex.ReleaseMutex();
            }
        }
        /// <summary>
        /// 写入日志项到用户隔离储存区
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exitCode"></param>
        protected virtual void WriteApplicationLogEntry(string msg, int exitCode)
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
            using (var stream = new IsolatedStorageFileStream("log.txt", FileMode.Append, FileAccess.Write, storage))
            using (var writer = new StreamWriter(stream))
                writer.WriteLine($"{DateTime.Now}:{msg} - {exitCode}");
        }
        /// <summary>
        /// 保存应用程序状态到用户隔离储存区
        /// </summary>
        protected virtual void PersistApplicationState()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
            using (var stream = new IsolatedStorageFileStream("log.txt", FileMode.Append, FileAccess.Write, storage))
            using (var writer = new StreamWriter(stream))
                foreach (DictionaryEntry item in this.Properties)
                {
                    writer.WriteLine(item.Value);
                }
        }
        /// <summary>
        /// 退出状态
        /// </summary>
        public enum ApplicationExitCode
        {
            /// <summary>
            /// 退出成功
            /// </summary>
            Success = 0,
            /// <summary>
            /// 退出失败
            /// </summary>
            Failure = 1,
            /// <summary>
            /// 向应用程序日志写入失败时,更新退出代码以反映写入失败
            /// </summary>
            CanWriteToApplicationLog = 2,
            /// <summary>
            /// /向应用程序日志写入失败时,更新退出代码以反映写入失败
            /// </summary>
            CanpersistApplicationState = 3
        }
    }
}
