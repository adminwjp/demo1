using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Utility.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public enum MessageBeepType
    {
        /// <summary>
        /// 
        /// </summary>
        Default = -1,
        /// <summary>
        /// 
        /// </summary>
        Ok = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        Error = 0x00000010,
        /// <summary>
        /// 
        /// </summary>
        Question = 0x00000020,
        /// <summary>
        /// 
        /// </summary>
        Warning = 0x00000030,
        /// <summary>
        /// 
        /// </summary>
        Information = 0x00000040
    }
    /// <summary>
    /// 公共帮助类
    /// </summary>
    public class CommonHelper
    {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

        #region 时间戳帮助类
        public static readonly DateTime DefaultDate =new DateTime(1970, 1, 1, 0, 0, 0, 0);
        /// <summary>
        /// 1970, 1, 1, 0, 0, 0, 0 时间
        /// </summary>
        /// <returns></returns>
        public static DateTime ToLocalTime()
        {
#pragma warning disable CS0618 // 类型或成员已过时
            DateTime dt = TimeZone.CurrentTimeZone.ToLocalTime(DefaultDate);//将指定的时间转换为本地时间
#pragma warning restore CS0618 // 类型或成员已过时
            return dt;
        }

        ///<sumary>将时间戳转为本地时间</sumary>
        ///<param name="timespan">时间戳</param>
        /// <returns></returns>
        public static DateTime AddTimeSpan(long timespan)
        {
            DateTime dt = ToLocalTime();
            TimeSpan tp = new TimeSpan(timespan * 10000);
            return dt.Add(tp);
        }

        ///<sumary>将本地时间转为时间戳</sumary>
        /// <param name="currentDate">本地时间</param>
        /// <returns></returns>
        public static long TotalMilliseconds(DateTime currentDate)
        {
            DateTime dt = ToLocalTime();
            return (long)(currentDate - dt).TotalMilliseconds;
        }
        ///<sumary>将本地时间转为时间戳</sumary>
        /// <param name="totalMilliseconds">本地时间</param>
        /// <returns></returns>
        public static DateTime ToDate(long totalMilliseconds=0,bool zero=false)
        {
            if (totalMilliseconds <= 0 && zero)
            {
                return DefaultDate;
            }
            DateTime dt = DefaultDate;
            dt.AddMilliseconds(totalMilliseconds);
            return dt.AddMilliseconds(totalMilliseconds);
        }
        ///<sumary>将时间戳(秒)转为本地时间</sumary>
        /// <param name="second">时间戳(秒)</param>
        /// <returns></returns>
        public static DateTime AddSecond(long second)
        {
            return ToLocalTime().Add(new TimeSpan(second * 10000000));
        }

        ///<sumary>将本地时间转为时间戳(秒)</sumary>
        /// <param name="currentDt">本地时间</param>
        /// <returns></returns>
        public static long TotalSeconds(DateTime currentDt)
        {
            return (long)(currentDt - ToLocalTime()).TotalSeconds;
        }

        /// <summary> 获取时间戳 </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static long TotalMilliseconds(DateTime? dt = null,bool zero=false)
        {
            return dt==null&&zero ? 0:Convert.ToInt64(((dt ?? DateTime.Now) - DefaultDate).TotalMilliseconds);
        }

        /// <summary>获取时间戳 </summary>
        /// <param name="seconds">秒</param>
        /// <param name="date">时间</param>
        /// <returns></returns>
        public static long TotalMilliseconds(int seconds, DateTime? date = null)
        {
            return Convert.ToInt64(((date ?? DateTime.Now) - DefaultDate).TotalMilliseconds) + seconds * 1000;
        }

        /// <summary> 获取时间格式 </summary>
        /// <param name="dt">时间</param>
        /// <param name="format">格式化 yyyy-MM-dd hh:mm:ss</param>
        /// <returns></returns>
        public static string DateFormat(DateTime? dt = null, string format = "yyyy-MM-dd hh:mm:ss")
        {
            return (dt ?? DateTime.Now).ToString(format);
        }

        /// <summary>
        /// 时间格式 化
        /// </summary>
        /// <param name="format">yyyy-MM-dd-hh-mm-ss</param>
        /// <returns></returns>
        public static string DateString(string format = "yyyy-MM-dd-hh-mm-ss")
        {
            return DateTime.Now.ToString(format);
        }
        #endregion 时间戳帮助类

        #region win
        /// <summary>
        /// 激活，显示在最前
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="fAltTab"></param>
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frequency">声音频率（从37Hz到32767Hz）。在windows95中忽略</param>
        /// <param name="duration">声音的持续时间，以毫秒为单位。</param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")] //引入命名空间 using System.Runtime.InteropServices;
        public static extern bool Beep(int frequency, int duration);

     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MessageBeep(MessageBeepType type);

        /// <summary>
        /// 闪动窗口
        /// </summary>
        /// <param name="hWnd">要闪动的窗口</param>
        /// <param name="bInvert">闪动</param>
        [DllImport("User32")]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);
        #endregion win


#endif


    }
}
