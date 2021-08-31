//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Tunynet.Events;

namespace Tunynet.Common
{
    /// <summary>
    /// 修改头像自定义事件
    /// </summary>
    public class CropAvatarEventArgs : CommonEventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isFirst">是否是第一次执行</param>
        public CropAvatarEventArgs(bool isFirst)
            : base(string.Empty)
        {
            this._isFirst = isFirst;
        }

        private bool _isFirst;

        /// <summary>
        /// 是否是第一次创建头像
        /// </summary>
        public bool IsFirst
        {
            get { return _isFirst; }
        }
    }
}