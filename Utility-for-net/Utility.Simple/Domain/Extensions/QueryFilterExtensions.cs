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
    public static class QueryFilterExtensions
    {
        /// <summary>过滤 软删除 标识数据  </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Filter<T>(this Expression<Func<T, bool>> where) where T : class
        {
            if (typeof(T).IsAssignableFrom(typeof(IHasDeletionTime)))
            {
                where = where.Or(LinqExpression.PropertyOrFieldBind<T, bool>(false, "IsDeleted", ((Expression left, Expression right) => Expression.IsFalse(left))));
                // where = where.Or(LinqExpression.PropertyOrFieldBind<T, bool>(false, "IsDeleted", ExpressionType.Equal);
            }
            return where;
        }

    }
}
#endif