using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Json.Extensions;
using Utility.Helpers;
using System.Reflection;

namespace Utility.Template
{
    /// <summary>静态模型 </summary>
    public class StaticModel : BaseCommentModel
    {
        public string Name { get; set; }
        public StaticFlag Flag { get; set; } = StaticFlag.None;
        public bool Get { get; set; }
        public bool Set { get; set; }
        public bool New { get; set; }
        public string StringValue { get; set; }
    }

    //model

    public abstract class BaseCommentModel
    {
        
        internal static UseLanguage Language = UseLanguage.Csharp;

        /// <summary>注释 </summary>
        public string Comment { get; set; }
        public string Remark()
        {
            switch (Language)
            {
                case UseLanguage.Java:
                    return $"/**\r\n * {this.Comment}  \r\n * */\r\n";
                case UseLanguage.Csharp:
                    return $"/// <summary>{this.Comment}  </summary>\r\n";
                case UseLanguage.Python:
                    return $"# {this.Comment}  \r\n ";
                case UseLanguage.None:
                case UseLanguage.Php:
                case UseLanguage.Go:
                case UseLanguage.Objective:
                case UseLanguage.Swift:
                case UseLanguage.Js:
                default:
                    return $"// {this.Comment}  \r\n ";

            }

        }
        public string ClassAttr(List<string> atts)
        {
            if (atts.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            foreach (var item in atts)
            {
                builder.Append($"    {item}\r\n");
            }
            string attr = builder.ToString();
            return attr;
        }

    }
    public class ProModel : BaseCommentModel
    {
        public ProModel()
        {
            this.DapperProFlag = DapperProFlag.None;
            this.EfProFlag = EfProFlag.Column;
            this.NhibernateProFlag = NhibernateProFlag.Column;
        }
        public string PrivateProName { get; set; }
        public string TypeName { get; set; }
        public string DefaultValue { get; set; }


        /// <summary>属性类型 微软自带的 </summary>
        public Type ProType { get; set; }
        public PropertyInfo Pro { get; set; }

        public bool IsEnum { get; set; }
        public string EnumTypeName { get; set; }
        public string EnmumDefaultValue { get; set; }
        /// <summary>外键名称 不是外键列名 </summary>
        public string ConstiantName { get; set; }

        /// <summary>属性名称 </summary>
        public string ProName { get; set; }


        /// <summary>属性长度 默认255 </summary>
        public int Length { get; set; } = 255;
        public bool IsNotMapped { get; set; }

        /// <summary>列名 为null or "" 则为 属性名称 </summary>
        public string Column { get; set; }

        /// <summary>是否自增 自增代表主键 </summary>
        public bool Identity { get; set; }


        /// <summary>是否主键 </summary>
        public bool Pk { get; set; }

        /// <summary>外键时用到 外键列名称 </summary>
        public string FkName { get; set; }

        /// <summary>外键时用到  引用类名 </summary>
        public string ReferenceTypeName { get; set; }
        public string ReferenceTable { get; set; }
        public string ReferenceProName { get; set; }

        public string ReferenceFullName { get; set; }

        /// <summary>属性位置 0 默认排序 1 第一个属性 -1 最后一个属性 </summary>

        public int Index { get; set; }

        /// <summary>自定义属性 </summary>
        public bool Custom { get; set; }

        /// <summary>自定义属性编码字符串 </summary>
        public string CustomCode { get; set; }
        public RequestFlag RFlag { get; set; } = RequestFlag.None;

        /// <summary>是否注释掉 </summary>
        public bool IsComment { get; set; }

        public bool IsSingle { get; set; }
        public bool IsCollection { get; set; }

        public ProCollectionFlag ProCollectionFlag { get; set; } = ProCollectionFlag.None;
        public DapperProFlag DapperProFlag { get; set; } = DapperProFlag.Column;
        public NhibernateProFlag NhibernateProFlag { get; set; } = NhibernateProFlag.Column;

        public EfProFlag EfProFlag { get; set; } = EfProFlag.Column;


        /// <summary>获取属性上自定义注释 例如//public int A{get;set;}\r\n//public int B{get;set;}\r\n </summary>

        public string PropertyComment { get; set; }

        public string OverrideKey { get; set; } = string.Empty;

        public readonly List<string> Attrs = new List<string>();
    }


    public class ArgumentModel : BaseCommentModel
    {

        /// <summary>参数名称 </summary>
        public string Name { get; set; }
        public ArgumentFalg Falg { get; set; }
        public GenericCollectionFlag CollectionFlag { get; set; } = GenericCollectionFlag.None;
        public bool HasValue { get; set; }
        public string Value { get; set; }

        /// <summary>参数类型 </summary>
        public Type ArgumentType { get; set; }
        public string CustomGenericType { get; set; }


        

        public string GeneratorMethodParam()
        {
            switch (this.Falg)
            {
                case ArgumentFalg.None:
                    return $"{CsharpTemplateHelper.Types[ArgumentType]} {this.Name}";
                case ArgumentFalg.Array:
                    return $"{CsharpTemplateHelper.Types[ArgumentType].Replace("?", "")}[] {this.Name}";
                case ArgumentFalg.Generic:
                    break;
                case ArgumentFalg.GenericUnkow:
                    return $"{this.CustomGenericType} {this.Name}";
                default:
                    break;
            }
            return string.Empty;
        }
        public const string CancellationToken = "System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken)";
        public static string GeneratorMethodParam(List<ArgumentModel> argumentModels, bool async = false)
        {
            return "(" + string.Join(",", argumentModels.Select(it => it.GeneratorMethodParam())) + (async ? "," + CancellationToken : string.Empty) + ")";
        }
    }
    public class CDataModel : BaseCommentModel
    {

    }
    public class ConstModel : BaseCommentModel
    {

        /// <summary>常量数据类型 </summary>
        public Type ConstType { get; set; } = StringHelper.Type;

        /// <summary>常量值 </summary>
        public string Value { get; set; }

        /// <summary>常量名称 </summary>
        public string Name { get; set; }

        /// <summary>常量作用域标识 </summary>

        public ScopeFlag ScopeFlag { get; set; } = ScopeFlag.None;
    }
    public class ConstractorModel : BaseCommentModel
    {
        public bool IsCustom { get; set; }
        /// <summary>自定义构造函数编码字符串 </summary>
        public string CustomConstractorCode { get; set; }
        public bool IsCustomInner { get; set; }
        /// <summary>构造函数参数 </summary>
        public readonly List<ArgumentModel> ArgumentModels = new List<ArgumentModel>();

        /// <summary>构造函数编码字符串 </summary>
        public string ConstractorCode { get; set; }

        /// <summary>构造函数域标识 </summary>
        public ScopeFlag ScopeFlag { get; set; } = ScopeFlag.None;
        public string Generator()
        {
            if (IsCustom)
            {
                return CustomConstractorCode;
            }
            string str = "        " + Remark();
            return string.Empty;
        }
        public static string Generator(List<ConstractorModel> constractorModels)
        {
            return string.Join("\r\n", constractorModels.Select(it => it.Generator()));
        }
       
    }
    /// <summary>方法模型 </summary>
    public class MethodModel : BaseCommentModel, System.ICloneable
    {
        public OverrideWay OvrirideWay { get; set; } = OverrideWay.None;

        /// <summary>方法所在位置 比如 类中 接口 中 结构体重 默认 类中 MethodWay.Class </summary>
        public ClassWay ClassWay { get; set; } = ClassWay.Class;

        /// <summary>自定义方法编码 </summary>
        public bool IsCustom { get; set; }
        public bool IsAsync { get; set; } = false;
        /// <summary>自定义方法编码字符串 </summary>
        public string CustomMethodCode { get; set; }

        /// <summary>自定义方法编码 </summary>
        public bool IsCustomInner { get; set; }
        /// <summary>自定义方法编码字符串 </summary>
        public string CustomMethodInnerCode { get; set; }
        /// <summary>自定义方法编码字符串 </summary>
        public string CustomAsyncMethodInnerCode { get; set; }

        /// <summary>方法名称 </summary>
        public string MethodName { get; set; }

        /// <summary>方法参数 </summary>
        public readonly List<ArgumentModel> ArgumentModels = new List<ArgumentModel>();
        /// <summary>方法上注解 </summary>
        public readonly List<string> Attrs = new List<string>();

        /// <summary>方法返回类型 Return 为ReturnTypeWay.None 时有效 </summary>
        public Type ReuturnType { get; set; }
        /// <summary>方法返回结果方式 ReuturnType属性在 为ReturnTypeWay.None 时有效 其他值时 CollectionFlag 属性才有效 </summary>
        public ReturnTypeWay Return { get; set; } = ReturnTypeWay.None;
     
        public string GenericUnknowType { get; set; }
        public GenericCollectionFlag CollectionFlag { get; set; } = GenericCollectionFlag.None;

        public readonly static Dictionary<GenericCollectionFlag, string> CollectionTypes = new Dictionary<GenericCollectionFlag, string>()
        {
            [GenericCollectionFlag.List] = "System.Collections.Generic.List",
            [GenericCollectionFlag.HashSet] = "System.Collections.Generic.HashSet",
            [GenericCollectionFlag.ICollection] = "System.Collections.Generic.ICollection",

        };

        /// <summary> 返回注释 </summary>
        public string ReturnComment { get; set; }

        /// <summary>方法标识 </summary>
        public MethodFlag Flag { get; set; } = MethodFlag.None;

        /// <summary>方法编码 字符串 </summary>
        public string MethodCode { get; set; }

        /// <summary>方法域标识 </summary>
        public ScopeFlag ScopeFlag { get; set; } = ScopeFlag.None;

        private bool _async;

        /// <summary>默认已实现接口方法代码字符串生成 </summary>
        /// <returns></returns>
        public string Generator()
        {
            if (this.IsCustom && this.ClassWay != ClassWay.Interface)
            {
                return CustomMethodCode;
            }
            string str = string.Empty;
            if (IsAsync && !_async)
            {
                _async = true;
                str = Generator();
                _async = false;
                IsAsync = false;
            }


            str += "        " + Remark();//方法注释
           // str += ArgumentModel.ArguementRemark(this.ArgumentModels);//方法参数注释
            str += "        ///<return>" + this.ReturnComment + "</return>\r\n";//返回值
            //注解
            foreach (var item in this.Attrs)
            {
                str += "        " + item + "\r\n";
            }

            str += "        ";//方法前空格
            switch (this.ClassWay)
            {
                case ClassWay.Class:
                case ClassWay.Struct:
                    switch (ScopeFlag)
                    {
                        case ScopeFlag.None:
                        case ScopeFlag.Public:
                            str += "public ";
                            break;
                        case ScopeFlag.Private:
                            str += "private ";
                            break;
                        case ScopeFlag.Protected:
                            str += "protected ";
                            break;
                        case ScopeFlag.Internal:
                            str += "internal ";
                            break;
                        case ScopeFlag.InternalProtected:
                            str += "internal protected ";
                            break;
                        default:
                            break;
                    }

                    break;
                case ClassWay.Interface:
                case ClassWay.None:
                default: break;
            }
            switch (ClassWay)
            {
                case ClassWay.Struct:
                case ClassWay.Class:
                    switch (this.OvrirideWay)
                    {
                        case OverrideWay.None:
                            break;
                        case OverrideWay.New:
                            str += " new  ";
                            break;
                        case OverrideWay.Override:
                            str += " override ";
                            break;
                        default:
                            break;
                    }
                    break;
                case ClassWay.None:
                case ClassWay.Interface:
                default:
                    break;
            }

            //方法返回类型 
            switch (this.Return)
            {
                case ReturnTypeWay.None:
                    if (this._async)
                    {
                        str += $"System.Threading.Tasks.Task<{CsharpTemplateHelper.Types[this.ReuturnType]}>";
                    }
                    else
                    {
                        str += CsharpTemplateHelper.Types[this.ReuturnType];
                    }
                    break;
                case ReturnTypeWay.GenericUnknow:
                    {
                        switch (this.CollectionFlag)
                        {
                            case GenericCollectionFlag.None:
                                if (this._async)
                                {
                                    str += $"System.Threading.Tasks.Task<{this.GenericUnknowType}>";
                                }
                                else
                                {
                                    str += this.GenericUnknowType;
                                }

                                break;
                            case GenericCollectionFlag.List:
                            case GenericCollectionFlag.ICollection:
                            case GenericCollectionFlag.HashSet:
                                if (this._async)
                                {
                                    str += $"System.Threading.Tasks.Task<{CollectionTypes[this.CollectionFlag]}<{this.GenericUnknowType}>>";
                                }
                                else
                                {
                                    str += $"{CollectionTypes[this.CollectionFlag]}<{this.GenericUnknowType}>";
                                }
                                break;
                            default: break;
                        }
                        break;
                    }
                case ReturnTypeWay.Generic:
                    {
                        switch (this.CollectionFlag)
                        {
                            case GenericCollectionFlag.List:
                            case GenericCollectionFlag.ICollection:
                            case GenericCollectionFlag.HashSet:
                                if (this._async)
                                {
                                    str += $"System.Threading.Tasks.Task<{CollectionTypes[this.CollectionFlag]}<{CsharpTemplateHelper.Types[this.ReuturnType]}>>";
                                }
                                else
                                {
                                    str += $"System.Threading.Tasks.Task<{CollectionTypes[this.CollectionFlag]}<{CsharpTemplateHelper.Types[this.ReuturnType]}>>";
                                }
                                break;
                            case GenericCollectionFlag.None:
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    break;
            }

            str += " " + this.MethodName + (_async ? "Async" : "");//方法名
            switch (ClassWay)
            {
                case ClassWay.None:
                    break;
                case ClassWay.Struct:
                case ClassWay.Class:
                    //if (_async)
                    //{
                    //    ArgumentModels.Add(new ArgumentModel() {CustomGenericType= "System.Threading.CancellationToken",Name= "cancellationToken",HasValue=true,Value= "default(System.Threading.CancellationToken)" });
                    //}
                    if (this.IsCustomInner)
                    {
                        if (_async)
                        {
                            str += ArgumentModel.GeneratorMethodParam(this.ArgumentModels, _async) + "\r\n        {\r\n" + this.CustomAsyncMethodInnerCode + "        }\r\n";//方法参数 code
                        }
                        else
                        {
                            str += ArgumentModel.GeneratorMethodParam(this.ArgumentModels, _async) + "\r\n        {\r\n" + this.CustomMethodInnerCode + "        }\r\n";//方法参数 code
                        }
                    }
                    break;
                case ClassWay.Interface:
                    str += ArgumentModel.GeneratorMethodParam(this.ArgumentModels, _async) + ";\r\n";//方法参数 code
                    break;
                default:
                    break;
            }
            //if (_async)
            //{
            //    ArgumentModels.RemoveAt(ArgumentModels.Count-1);
            //}
            return str;
        }


        public static string Generator(List<MethodModel> methodModels)
        {
            return string.Join("\r\n", methodModels.Select(it => it.Generator()));
        }
      

        public object Clone()
        {
            string json = this.ToJson();
            return json.ToObject<MethodModel>();
        }
    }
    /// <summary>实体类模型 </summary>
    public class ClassModel : BaseCommentModel
    {
     
        public ClassModel()
        {
            this.Attrs.Add("    [System.Serializable]\r\n");
        }

        internal const string WpfCode = "        /// <summary>设置值 虚方法 使用wpf继承就可以了</summary>\r\n        protected virtual void Set<T>(ref T oldVal, T newVal, string propertyName = null)\r\n        {\r\n            oldVal = newVal;\r\n        }\r\n";

    
        public bool Key { get; set; } = true;

        /// <summary>类名 </summary>
        public string ClassName { get; set; }

        public Type ClassType { get; set; }
        public StringFormat StringFormat = StringFormat.InitialLetterUpperCaseLower;
        public void GeneraotrByType()
        {
            this.ClassName = ClassType.Name;
            this.Comment = this.ClassName;
            this.Prefix = CsharpTemplateHelper.GetPrefixMappNameStatic(this.ClassName);
            this.Table = $"t_{StringHelper.Parse(this.Prefix, StringFormat)}";
            foreach (var item in ClassType.GetProperties())
            {
                var pro = new ProModel() { 
                    ProType=item.PropertyType,
                    ProName= item.Name,
                    Comment= item.Name,
                    Pro =item,
                    DapperProFlag= DapperProFlag.Column,
                    EfProFlag= EfProFlag.Column,
                    NhibernateProFlag= NhibernateProFlag.Column
                };
                ProModels.Add(pro);
                pro.Column = StringHelper.Parse(pro.ProName, StringFormat);


            }
        }
        /// <summary>实体类属性 </summary>

        public readonly List<ProModel> ProModels = new List<ProModel>(10);
        public readonly List<ProModel> BaseProModels = new List<ProModel>(10);
        public readonly List<ProModel> AlProModels = new List<ProModel>(10);

        /// <summary>实体类表名 为null or "" 则就是类名</summary>
        public string Table { get; set; }

        /// <summary>方法集合</summary>
        public readonly List<MethodModel> MethodModels = new List<MethodModel>();

        /// <summary>构造函数集合</summary>
        public List<ConstractorModel> ConstractorModels = new List<ConstractorModel>();

        /// <summary>常量集合</summary>
        public readonly List<ConstModel> ConstModels = new List<ConstModel>();

        /// <summary>CData注释集合</summary>
        public readonly List<CDataModel> CDataModels = new List<CDataModel>();


        /// <summary>静态集合</summary>
        public List<StaticModel> StaticModels = new List<StaticModel>();



        public readonly List<string> Attrs = new List<string>();

       

        public bool Abstract { get; set; }
        public string Prefix { get; internal set; }
        public ClassModel Base { get; set; }
    }

}

