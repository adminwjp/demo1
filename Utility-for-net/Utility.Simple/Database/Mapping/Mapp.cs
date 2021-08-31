/**
  规范点 话 就 不灵活 了 使用一个接口 中同一个接口对象 (不然继承(接口)总是出现多个接口(别扭 多继承.可读性好点话 ,重复代码太多 累))
  接口 多继承 接口 (使用同一个对象代码也多 和不继承接口代码一样)
  example:
  //1.多继承(代码量多  可读性不好)
  public interface a<b> where b:a<b>{ b a1(); } 
  public interface a1:a<a1>{ a1 b1(); }
   //2.多继承 (代码量少  可读性不好 (最好这种写法(重复的逻辑多了,可重用性就不够了(浪费代码))))
  public interface a { a a1(); } 
  public interface a1:a{ a1 b1(); }
  //3.单继承 (代码量多  可读性好点)
  public interface a { a a1(); } 
  public interface a1{  a1 a1(); a1 b1(); }
   */
using System;
using System.Collections.Generic;
using System.Reflection;
using Utility.Database.Entities;
#if !(NET20 || NET30 || NET35)
using System.Linq.Expressions;
#endif

namespace Utility.Database.Mapping
{
    public class MappClass<Entity> : MappClass where Entity:class
    {

     

        public MappClass():base(typeof(Entity))
        {

        }
        public MappClass(Type classType):base(classType)
        {

        }
        public new IIdColumnMapp<Entity> Id()
        {
            var id = new IdColumnMapp<Entity>();
            id.ColumnEntry.Flag = ColumnFlag.PrimaryKey;
            ColumnEntries.Add(id.ColumnEntry);
            return id;
        }
        public new IColumnMapp<Entity> Property()
        {
            var property = new ColumnMapp<Entity>();
            property.ColumnEntry.Flag = ColumnFlag.Column;
            ColumnEntries.Add(property.ColumnEntry);
            return property;
        }
        public new IOneOneColumnMapp<Entity> OneOne()
        {
            var oneOne = new OneOneColumnMapp<Entity>();
            ColumnEntries.Add(oneOne.ColumnEntry);
            oneOne.ColumnEntry.Flag = ColumnFlag.ForeignKey;
            return oneOne;
        }
        public new IOneManyColumnMapp<Entity> OneMany()
        {
            var oneMany = new OneManyColumnMapp<Entity>();
            ColumnEntries.Add(oneMany.ColumnEntry);
            oneMany.ColumnEntry.Flag = ColumnFlag.ForeignKey;
            return oneMany;
        }


        internal new T SetProperty<T>(T mapp, string propertyName) where T : IColumnMapp<Entity>
        {
            mapp.Property(propertyName);
            return mapp;
        }

        public new IIdColumnMapp<Entity> Id(string propertyName)
        {
            return SetProperty(Id(), propertyName);
        }

        public new IColumnMapp<Entity> Property(string propertyName)
        {
            return SetProperty(Property(), propertyName);
        }
        public new IOneOneColumnMapp<Entity> OneOne(string propertyName)
        {
            return SetProperty(OneOne(), propertyName);
        }
        public new IOneManyColumnMapp<Entity> OneMany(string propertyName)
        {
            return SetProperty(OneMany(), propertyName);
        }

#if !(NET20 || NET30 || NET35)
        internal  T SetProperty<T,PropertyType>(T mapp, Expression<Func<Entity, PropertyType>> expression) where T : IColumnMapp<Entity> 
        {
            mapp.Property(expression);
            return mapp;
        }

        public  IIdColumnMapp<Entity> Id<PropertyType>(Expression<Func<Entity, PropertyType>> expression)
        {
            return SetProperty(Id(), expression);
        }

        public  IColumnMapp<Entity> Property<PropertyType>(Expression<Func<Entity, PropertyType>> expression) 
        {
            return SetProperty(Property(), expression);
        }


        public  IOneOneColumnMapp<Entity> OneOne<PropertyType>(Expression<Func<Entity, PropertyType>> expression)
        {
            return SetProperty(OneOne(), expression);
        }
        public  IOneManyColumnMapp<Entity> OneMany<PropertyType>(Expression<Func<Entity, PropertyType>> expression) 
        {
            return SetProperty(OneMany(), expression);
        }
#endif
    }

    public class MappClass
    {
        private ClassEntity tableEntry;
        private IList<PropertyEntity> columnEntries;

        public ClassEntity TableEntry { get => tableEntry = tableEntry ?? new ClassEntity(); }

        public IList<PropertyEntity> ColumnEntries 
        { 
            get
            {
                if (columnEntries == null)
                {
                    columnEntries = new Utility.Collections.Array<PropertyEntity>(20);
                    TableEntry.PropertyEntities = columnEntries;
                }
                return columnEntries;
            }
        }
        public  MappClass(Type classType)
        {
            TableEntry.ClassType = classType;
        }
        public void Table(string table)
        {
            TableEntry.Table = table;
        }
        public IIdColumnMapp Id()
        {
            var id = new IdColumnMapp();
            id.ColumnEntry.Flag = ColumnFlag.PrimaryKey;
            ColumnEntries.Add(id.ColumnEntry);
            return id;
        }
        public IColumnMapp Property()
        {
            var property = new ColumnMapp();
            property.ColumnEntry.Flag = ColumnFlag.Column;
            ColumnEntries.Add(property.ColumnEntry);
            return property;
        }
        public IOneOneColumnMapp OneOne()
        {
            var oneOne = new OneOneColumnMapp();
            ColumnEntries.Add(oneOne.ColumnEntry);
            oneOne.ColumnEntry.Flag = ColumnFlag.ForeignKey;
            return oneOne;
        }
        public IOneManyColumnMapp OneMany()
        {
            var oneMany = new OneManyColumnMapp();
            ColumnEntries.Add(oneMany.ColumnEntry);
            oneMany.ColumnEntry.Flag = ColumnFlag.ForeignKey;
            return oneMany;
        }


        internal T SetProperty<T>(T mapp, string propertyName) where T : IColumnMapp
        {
            mapp.Property(propertyName);
            return mapp;
        }

        public IIdColumnMapp Id(string propertyName)
        {
            return SetProperty(Id(), propertyName);
        }

        public IColumnMapp Property(string propertyName)
        {
            return SetProperty(Property(), propertyName);
        }
        public IOneOneColumnMapp OneOne(string propertyName)
        {
            return SetProperty(OneOne(), propertyName);
        }
        public IOneManyColumnMapp OneMany(string propertyName)
        {
            return SetProperty(OneMany(), propertyName);
        }

#if !(NET20 || NET30 || NET35)
        internal T SetProperty<T, Entity, PropertyType>(T mapp, Expression<Func<Entity, PropertyType>> expression) where T : IColumnMapp<Entity> where Entity : class
        {
            mapp.Property(expression);
            return mapp;
        }
#endif
    }

    //重复 代码 相当于复制粘贴

    public interface IColumnMapp 
    {
        /// <summary>
        /// 主键时 无效
        /// </summary>
        IColumnMapp IsNull();
        IColumnMapp Column(string column);
        IColumnMapp Comment(string comment);
        IColumnMapp Property(string propertyName);
        IColumnMapp Length(int length);
        IColumnMapp Default(string defaultValue);
        IColumnMapp Check(string check);
    }

    public interface IColumnMapp<Entity>:IColumnMapp where Entity:class
    {
#if !(NET20 || NET30 || NET35)
        IColumnMapp<Entity> Property<PropertyType>(Expression<Func<Entity, PropertyType>> expression);
#endif
    }

    public interface IIdColumnMapp: IColumnMapp
    {
        IIdColumnMapp HasIdentity();
      
        /// <summary>
        /// 主键 名称 不是 列名称
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        IIdColumnMapp Constraint(string constraint);
    }

    public interface IIdColumnMapp<Entity>: IIdColumnMapp, IColumnMapp<Entity> where Entity:class
    {
       
    }

    public interface IOneOneColumnMapp: IColumnMapp
    {
        /// <summary>
        /// 关联 外键 表 主键
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        IOneOneColumnMapp ReferenceId(string column);
        /// <summary>
        /// 关联 外键 表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        IOneOneColumnMapp Table(string table);
        /// <summary>
        /// 关联 外键 名称
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        IOneOneColumnMapp Constraint(string constraint);

    }

    public interface IOneOneColumnMapp<Entity>: IOneOneColumnMapp, IColumnMapp<Entity>  where Entity : class
    {
       
    }

    public interface IOneManyColumnMapp: IOneOneColumnMapp
    {
       
    }

    public interface IOneManyColumnMapp<Entity> : IOneManyColumnMapp, IColumnMapp<Entity>  where Entity : class
    {
       
    }

    internal class ColumnMapp : IColumnMapp
    {
        public PropertyEntity ColumnEntry => columnEntry = columnEntry ?? new PropertyEntity();
        private PropertyEntity columnEntry;

        public virtual IColumnMapp Property(PropertyInfo property)
        {
            ColumnEntry.Property = property;
            return this;
        }
        public virtual IColumnMapp Column(string column)
        {
            ColumnEntry.Column = column;
            return this;
        }


        public virtual IColumnMapp Type(Type propertyType)
        {
            ColumnEntry.PropertyType = propertyType;
            return this;
        }

        public virtual IColumnMapp Comment(string comment)
        {
            ColumnEntry.Comment = comment;
            return this;
        }
        public virtual IColumnMapp Property(string propertyName)
        {
            ColumnEntry.PropertyName = propertyName;
            return this;
        }

#if !(NET20 || NET30 || NET35)
        public virtual IColumnMapp Property<Entity, PropertyType>(Expression<Func<Entity, PropertyType>> expression) where Entity : class
        {
            if (expression == null) return this;
            var binaryExpression = expression as BinaryExpression;
            if (binaryExpression != null && expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = (MemberExpression)expression.Body;
                ColumnEntry.PropertyName = memberExpression.Member.Name;
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

                if (memberExpression.Member.MemberType == MemberTypes.Property)
                {
                    ColumnEntry.Property = memberExpression.Member as System.Reflection.PropertyInfo;
                }
#endif
                ColumnEntry.PropertyType = memberExpression.Type;
                //ColumnEntry.PropertyType = expression.ReturnType;
            }
            return this;
        }
#endif

        public virtual IColumnMapp Default(string defaultValue)
        {
            ColumnEntry.Default = defaultValue;
            return this;
        }

        public virtual IColumnMapp Check(string check)
        {
            ColumnEntry.Check = check;
            return this;
        }

        public IColumnMapp IsNull()
        {
            ColumnEntry.IsNull = true;
            return this;
        }

        public IColumnMapp Length(int length)
        {
            ColumnEntry.Length = length;
            return this;
        }
    }


    internal class ColumnMapp<Entity> : ColumnMapp, IColumnMapp<Entity> where Entity : class
    {
#if !(NET20 || NET30 || NET35)
        public IColumnMapp<Entity> Property<PropertyType>(Expression<Func<Entity, PropertyType>> expression)
        {
            base.Property(expression);
            return this;
        }
#endif
    }

    internal class IdColumnMapp : ColumnMapp, IIdColumnMapp
    {
        public IIdColumnMapp Constraint(string constraint)
        {
            ColumnEntry.Constraint = constraint;
            return this;
        }

        public IIdColumnMapp HasIdentity()
        {
            ColumnEntry.Identity = true;
            return this;
        }
    }

    internal class IdColumnMapp<Entity> : IdColumnMapp, IIdColumnMapp<Entity> where Entity : class
    {
#if !(NET20 || NET30 || NET35)
        public IColumnMapp<Entity> Property<PropertyType>(Expression<Func<Entity, PropertyType>> expression)
        {
            base.Property(expression);
            return this;
        }
#endif
    }

    internal class OneOneColumnMapp : ColumnMapp, IOneOneColumnMapp
    {
        private FKColumnEntity foreignKeyColumnEntry;
        public FKColumnEntity ForeignKeyColumnEntry
        {
            get
            {
                if (foreignKeyColumnEntry == null)
                {
                    foreignKeyColumnEntry = new FKColumnEntity();
                    ColumnEntry.FKColumnEntity = foreignKeyColumnEntry;
                }
                return foreignKeyColumnEntry;
            }
        }
        /// <summary>
        /// 关联 外键 表 主键
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public IOneOneColumnMapp ReferenceId(string column)
        {
            ForeignKeyColumnEntry.ReferenceId = column;
            return this;
        }
        /// <summary>
        /// 关联 外键 表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public IOneOneColumnMapp Table(string table)
        {
            ForeignKeyColumnEntry.ReferenceTable = table;
            return this;
        }
        /// <summary>
        /// 关联 外键 名称
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public IOneOneColumnMapp Constraint(string constraint)
        {
            ForeignKeyColumnEntry.Constraint = constraint;
            return this;
        }

    }

    internal class OneOneColumnMapp<Entity> : OneOneColumnMapp, IOneOneColumnMapp<Entity> where Entity : class
    {
#if !(NET20 || NET30 || NET35)
        public IColumnMapp<Entity> Property<PropertyType>(Expression<Func<Entity, PropertyType>> expression)
        {
            base.Property(expression);
            return this;
        }
#endif
    }
    internal class OneManyColumnMapp : OneOneColumnMapp, IOneManyColumnMapp
    {

    }

    internal class OneManyColumnMapp<Entity> : OneManyColumnMapp, IOneManyColumnMapp<Entity> where Entity : class
    {
#if !(NET20 || NET30 || NET35)
        public IColumnMapp<Entity> Property<PropertyType>(Expression<Func<Entity, PropertyType>> expression)
        {
            base.Property(expression);
            return this;
        }
#endif
    }

}
