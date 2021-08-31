using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Template
{
    public enum ReturnTypeWay
    {
        /// <summary>已知的类型 </summary>
        None,
        /// <summary>未知知的泛型类型 例如 List<T> </summary>
        GenericUnknow,
        /// <summary>已知的泛型类型 </summary>
        Generic
    }
    /// <summary>静态标识 </summary>
    public enum StaticFlag
    {
        None,
        Class,
        Method,
        Property,
        Field,
        Action,
        Fun,
        Pre
    }
    /// <summary>作用域标识 </summary>
    public enum ScopeFlag
    {
        /// <summary>公共 </summary>
        None,
        /// <summary>公共 </summary>
        Public,
        /// <summary>私有 </summary>
        Private,
        /// <summary>受保护 </summary>
        Protected,
        /// <summary>同一命名空间 </summary>
        Internal,
        /// <summary>同一命名空间受保护 </summary>
        InternalProtected
    }
    public enum UseLanguage
    {
        None,
        Csharp,
        Java,
        Koltin,
        Objective,
        Swift,
        Php,
        Python,
        Dart,
        Js,
        Go
    }
    public enum ProCollectionFlag
    {
        None,
        Collection,
        Set,
        List,
    }
    public enum DapperProFlag
    {
        /// <summary>等价Column </summary>
        None,
        Key,
        Column,
        NotMapped,
        IgnoreUpdate,
        IgnoreInsert,
        IgnoreSelect
    }
    public enum EfProFlag
    {
        Column,
        None,
        Key,
        /// <summary> PrimaryKey 自增</summary>
        Identity,
        ForeignKey,
        NotMapped,

    }
    public enum NhibernateProFlag
    {
        /// <summary>等价Column </summary>
        None,
        /// <summary> PrimaryKey</summary>
        Key,
        /// <summary> PrimaryKey 自增</summary>
        Identity,
        Column,
        NotMapped,
        Single,
        Set,
        Bag,
        List,
        /// <summary> 等价set</summary>
        Collection
    }
    public enum RequestFlag
    {
        None,
        FromForm,
        FromBody,
        FromQuery,
        FromHeader,
        BindProperty
    }
    public enum MethodFlag
    {
        /// <summary>空方法 </summary>
        None,
        Abstract,
        Virtual
    }
    public enum GenericCollectionFlag
    {
        None,
        List,
        ICollection,
        HashSet
    }
    public enum OverrideWay
    {
        None,
        /// <summary>覆盖重写 </summary>
        New,
        /// <summary>抽象方法 虚方法重写 </summary>
        Override
    }

    public enum ClassWay
    {
        None,
        /// <summary>类方法 </summary>
        Class,
        /// <summary>接口方法 </summary>
        Interface,
        /// <summary>结构体方法 </summary>
        Struct,
    }
    public enum ArgumentFalg
    {
        None,
        Array,
        Generic,
        GenericUnkow
    }
}
