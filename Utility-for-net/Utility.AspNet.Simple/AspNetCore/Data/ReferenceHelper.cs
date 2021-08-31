using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.AspNetCore.Data
{
    /// <summary>
    /// 去掉 引用 帮助类  防止 循环引用 造成 内存 死机
    /// 调用 错了
    /// //劲 支持 netcoreapp 3.1 其它 版本 看源码 修改
    /// </summary>
    public class ReferenceHelper
    {
        /// <summary>
        /// 过滤 掉 普通 type 
        /// </summary>
        public static HashSet<Type> Distincts = new HashSet<Type>();

        /// <summary>
        /// 过滤 掉  type 自引用 去掉
        /// </summary>
        public static Dictionary<Type,LinkedList<Type>> DistinctTypes = new Dictionary<Type, LinkedList<Type>>();

        /// <summary>
        /// 是否自引用 异常无法捕获
        /// </summary>
        public static Func<Type, Type, bool> IsReferenceEvent= (Type type, Type referenceType)=> IsReference(type,referenceType);

        /// <summary>
        /// 是否自引用 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="referenceType"></param>
        /// <returns></returns>
        public static bool IsReference(Type type,Type referenceType)
        {
            if (DistinctTypes.ContainsKey(type))
            {
                var types = DistinctTypes[type];
                if (types != null)
                {
                    var t = types.First;
                    while (t != null)
                    {
                        if (object.Equals(referenceType, t))
                        {
                            return true;
                        }
                        t = t.Next;
                    }
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            Distincts.Add(typeof(char));
            Distincts.Add(typeof(string));
            Distincts.Add(typeof(sbyte));
            Distincts.Add(typeof(byte));
            Distincts.Add(typeof(uint));
            Distincts.Add(typeof(int));
            Distincts.Add(typeof(ushort));
            Distincts.Add(typeof(short));
            Distincts.Add(typeof(ulong));
            Distincts.Add(typeof(long));
            Distincts.Add(typeof(float));
            Distincts.Add(typeof(double));
            Distincts.Add(typeof(decimal));

            Distincts.Add(typeof(char?));
            Distincts.Add(typeof(sbyte?));
            Distincts.Add(typeof(byte?));
            Distincts.Add(typeof(uint?));
            Distincts.Add(typeof(int?));
            Distincts.Add(typeof(ushort?));
            Distincts.Add(typeof(short?));
            Distincts.Add(typeof(ulong?));
            Distincts.Add(typeof(long?));
            Distincts.Add(typeof(float?));
            Distincts.Add(typeof(double?));
            Distincts.Add(typeof(decimal?));
        }
    }
}
