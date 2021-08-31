#if !(NET10  || NET20 || NET30 || NET35)
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;
using Utility.Helpers;

namespace Utility.Domain.Extensions
{
    /// <summary>
    /// 默认操作
    /// </summary>
    public static class UpdateValueExtension
    {
        /// <summary>
        /// 手动赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="flag">1 add 2 update 3 delete</param>
        public static bool UpdateValue<T>(this T entity, int flag = 1) where T : class
        {
            object obj = entity;
            if (flag==1&&obj is IHasCreationTime creationTime)
            {
                creationTime.CreationTime = CommonHelper.TotalMilliseconds();
                return true;
            }
            else if (flag == 2 && obj is IHasModificationTime modificationTime)
            {
                modificationTime.LastModificationTime = CommonHelper.TotalMilliseconds();
                return true;
            }
            else if (flag == 3 && obj is IHasDeletionTime deletionTime)
            {
                deletionTime.DeletionTime = CommonHelper.TotalMilliseconds();
                deletionTime.IsDeleted = true;
                return true;
            }
            return false;
        }
    }
}
#endif