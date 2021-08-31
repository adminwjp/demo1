using System;
#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0)
using System.Collections.Concurrent;
#endif
using System.Collections.Generic;
using Utility.Database.Entities;

namespace Utility.Database
{
    /// <summary>
    /// base class cache provider
    /// </summary>
    public abstract class CacheProvider
    {

        /// <summary>
        /// get all type
        /// </summary>
        /// <returns></returns>
        public virtual Type[] GetTypes()
        {
            return null;
        }

        /// <summary>
        /// according class tpye get TableEntry
        /// </summary>
        /// <param name="classType">class tpye</param>
        /// <returns>return TableEntry for  according class tpye find TableEntry successs, if or return null.</returns>
        public virtual ClassEntity GetTableEntry(Type classType)
        {
            return null;
        }
    }

    /// <summary>
    /// simple collection cache
    /// </summary>
    public class SimpleCacheProvider : CacheProvider
    {
        /// <summary>
        /// new SimpleCacheProvider(  new Dictionary &lt; Type, TableEntry &gt; ())
        /// </summary>
        public static readonly SimpleCacheProvider Empty = new SimpleCacheProvider(new Dictionary<Type, ClassEntity>());

        /// <summary>
        /// cache 
        /// </summary>
        protected IDictionary<Type, ClassEntity> Caches;

        /// <summary>
        /// has param constractor 
        /// </summary>
        /// <param name="caches">simple collection cache ,not concurrent collection</param>

        public SimpleCacheProvider(IDictionary<Type, ClassEntity> caches)
        {
            this.Caches = caches;
        }

        /// <summary>
        /// new SimpleCacheProvider(  new Dictionary &lt; Type, TableEntry &gt; ())
        /// </summary>
        /// <returns></returns>
        public static CacheProvider Create()
        {
            return new SimpleCacheProvider(new Dictionary<Type, ClassEntity>());
        }
        /// <summary>
        /// get all type
        /// </summary>
        /// <returns></returns>
        public override Type[] GetTypes()
        {
            using (IEnumerator<Type> enumerator = Caches.Keys.GetEnumerator())
            {
                Type[] types = new Type[Caches.Count];
                int i = 0;
                while (enumerator.MoveNext())
                {
                    types[i] = enumerator.Current;
                    ++i;
                }
                return types;
            }
        }

        /// <summary>
        /// according class tpye get TableEntry
        /// </summary>
        /// <param name="classType">class tpye</param>
        /// <returns>return TableEntry for  according class tpye find TableEntry successs, if or return null.</returns>
        public override ClassEntity GetTableEntry(Type classType)
        {
            foreach (var item in Caches)
            {
                if (item.Key.Equals(classType)) return item.Value;
            }
            return null;
        }
    }

#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0)

    /// <summary>
    /// simple concurrent collection cache
    /// </summary>
    public class SimpleConcurrentCacheProvider : SimpleCacheProvider
    {
        /// <summary>
        /// new SimpleConcurrentCacheProvider()
        /// </summary>
        public new static readonly SimpleConcurrentCacheProvider Empty = new SimpleConcurrentCacheProvider();

        /// <summary>
        ///  no param constractor ,
        ///  default new ConcurrentDictionary &lt; Type, TableEntry &gt; ()
        /// </summary>
        public SimpleConcurrentCacheProvider() : base(new ConcurrentDictionary<Type, ClassEntity>())
        {

        }
    }
#endif
}
