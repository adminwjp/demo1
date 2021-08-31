#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Utility.Helpers;

namespace Utility.Mappers
{

    /// <summary>
    ///<see cref="IMapper"/>  interface  implement.
    ///simple custom implement
    /// </summary>
    public class DefaultMapper : IMapper
    {

        /// <summary>
        /// new DefaultObjectMapper()
        /// </summary>
        public static readonly DefaultMapper Empty = new DefaultMapper();

        /// <summary>
        /// according source entity mapping  target entity
        /// </summary>
        /// <typeparam name="SoucreEntity">source entity</typeparam>
        /// <typeparam name="TargetEntity">target entity</typeparam>
        /// <param name="source">source entity</param>
        /// <returns>return target entity</returns>
        public virtual TargetEntity Map<SoucreEntity, TargetEntity>(SoucreEntity source)
        {
            return Map<TargetEntity>(source);
        }

        /// <summary>
        /// according source entity mapping  target entity
        /// </summary>
        /// <typeparam name="TargetEntity">target entity</typeparam>
        /// <param name="source">source entity</param>
        /// <returns>return target entity</returns>
        public virtual TargetEntity Map<TargetEntity>(object source)
        {
            TargetEntity target;
            if (TypeHelper.IsList(source.GetType()))
            {
                target = (TargetEntity)TypeHelper.CreateList(typeof(TargetEntity).GenericTypeArguments[0]);
                Collection(source, target);
                return target;
            }
            else if (TypeHelper.IsSet(source.GetType()))
            {
                target = (TargetEntity)TypeHelper.CreateSet(typeof(TargetEntity).GenericTypeArguments[0]);
                Collection(source, target);
                return target;
            }
            else
            {
                target = Activator.CreateInstance<TargetEntity>();
            }
            Mapp<object, TargetEntity>(source, target);
            return target;
        }
        void Collection<TargetEntity>(object source,TargetEntity target)
        {

           var it= ((IEnumerable)source).GetEnumerator();
            var type = target.GetType();
            var method = type.GetMethod("Add");
            while (it.MoveNext())
            {
                var obj = it.Current;
                var t = Activator.CreateInstance(type.GenericTypeArguments[0]);
                Mapp(obj, t);
                method.Invoke(target,new object[] { t });
            }
        }

        /// <summary>
        /// according source entity mapping  target entity
        /// </summary>
        /// <typeparam name="T">target entity</typeparam>
        /// <typeparam name="F">source entity</typeparam>
        /// <param name="source">source entity</param>
        /// <returns>return target entity</returns>
        public virtual F Mapp<T, F>(T source) where F : new()
        {
            F target = new F();
            Mapp(source, target);
            return target;
        }

        /// <summary>
        /// according source entity mapping  target entity
        /// </summary>
        /// <param name="destination"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination">target entity</typeparam>
        /// <param name="obj">source entity</param>
        /// <returns>return target entity</returns>
        public virtual TDestination Map<TSource, TDestination>(TSource obj, TDestination destination)
        {
            Mapp(obj, destination);
            return destination;
        }

        /// <summary>
        /// according source entity mapping  target entity
        /// </summary>
        /// <typeparam name="F">source entity</typeparam>
        /// <typeparam name="T">target entity</typeparam>
        /// <param name="source">source entity</param>
        /// <param name="target">target entity</param>
        public virtual void Mapp<T, F>(T source, F target)
        {
            Type sourceType = source.GetType();
            Type targetType = target.GetType();
            foreach (PropertyInfo property in sourceType.GetProperties())
            {
                PropertyInfo propertyInfo = targetType.GetProperty(property.Name);
                if (propertyInfo != null)
                {
                    var val = property.GetValue(source, null);
                    if (val == null)
                    {
                        continue;
                    }
                    var res = false;
                    foreach (var p in PropertyMap.PropertyMaps)
                    {
                        res=p.Map(source, target, property, propertyInfo, val);
                        if (res)
                        {
                            break;
                        }
                    }
                    if (!res)
                    {
                        //do nothing
                    }
                }
            }
        }
    }
    public class PropertyMap: IEqualityComparer<PropertyMap>
    {
        public static readonly HashSet<PropertyMap> PropertyMaps = new HashSet<PropertyMap>() { new PropertyMap()};
        static readonly Guid id = Guid.NewGuid();
        protected Guid Id;
        protected PropertyMap()
        {
            Id = id;
        }
        public virtual bool Map<T, F>(T source, F target, PropertyInfo sourceProperty, PropertyInfo targetProperty, object val)
        {
            if (
                (sourceProperty.PropertyType == targetProperty.PropertyType)

                   //IsGenericType
                   || (sourceProperty.PropertyType.IsGenericType && targetProperty.PropertyType.IsGenericType
                    && sourceProperty.PropertyType == targetProperty.PropertyType)
                   //IsGenericType -> not IsGenericType
                   || (sourceProperty.PropertyType.IsGenericType && !targetProperty.PropertyType.IsGenericType
                    && sourceProperty.PropertyType.GenericTypeArguments[0] == targetProperty.PropertyType)
                     //not IsGenericType ->  IsGenericType
                     || (!sourceProperty.PropertyType.IsGenericType && targetProperty.PropertyType.IsGenericType
                    && sourceProperty.PropertyType == targetProperty.PropertyType.GenericTypeArguments[0])
                    )
            {
                targetProperty.SetValue(target, val, null);
                return true;//continue;
            }
            if (MapInteger(source, target, sourceProperty, targetProperty, val))
            {
                return true;
            }
            if (MapIntegerToDate(source, target, sourceProperty, targetProperty, val))
            {
                return true;
            }
            if (MapDateToInteger(source, target, sourceProperty, targetProperty, val))
            {
                return true;
            }
            return false;
        }
        protected virtual bool MapInteger<T, F>(T source, F target,PropertyInfo sourceProperty, PropertyInfo targetProperty,object val)
        {
            //int -> int
            if (TypeHelper.IsInterger(sourceProperty.PropertyType) &&
                TypeHelper.IsInterger(targetProperty.PropertyType))
            {
                if (sourceProperty.PropertyType != targetProperty.PropertyType)
                {
                    //int?  -> long,long?
                    //long?  -> int,int?
                    //if (TypeHelper.IsICollection(typeof(Nullable<>), sourceProperty.PropertyType))
                    //{
                    //    if (val == null)
                    //    {
                    //        return true;// continue;
                    //    }
                    //}
                    //int  -> long,long?
                    //long  -> int,int?
                    targetProperty.SetValue(target, unchecked(Convert.ChangeType(val, targetProperty.PropertyType)), null);
                    return true;// continue;
                }
            }
            return false;
        }

        protected virtual bool MapIntegerToDate<T, F>(T source, F target, PropertyInfo sourceProperty, PropertyInfo targetProperty, object val)
        {
            //int -> datetime
            if (TypeHelper.IsInterger(sourceProperty.PropertyType) &&
                    TypeHelper.IsDateTime(targetProperty.PropertyType))
            {
                //int?  -> datetime,datetime?
                //long?  -> datetime,datetime?
                //if (TypeHelper.IsICollection(typeof(Nullable<>), sourceProperty.PropertyType))
                //{
                //    if (val == null)
                //    {
                //        return true;//continue;
                //    }
                //}
                var l = (long)Convert.ChangeType(val, typeof(long));
                //int datetime,datetime?
                //long datetime,datetime?
                sourceProperty.SetValue(target, CommonHelper.ToDate(l, true), null);
                return true;//continue;
            }
            return false;
        }

        protected virtual bool MapDateToInteger<T, F>(T source, F target, PropertyInfo sourceProperty, PropertyInfo targetProperty, object val)
        {
             //datetime -> int 
             if (TypeHelper.IsDateTime(sourceProperty.PropertyType) &&
                           TypeHelper.IsInterger(targetProperty.PropertyType))
            {
                //datetime  -> int,int?
                //datetime?  -> long,long?
                //if (TypeHelper.IsICollection(typeof(Nullable<>), sourceProperty.PropertyType))
                //{
                //    if (val == null)
                //    {
                //        return true;// continue;
                //    }
                //}
                var d = (DateTime)Convert.ChangeType(val, typeof(DateTime));
                //datetime -> int,int?
                //datetime  -> long,long?
                targetProperty.SetValue(target, Convert.ChangeType(CommonHelper.TotalMilliseconds(d), targetProperty.PropertyType), null);
                return true;//continue;
            }
            return false;
        }

        public bool Equals(PropertyMap x, PropertyMap y)
        {
            return (x != null && y != null && x.Id == y.Id) ;
        }

        public int GetHashCode(PropertyMap obj)
        {
            return  obj?.Id.GetHashCode()??Id.GetHashCode();
        }
    }
}
#endif