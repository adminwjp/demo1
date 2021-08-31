#if !(NET10 || NET20 || NET30 || NET35) && !NETCOREAPP1_0 && !NETCOREAPP1_1 &&!NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Utility.Domain.Entities;
using Utility.Extensions;

namespace Utility.Domain.Extensions
{
    /// <summary>
    /// 查询 过滤 操作
    /// </summary>
    public static class NhibernateQueryFilterExtensions
    {

#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1

        /// <summary>过滤 软删除 标识数据  </summary>
        /// <returns></returns>
        public static NHibernate.Criterion.AbstractCriterion Filter<T>() where T : class
        {
            if (typeof(T).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                return NHibernate.Criterion.Expression.Eq("IsDeleted", false);
            }
            return null;
        }
#endif

    }
}
#endif