//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Tunynet.Common
{
    /// <summary>
    /// 第三方帐号类型标识
    /// </summary>
    public class AccountTypeKeys
    {
        #region Instance

        private static volatile AccountTypeKeys _instance = null;
        private static readonly object lockObject = new object();

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <returns></returns>
        public static AccountTypeKeys Instance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new AccountTypeKeys();
                    }
                }
            }
            return _instance;
        }

        private AccountTypeKeys()
        { }

        #endregion Instance

        /// <summary>
        /// QQ
        /// </summary>
        /// <returns></returns>
        public string QQ()
        {
            return "QQ";
        }

        /// <summary>
        /// 微信
        /// </summary>
        /// <returns></returns>
        public string WeChat()
        {
            return "WeChat";
        }

        /// <summary>
        /// 新浪微博
        /// </summary>
        /// <returns></returns>
        public string SinaWeibo()
        {
            return "SinaWeibo";
        }

        /// <summary>
        /// 支付宝
        /// </summary>
        /// <returns></returns>
        public string AliPay()
        {
            return "AliPay";
        }
    }
}