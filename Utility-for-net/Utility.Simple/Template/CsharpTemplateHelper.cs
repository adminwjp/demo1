using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.IO;
using Utility.Helpers;

namespace Utility.Template
{
    public class EntityFactory
    {     
        public readonly static EntityFactory Default = new EntityFactory();
        public readonly IDictionary<string, string> EntityStringCodes = new Utility.Collections.Collection<string, string>(1000);
        protected virtual void GeneratorEntity(ClassModel classModel)
        {
            StringBuilder builder = new StringBuilder();//private
            builder.Append("        #region 私有变量 start.......\r\n");
            StringBuilder builder1 = new StringBuilder();//public
            builder1.Append("        #region 公共变量 start.......\r\n");
            foreach (var proModel in classModel.ProModels)
            {
                //自定义代码
                if (proModel.Custom)
                {
                    builder1.Append($"{proModel.CustomCode}\r\n");
                    continue;
                }
                proModel.PrivateProName = $"_{StringHelper.Parse(proModel.ProName, StringFormat.InitialLetterLower)}";
                if (proModel.IsSingle || proModel.NhibernateProFlag == NhibernateProFlag.Single)
                {
                    proModel.TypeName = proModel.ReferenceTypeName;//单 引用
                }
                else if (proModel.IsCollection)
                {//多 引用
                    switch (proModel.ProCollectionFlag)
                    {

                        case ProCollectionFlag.Set:
                            proModel.TypeName = $"System.Collections.Generic.ISet<{proModel.ReferenceTypeName}>";
                            break;
                        case ProCollectionFlag.List:
                            proModel.TypeName = $"System.Collections.Generic.IList<{proModel.ReferenceTypeName}>";
                            break;
                        case ProCollectionFlag.Collection:
                            proModel.TypeName = $"System.Collections.Generic.ICollection<{proModel.ReferenceTypeName}>";
                            break;
                        case ProCollectionFlag.None:
                        default:

                            break;
                    }
                }
                else if (proModel.IsEnum)
                {
                    proModel.TypeName = proModel.EnumTypeName;//枚举
                }
                else
                {
                    proModel.TypeName = CsharpTemplateHelper.Types[proModel.ProType];//普通类型
                }
                var privateStringCode = $"        private {proModel.TypeName} {proModel.PrivateProName};\\" + proModel.Comment + "\r\n";
                var publicaStringCode = proModel.Remark() + string.Join("", proModel.Attrs.Select(it => it))
                    + "        public" + (proModel.OverrideKey) + " " + proModel.TypeName + " " +
                    proModel.ProName + " { get { return this." + proModel.PrivateProName + "; } set { "
                    + (CsharpTemplateHelper.IsWpf ? ("Set(ref " + proModel.PrivateProName + ", value,\"" + proModel.ProName + "\")") : "this." + proModel.PrivateProName + "=value") + "; } }\r\n";

                builder.Append(privateStringCode);
                builder1.Append(publicaStringCode);
            }
            builder.Append("        #endregion 私有变量 end......\r\n\r\n");
            builder1.Append("        #endregion 公共变量 end...... \r\n");
            string baseClass = string.Empty;//模型继承 基类不需要 派生基类需要
            baseClass = classModel.Base == null ? "" : " : " + classModel.Base.ClassName;
            string str = classModel.Remark() + string.Join("", classModel.Attrs.Select(it => it))
                + $"    public abstract class {classModel.ClassName}" + baseClass + "\r\n"
                + "    {\r\n" + builder.ToString() + "\r\n" + builder1.ToString()
                + (CsharpTemplateHelper.IsWpf && classModel.Base == null ? ClassModel.WpfCode : "")
                + "    }\r\n";

            var str1 = $"namespace {CsharpTemplateHelper.EntityNamespace}\r\n" + "{\r\n" + $"{str}" + "}\r\n";
            EntityStringCodes[classModel.ClassName] = str1;
        }

    }
    public class NhibernateFactory
    {
        public readonly IDictionary<string, string> HbmStringCodes = new Utility.Collections.Collection<string, string>(1000);
        public readonly IDictionary<string, string> MappStringCodes = new Utility.Collections.Collection<string, string>(1000);
        public readonly IDictionary<string, string> FluMappStringCodes = new Utility.Collections.Collection<string, string>(1000);
        public readonly IDictionary<string, string> AnnotationStringCodes = new Utility.Collections.Collection<string, string>(1000);
        /// <summary>
        /// 默认 实例化
        /// </summary>
        public readonly static NhibernateFactory Default = new NhibernateFactory();

       
        public virtual string GeneratorHbm(ClassModel classModel)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder(1000);
            foreach (ProModel proModel in classModel.ProModels)
            {
                if (proModel.IsNotMapped)
                {
                    continue;
                }
                else if (proModel.IsSingle)
                {
                    var fk= $"    <!-- {proModel.Comment} -->\r\n    <many-to-one name=\"{proModel.ProName}\"" +
                        $"  column=\"{proModel.FkName}\" foreign-key=\"{proModel.ConstiantName}\" class=\"{proModel.ReferenceFullName}\"/>\r\n";
                    builder.Append(fk);
                }
                else if (proModel.IsCollection)
                {
                    string fk = $"    <!-- {proModel.Comment} -->\r\n";
                    //bag set list
                    fk += $"    <set name=\"{proModel.ProName}\"  inverse=\"true\" lazy=\"false\"  cascade=\"all\">\r\n"+
                        $"      <key column=\"{proModel.FkName}\" foreign-key=\"{proModel.ConstiantName}\"/>\r\n"
                        +"      <one-to-many class=\"{proModel.ReferenceFullName}\"/>\r\n    </{key}>\r\n";
                   
                    builder.Append(fk);
                }
                else if (proModel.Identity)
                {
                    var pk= $"    <!-- {proModel.Comment} -->\r\n    <id name=\"{proModel.ProName}\""+
                        " column=\"{proModel.Column}\" unsaved-value=\"0\">\r\n      <generator class=\"increment\" />\r\n"+
                        "      <!-- unsaved-value used to be null and generator was increment in h2.0.3 -->\r\n    </id>\r\n";
                    builder.Append(pk);
                }
                else if (proModel.Pk)
                {
                    var pk = "    <!-- {proModel.Comment} -->\r\n    <id name=\"{proModel.ProName}\" column=\"{ proModel.Column}\" />\r\n";
                    builder.Append(pk);
                }
                else
                {
                    if (proModel.IsEnum)
                    {
                        var em= $"    <!-- {proModel.Comment} -->\r\n"+
                            $"    <property column=\"{proModel.Column}\" name=\"{proModel.ProName}\" type=\"{proModel.EnumTypeName}\" />\r\n";
                        builder.Append(em);
                    }
                    else
                    {
                        if (!CsharpTemplateHelper.Types.ContainsKey(proModel.ProType))
                        {
                            continue;
                        }
                        var pro= $"    <!-- {proModel.Comment} -->\r\n"+
                            $"    <property column=\"{proModel.Column}\" name=\"{proModel.ProName}\""+
                            $" type=\"{CsharpTemplateHelper.Types[proModel.ProType].Replace("?", "")}\" "
                        + (proModel.ProType == typeof(string) && proModel.Length != 0 ? "length=\"" + proModel.Length + "\"" : "") 
                        + "/>\r\n";
                        builder.Append(pro);
                    }
                   
                }
            }
            str = "  <class name=\"" + CsharpTemplateHelper.EntityNamespace + "." + classModel.ClassName
                   + "," + CsharpTemplateHelper.AssemblyName + "\" table=\"" + classModel.Table + "\" discriminator-value=\"0\">\r\n" + builder.ToString() + "  </class>";

            return str;
        }
     


        public virtual string GeneratorMap(ClassModel classModel)
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder(1000);
            foreach (ProModel proModel in classModel.ProModels)
            {
                if (proModel.IsNotMapped)
                {
                    continue;
                }
                else if (proModel.IsSingle)
                {
                    var fk = $"            ManyToOne(x => x." + proModel.ProName + ", it => { it.ForeignKey(\"" + proModel.ConstiantName + "\"); it.Column(\"" + proModel.FkName
                      + "\"); it.Lazy(NHibernate.Mapping.ByCode.LazyRelation.NoLazy); it.NotNullable(false); it.NotFound(NHibernate.Mapping.ByCode.NotFoundMode.Ignore); });//"
                      + proModel.Comment.Trim() + "\r\n\r\n";
                    builder.Append(fk);
                }
                else if (proModel.IsCollection)
                {
                    //bag set list

                    string fk ="                Set"   + "(x => x." + proModel.ProName + ", it => { it.Lazy(NHibernate.Mapping.ByCode.CollectionLazy.Lazy); it.Key(k => { k.Column(\""
                                  + proModel.FkName + "\"); k.ForeignKey(\"" + proModel.ConstiantName + "\"); }); });//" + proModel.Comment.Trim() + "\r\n\r\n";

                    builder.Append(fk);
                }
                else if (proModel.Identity)
                {
                    var pk = "            Id(x => x." + proModel.ProName + ", map => { map.Column(\"" +
                 proModel.Column + "\"); map.Generator(NHibernate.Mapping.ByCode.Generators.Native);  });//"
                 + proModel.Comment + "\r\n\r\n";
                    builder.Append(pk);
                }
                else if (proModel.Pk)
                {
                    var pk = "            Id(x => x." + proModel.ProName + ",it=> { it.Column(\"" + proModel.Column + "\");" +
                               (proModel.ProType.IsValueType ? string.Empty : " it.Length(" + proModel.Length + ");") 
                               + " });//" + proModel.Comment.Trim() + "\r\n\r\n";
                    builder.Append(pk);
                }
                else
                {
                    if (proModel.IsEnum)
                    {
                        var em = "            Property(x => x." + proModel.ProName + ",it=> { it.Column(\"" + proModel.Column + "\"); });//" + proModel.Comment.Trim() + "\r\n\r\n";
                        builder.Append(em);
                    }
                    else
                    {
                        var pro = "            Property(x => x." + proModel.ProName + ",it=> { it.Column(\"" + proModel.Column + "\");" +
               (proModel.ProType.IsValueType ? string.Empty : " it.Length(" + proModel.Length + ");") + " });//" + proModel.Comment.Trim() + "\r\n\r\n";
                        builder.Append(pro);
                    }

                }
            }
            var map = $"{classModel.Prefix}Map";
            if (classModel.Base != null)
            {
                if (false)
                {
                    //派生基类
                    return  $"namespace {CsharpTemplateHelper.NhMappNamespace}\r\n" + "{\r\n" + "    /// <summary>nhibernate "
          + classModel.Comment + " nhibernate映射 "
 + " xml mapp 必须虚方法(属性)不然错误 ,注解可以不需要 虚方法(属性) </summary>\r\n    public abstract class "
 + map + "<T> : BaseFluentNhibernateMapp<T> where T : " + $"{classModel.ClassName}\r\n"
 + "    {\r\n        public "
       + map + "(string tableName) : base(tableName)\r\n        {\r\n"
                  + builder.ToString() + "        }\r\n\r\n    }\r\n}\r\n";
                }
                else
                {
                    //基类
                    return $"namespace {CsharpTemplateHelper.NhMappNamespace}\r\n" + "{\r\n" + "    /// <summary>nhibernate "
       + classModel.Comment + " nhibernate映射 "
   + " xml mapp 必须虚方法(属性)不然错误 ,注解可以不需要 虚方法(属性) </summary>\r\n    public abstract class "
   + map + "<T> : NHibernate.Mapping.ByCode.Conformist.ClassMapping<T> where T : " + $"{classModel.ClassName}\r\n"
   + "    {\r\n        public "
    + map + "(string tableName)\r\n        {\r\n                Table(tableName);\r\n                Lazy(false);\r\n"
                    + builder.ToString()
                    + "                this.Set();\r\n        }\r\n\r\n            protected abstract void Set();\r\n    }\r\n}\r\n";
                }



            }
            else
            {
              return $"namespace {CsharpTemplateHelper.NhMappNamespace}\r\n" + "{\r\n" + "    /// <summary> " + classModel.Comment + " nhibernate映射 "
       + " </summary>\r\n    public  class " + map + " : BaseNhibernateMapp<" + classModel.ClassName + ">\r\n    {\r\n        public "
              + map + "():base(\""
              + classModel.Table + "\")\r\n        {\r\n            }\r\n\r\n            protected override void Set()\r\n            {\r\n"
              + builder.ToString() + "        }\r\n    }\r\n}\r\n";
            }
        }
    
    }
    /// <summary>
    /// ef   
    /// </summary>
    public class EfFactory
    {
        public enum EfMappFalg
        {
            Ef = 0x0,
            EfCore = 0x1,
            All = 0x2
        }

        /// <summary>
        /// 默认 实例化
        /// </summary>
        public readonly static EfFactory Default = new EfFactory();
        public virtual string GetMappString(ClassModel classModel)
        {
            string str = "#if " + CsharpTemplateHelper.EfCoreDefine;
            str += "using Microsoft.EntityFrameworkCore;\r\nusing Microsoft.EntityFrameworkCore.Metadata.Builders;\r\n";
            // str += "#endif\r\n";
            str += "#elif "+ CsharpTemplateHelper.EfDefine;
            str += "using System.Data.Entity;\r\nusing System.Data.Entity.ModelConfiguration;\r\n";
            str += "#endif\r\n\r\nnamespace " + CsharpTemplateHelper.EfMappNamespace + "\r\n{\r\n";
            var map = $"{classModel.Prefix}Map";
            if (false)
            {
                if (classModel.Base != null)
                {
                    //派生基类
                    string efC = "    "+classModel.Remark()+"    public abstract class " +
 map + "<T> :" + $"{classModel.Base.Prefix}Map" + "<T> where T:" + classModel.Base.ClassName
 + "\r\n    {\r\n        public override  void Configure(EntityTypeBuilder<T> builder)\r\n        {\r\n            base.Configure(builder);\r\n"
 + string.Join("", classModel.ProModels.Select(it => ProMapp(it,true)))
 + "        }\r\n\r\n    }\r\n";

                    string ef = "    " + classModel.Remark() + "    public abstract class " +
             map + "<T> : " + $"{classModel.Base.Prefix}Map" + "<T> where T:" + classModel.Base.ClassName
             + "\r\n    {\r\n        public  " + map + "(string table) : base(table)\r\n        {\r\n"
             + string.Join("", classModel.ProModels.Select(it => ProMapp(it, false)))
             + "        }\r\n\r\n    }\r\n";
                    var efStr = str + $"#if {CsharpTemplateHelper.EfCoreDefine}{efC}\r\n#elif {CsharpTemplateHelper.EfDefine}{ef}\r\n#endif\r\n" + "}\r\n";
                    return efStr;
                }
                else
                {
                    //基类
                    string efC = "    " + classModel.Remark() + "    public abstract class " +
  map + "<T> : IEntityTypeConfiguration<T> where T:" + classModel.ClassName
  + "\r\n    {\r\n        public virtual  void Configure(MEntityTypeBuilder<T> builder)\r\n        {\r\n            builder.ToTable(this.TableName);\r\n"
  + string.Join("", classModel.ProModels.Select(it => ProMapp(it, true)))
  + "            this.Set(builder);\r\n\r\n        }\r\n\r\n        protected abstract void Set(EntityTypeBuilder<T> builder);\r\n        protected string TableName { get; set; }\r\n    }\r\n";

                    string ef = "    " + classModel.Remark() + "    public abstract class " +
             map + "<T> : EntityTypeConfiguration<T> where T:" + classModel.ClassName
             + "\r\n    {\r\n        public  " + map + "(string table)\r\n        {\r\n            ToTable(table);\r\n"
             + string.Join("", classModel.ProModels.Select(it => ProMapp(it, false)))
             + "            this.Set();\r\n\r\n        }\r\n\r\n        protected abstract void Set();\r\n    }\r\n";
                    var efStr = str + $"#if {CsharpTemplateHelper.EfCoreDefine}{efC}\r\n#elif {CsharpTemplateHelper.EfDefine}{ef}\r\n#endif\r\n" + "}\r\n";
                    return efStr;
                }
            }
            else
            {
                string efC = "    "+classModel.Remark()+"    public  class " +
             map + " : " + "BaseEfMapp" + "<" + classModel.ClassName + ">\r\n    {\r\n        public " + map + "()\r\n        {\r\n            this.TableName = \"" +
             classModel.Table + "\";\r\n         }\r\n\r\n        protected override void Set(EntityTypeBuilder<" + classModel.ClassName + "> builder)\r\n        {\r\n"
             + string.Join("", classModel.ProModels.Select(it => ProMapp(it, true)))
             + "        }\r\n    }\r\n";


                string ef = "    " + classModel.Remark() + "    public  class " +
             map + " : " + "BaseEfMapp" + "<" + classModel.ClassName  + ">\r\n    {\r\n        public " + map + "(): base(\"" + classModel.Table + "\")\r\n        {\r\n         }\r\n\r\n        protected override void Set()\r\n        {\r\n"
             + string.Join("", classModel.ProModels.Select(it => ProMapp(it, false)))
             + "        }\r\n    }\r\n";
                var efStr = str + $"#if {CsharpTemplateHelper.EfCoreDefine}{efC}\r\n#elif {CsharpTemplateHelper.EfDefine}{ef}\r\n#endif\r\n" + "}\r\n";
                return efStr;
            }
        }
        public virtual string Mapp(ProModel proModel, bool efCore = true)
        {
            return ProMapp(proModel, efCore);
        }

        internal static string ProMapp(ProModel proModel, bool efCore = true)
        {
            if (proModel.Custom || proModel.IsNotMapped) return string.Empty;

            string prefix = efCore ? "builder." : string.Empty;
            string mapp = string.Empty;
            if (proModel.IsEnum)
            {
                mapp = $"            {prefix}Property(it => it.{ proModel.ProName}).HasColumnName(\"{proModel.Column}\");//{proModel.Comment.Trim()}\r\n\r\n";
                return mapp;
            }
            switch (proModel.EfProFlag)
            {
                case EfProFlag.Key:
                case EfProFlag.Identity:
                    string suffix = efCore ? "//.HasAnnotation(\"MySql: ValueGenerationStrategy\", MySqlValueGenerationStrategy.IdentityColumn);" :
                        "//.HasTableAnnotation(\"MySql: ValueGenerationStrategy\", MySqlValueGenerationStrategy.IdentityColumn);";
                    mapp = $"            {prefix}HasKey(it => it.{proModel.ProName});{suffix}\r\n\r\n" +
                            $"            {prefix}Property(it => it.{proModel.ProName}).HasColumnName(\"{proModel.Column}\")"
                            + (proModel.Identity ?
                            efCore ? ".ValueGeneratedOnAdd()" : ".HasDatabaseGeneratedOption( System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)" :
                            string.Empty) + ";\r\n\r\n";
                    break;
                case EfProFlag.ForeignKey:
                    if (proModel.IsSingle)
                    {
                        if (efCore)
                        {
                            mapp = "            builder.HasOne(it => it." + proModel.ProName + ").WithMany(" +
                                (!string.IsNullOrEmpty(proModel.ReferenceProName) ? "it=>it." + proModel.ReferenceProName : "") + ").HasForeignKey(it=>it." + proModel.FkName + ").HasConstraintName(\"" + proModel.ConstiantName + "\");//" + proModel.Comment.Trim() + "\r\n\r\n";
                        }
                        else
                        {
                            mapp = "            HasOptional(it => it." + proModel.ProName + ").WithMany(it=>it." + proModel.ReferenceProName + ").Map(it=>{it.MapKey(\"" + proModel.FkName + "\"); "
                                + (string.IsNullOrEmpty(proModel.ReferenceTable) ? "" : "it.ToTable(\"" + proModel.ReferenceTable + "\");") + "}).WillCascadeOnDelete();//" + proModel.Comment.Trim() + "\r\n\r\n";
                        }
                    }
                    else
                    {
                        if (efCore)
                        {
                            //The collection argument 'foreignKeyPropertyNames' must contain at least one element
                            mapp = "            builder.HasMany(it => it." + proModel.ProName + ").WithOne(" +
                                (!string.IsNullOrEmpty(proModel.ReferenceProName) ? "it=>it." + proModel.ReferenceProName : "") + ").HasForeignKey(it=>it." + proModel.FkName + ").HasConstraintName(\"" + proModel.ConstiantName + "\").IsRequired().OnDelete(DeleteBehavior.Cascade);//" + proModel.Comment.Trim() + "\r\n\r\n";
                        }
                        else
                        {
                            mapp = "            HasMany(it => it." + proModel.ProName + ").WithOptional(it=>it." + proModel.ReferenceProName + ").Map(it=>{it.MapKey(\"" + proModel.FkName + "\"); "
                                + (string.IsNullOrEmpty(proModel.ReferenceTable) ? "" : "it.ToTable(\"" + proModel.ReferenceTable + "\");") + "}).WillCascadeOnDelete();//" + proModel.Comment.Trim() + "\r\n\r\n";
                        }
                    }
                    return mapp;
                case EfProFlag.Column:
                    mapp = $"            {prefix}Property(it => it.{proModel.ProName}).HasColumnName(\"{proModel.Column}\")"
                               + (proModel.ProType == typeof(string) && proModel.Length != 0 ? ".HasMaxLength(" + proModel.Length + ")" : "") + ";//" + proModel.Comment.Trim() + "\r\n\r\n";

                    return mapp;
            }
            return mapp;
        }


        public EfFactory()
        {

        }

    }

   

    public class ContextHelper
    {
        public static string GetDefine()
        {
            return "#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP5_0 || NETSTANDARD2_0 || NETSTANDARD2_1\r\n" +
"using Microsoft.EntityFrameworkCore;\r\n" +
//"#endif\r\n" +
"#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48\r\n" +
"using System.Data.Entity;\r\n" +
"#endif\r\n";
        }
        public static string GeneratorDbContext(List<ClassModel> classModels)
        {

            string str = $"    public class {CsharpTemplateHelper.ProgramName} : DbContext\r\n    " + "{\r\n#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP5_0 || NETSTANDARD2_0 || NETSTANDARD2_1\r\n" +
                "        public static readonly ILoggerFactory MyLoggerFactory  = LoggerFactory.Create(builder =>\r\n        {\r\n            //builder.AddConsole();\r\n";
            str += "        });\r\n//#endif\r\n\r\n";

            str += "#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 \r\n";
            str += "        public " + CsharpTemplateHelper.ProgramName + "() : base(\"Database = Example; Data Source = localhost; User Id = root; Password = wjp930514.; Old Guids = True; charset = utf8;\")\r\n        {\r\n\r\n        }\r\n";
            str += "        public " + CsharpTemplateHelper.ProgramName + "(string nameOrConnectionString) : base(nameOrConnectionString)\r\n        {\r\n\r\n        }\r\n";
            str += "#endif\r\n\r\n";

            foreach (var item in classModels)
            {
                string n = item.Prefix;
                if (n.EndsWith("y"))
                {
                    n = n.TrimEnd('y') + "ies";
                }
                else if (n.EndsWith("s"))
                {
                    n = n + "es";
                }
                else
                {
                    n += "s";
                }

                str += "        public DbSet<" + item.ClassName + "> " + n + " { get; set; }\r\n";
            }

            str += "#if NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 \r\n";
            str += "        protected override void OnModelCreating(DbModelBuilder modelBuilder)\r\n"
              + "        {\r\n";
            foreach (var item in classModels)
            {
                str += "            modelBuilder.Configurations.Add(new " + item.Prefix+"Map" + "());\r\n";
            }
            str += "            // Type type=typeof(" + classModels[0].Prefix + "Map" + ");\r\n"
                + "            // modelBuilder.Configurations.AddFromAssembly(type.Assembly);\r\n"
                + "            base.OnModelCreating(modelBuilder);\r\n"
                + "        }\r\n"
                + "//#endif\r\n\r\n";


            str += "\r\n#elif NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP5_0 || NETSTANDARD2_0 || NETSTANDARD2_1\r\n";
            str += "        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)\r\n        {\r\n"
                + "            // optionsBuilder.UseLoggerFactory(MyLoggerFactory);\r\n"
                + "            optionsBuilder.UseMySql(\"Database = Example; Data Source = localhost; User Id = root; Password = wjp930514.; Old Guids = True; charset = utf8;\");\r\n"
                + "            base.OnConfiguring(optionsBuilder);\r\n"
                + "        }\r\n"
                + "        protected override void OnModelCreating(ModelBuilder modelBuilder)\r\n"
                + "        {\r\n";
            int i = 0;
            foreach (var item in classModels)
            {
                str += "            modelBuilder.ApplyConfiguration<" + item.ClassName + ">(new " + item.Prefix+"Map" + "());\r\n";
                i++;
            }
            str += "            // Type type=typeof(" + classModels[0].Prefix + "Map" + ");\r\n"
                + "            // modelBuilder.ApplyConfigurationsFromAssembly(type.Assembly, it => it.Namespace==type.Namespace&&it.Name.EndsWith(\"EfMapp\"));\r\n"
                + "            base.OnModelCreating(modelBuilder);\r\n"
                + "        }\r\n"
                + "#endif\r\n\r\n"
                + "    }\r\n";
            string val = GetDefine() +
          "using Microsoft.Extensions.Logging; \r\n" +
          "\r\nnamespace " + CsharpTemplateHelper.ProgramName+".Ef" + "\r\n{\r\n" + str + "\r\n\r\n}\r\n";
            return val;
        }
    }

    public class DapperDalGeneraotr
    {
        public static Func<ProModel, bool> SkipAdd { get; set; } = SkipInsert;
        public static Func<ProModel, bool> SkipModify { get; set; } = SkipUpdate;

        public static bool SkipInsert(ProModel proModel)
        {
            if (proModel.ProName == "ModifyDate" || proModel.ProName == "UpdateDate")
            {
                return true;
            }
            return false;
        }
        public static bool SkipUpdate(ProModel proModel)
        {
            if (proModel.ProName == "CreateDate" || proModel.ProName == "RegDate")
            {
                return true;
            }
            return false;
        }

        public static string GetDapperDALName(ClassModel classModel)
        {
            string name = CsharpTemplateHelper.GetPrefixMappNameStatic(classModel.ClassName);
            name = $"{name}DapperDAL";
            return name;
        }

        public static string GetInsertSql(ClassModel classModel)
        {
            string sql = string.Empty;
            string suffix = string.Empty;
            bool has = false;
            List<ProModel> proModels = new List<ProModel>();
            proModels.AddRange(classModel.ProModels);
            proModels.AddRange(classModel.BaseProModels);
            foreach (var item in proModels)
            {
                if (SkipAdd(item)) continue;

                if (item.Identity)
                {
                    continue;
                }
                if (item.IsSingle)
                {
                    //if (has)
                    //{
                    //    sql += ",";
                    //    suffix += ",";
                    //    has = false;
                    //}
                    //sql += item.ProName + "Id";
                    //suffix += "@" + item.ProName + "Id";
                    //has = true;
                }
                else if (item.IsCollection)
                {
                    continue;
                }
                else
                {
                    if (has)
                    {
                        sql += ",";
                        suffix += ",";
                        has = false;
                    }
                    sql += item.Column;
                    suffix += "@" + item.Column;
                    has = true;
                }

            }
            return $"INSERT INTO {classModel.Table}({sql}) VALUES({suffix});";
        }

        public static string GetWhereIdSql(ClassModel classModel)
        {
            string where = string.Empty;
            List<ProModel> proModels = new List<ProModel>();
            proModels.AddRange(classModel.ProModels);
            proModels.AddRange(classModel.BaseProModels);
            foreach (var item in proModels)
            {
                if (item.Identity)
                {
                    where = item.ProName + "=@" + item.ProName;
                    break;
                }
                else if (item.Pk)
                {
                    where = item.ProName + "=@" + item.ProName;
                    break;
                }
            }
            return where;
        }
        public static string GetUpdateSql(ClassModel classModel)
        {
            string sql = string.Empty;
            string where = string.Empty;
            bool has = false;
            List<ProModel> proModels = new List<ProModel>();
            proModels.AddRange(classModel.ProModels);
            proModels.AddRange(classModel.BaseProModels);
            foreach (var item in proModels)
            {
                if (SkipModify(item)) continue;

                if (item.Identity)
                {
                    where = item.ProName + "=@" + @item.ProName;
                    continue;
                }
                if (item.IsSingle)
                {
                    //if (has)
                    //{
                    //    sql += ",";
                    //    has = false;
                    //}
                    //sql += item.ProName + "Id=" + "=@" + item.ProName + "Id";
                    //has = true;
                }
                else if (item.IsCollection)
                {
                    continue;
                }
                else
                {
                    if (has)
                    {
                        sql += ",";
                        has = false;
                    }
                    sql += item.Column + "=@" + item.Column;
                    has = true;
                }

            }
            return $"UPDATE  {classModel.Table} SET {sql}  WHERE {where};";
        }

        public static string GetSelectSql(ClassModel classModel)
        {
            string sql = string.Empty;
            bool has = false;
            List<ProModel> proModels = new List<ProModel>();
            proModels.AddRange(classModel.ProModels);
            proModels.AddRange(classModel.BaseProModels);
            foreach (var item in proModels)
            {
                if (has)
                {
                    sql += ",";
                    has = false;
                }
                if (item.Identity)
                {
                    sql += item.Column;
                    has = true;
                    continue;
                }
                if (item.IsSingle)
                {
                    sql += item.ProName + "Id";
                    has = true;
                }
                else if (item.IsCollection)
                {
                    continue;
                }
                else
                {
                    sql += item.Column;
                    has = true;
                }

            }
            return $"SELECT {sql} FROM {classModel.Table} ";
        }

        public static string QueryWhere(ClassModel classModel, Type idType)
        {
            string w = string.Empty;
            w += "            string where = string.Empty;\r\n";
            List<ProModel> proModels = new List<ProModel>();
            proModels.AddRange(classModel.ProModels);
            proModels.AddRange(classModel.BaseProModels);
            foreach (var item in proModels)
            {
                if (item.Custom) continue;
                if (item.Identity)
                {
                    string name = "obj." + item.ProName;
                    w += "            " + CsharpTemplateHelper.TypeIf[item.ProType].Replace("@name", name) + "\r\n";
                    w += "            {\r\n";
                    w += "               where += \" OR  " + item.ProName + "= @" + item.ProName + "  \";\r\n";
                    w += "            }\r\n";
                    continue;
                }
                if (item.IsSingle || item.NhibernateProFlag == NhibernateProFlag.Single)
                {
                    //if (classModel.AutoOne)
                    //{
                    //    string name = "obj." + item.ProName+"Id";
                    //    w += "            " + TypeIf[idType].Replace("@name", name) + "\r\n";
                    //    w += "            {\r\n";
                    //    w += "               where += \" OR " + item.ProName + "Id= @" + item.ProName + "Id   \";\r\n";
                    //    w += "            }\r\n";
                    //}
                    //else
                    //{
                    //    continue;
                    //}
                }
                else if (item.IsCollection)
                {
                    continue;
                }
                else if (item.IsEnum)
                {
                    string name = "obj." + item.ProName;
                    w += "            if(" + name + "!=" + (item.EnmumDefaultValue.IndexOf(".") > -1 ? item.EnmumDefaultValue : item.EnumTypeName + "." + item.EnmumDefaultValue) + ")\r\n";
                    w += "            {\r\n";
                    w += "               where += \" OR " + item.ProName + "= @" + item.ProName + "   \";\r\n";
                    w += "            }\r\n";
                }
                else
                {
                    string name = "obj." + item.ProName;
                    w += "            " + CsharpTemplateHelper.TypeIf[item.ProType].Replace("@name", name) + "\r\n";
                    w += "            {\r\n";
                    w += "               where += \" OR " + item.ProName + "= @" + item.ProName + "   \";\r\n";
                    w += "            }\r\n";
                }
            }
            w += "            where= where.Length>0?where.Substring(4):where;\r\n";
            w += "            return where;";
            return w;
        }

        public static string SetVal(ClassModel classModel)
        {
            string code = string.Empty;
            code += "            System.Collections.Generic.List<" + classModel.ClassName + "> res=new System.Collections.Generic.List<" + classModel.ClassName + ">();\r\n";
            code += "            foreach(var item in datas)\r\n";
            code += "            {\r\n";
            code += "               " + classModel.ClassName + "   it=new " + classModel.ClassName + "();\r\n";
            List<ProModel> proModels = new List<ProModel>();
            proModels.AddRange(classModel.ProModels);
            proModels.AddRange(classModel.BaseProModels);
            foreach (var item in proModels)
            {
                if (item.Identity)
                {
                    code += "               it." + item.ProName + "=item." + item.Column + ";\r\n";
                    continue;
                }
                if (item.IsSingle)
                {
                    code += "               it." + item.ProName + "Id=item." + item.Column + "Id;\r\n";
                }
                else if (item.IsCollection)
                {
                    continue;
                }
                else
                {
                    code += "               it." + item.ProName + "=item." + item.Column + ";\r\n";
                }
            }
            code += "               res.Add(it);//yield return it;\r\n";

            code += "            }\r\n";
            code += "            return res;\r\n";
            return code;
        }

    }

    public class EnterpriseLibraryDalGeneraotr
    {




        public static string QueryWhere(ClassModel classModel, Type idType)
        {
            string w = string.Empty;
            w += "            string where = string.Empty;\r\n";
            List<ProModel> proModels = new List<ProModel>();
            proModels.AddRange(classModel.ProModels);
            proModels.AddRange(classModel.BaseProModels);
            Action<string> action = (name) =>
            {
                w += "               var parameter = command.CreateParameter();\r\n";
                w += "               command.Parameters.Add(parameter);\r\n";
                w += "               parameter.ParameterName = \"@" + name + "\";\r\n";
                w += "               parameter.Value = obj." + name + ";\r\n";
            };
            foreach (var item in proModels)
            {
                if (item.Custom) continue;
                if (item.Identity)
                {
                    string name = "obj." + item.ProName;
                    w += "            " + CsharpTemplateHelper.TypeIf[item.ProType].Replace("@name", name) + "\r\n";
                    w += "            {\r\n";
                    w += "               where += \" OR  " + item.ProName + "= @" + item.ProName + "  \";\r\n";
                    action(item.ProName);
                    w += "            }\r\n";
                    continue;
                }
                if (item.IsSingle || item.NhibernateProFlag == NhibernateProFlag.Single)
                {
                    //if (classModel.AutoOne)
                    //{
                    //    string name = "obj." + item.ProName+"Id";
                    //    w += "            " + TypeIf[idType].Replace("@name", name) + "\r\n";
                    //    w += "            {\r\n";
                    //    w += "               where += \" OR " + item.ProName + "Id= @" + item.ProName + "Id   \";\r\n";
                    //    w += "            }\r\n";
                    //}
                    //else
                    //{
                    //    continue;
                    //}
                }
                else if (item.IsCollection)
                {
                    continue;
                }
                else if (item.IsEnum)
                {
                    string name = "obj." + item.ProName;
                    w += "            if(" + name + "!=" + (item.EnmumDefaultValue.IndexOf(".") > -1 ? item.EnmumDefaultValue : item.EnumTypeName + "." + item.EnmumDefaultValue) + ")\r\n";
                    w += "            {\r\n";
                    w += "               where += \" OR " + item.ProName + "= @" + item.ProName + "   \";\r\n";
                    action(item.ProName);
                    w += "            }\r\n";
                }
                else
                {
                    string name = "obj." + item.ProName;
                    w += "            " + CsharpTemplateHelper.TypeIf[item.ProType].Replace("@name", name) + "\r\n";
                    w += "            {\r\n";
                    w += "               where += \" OR " + item.ProName + "= @" + item.ProName + "   \";\r\n";
                    action(item.ProName);
                    w += "            }\r\n";
                }
            }
            w += "            where= where.Length>0?where.Substring(4):where;\r\n";
            w += "            return where;";
            return w;
        }


    }
    public class Dapperemplate
    {
        public ClassModel ClassModel { get; set; }
        public Type IdType { get; set; }

        protected void Initial(ClassModel classModel)
        {
            MethodModel methodModel = new MethodModel()
            {
                IsCustom = true,
                CustomMethodCode = "        /// <summary>查询wehere sql </summary>\n" +
"        /// <param name=\"obj\">" + ClassModel.Comment + "</param>\n" +
"        /// <returns></returns>\n" +
"        protected override string QueryWhere(" + ClassModel.ClassName + " obj) \n" +
"        {\n" +
DapperDalGeneraotr.QueryWhere(ClassModel, IdType) +
"            where = where.Length > 0 ? \" WHERE \"+where.Substring(4) : where;\n" +
"            return where;\n" +
"        }\r\n"
            };

            methodModel = new MethodModel()
            {
                IsCustom = true,
                CustomMethodCode = "        public override string GetTable()\n" +
          "        {\n" +
          "            return " + ClassModel.ClassName + ".TableName;\n" +
          "        }\n"
            };
        }

        public string InsetCode()
        {
            string insertSql = DapperDalGeneraotr.GetInsertSql(ClassModel);
            return $"            string sql=\"{insertSql}\";\n            return Connection.Execute(sql, obj);\n";
        }



        public string QueryCode()
        {
            string selectSql = DapperDalGeneraotr.GetSelectSql(ClassModel);
            return $"            string sql=\"{selectSql}\"+QueryWhere(obj);\n" +
                "            var datas= Connection.Query(sql, obj);\n" +
                DapperDalGeneraotr.SetVal(ClassModel) +
                $"            //var datas= Connection.GetList<{ClassModel}>(QueryWhere(obj), obj).ToList();\n" +
                "            //return datas;\n";
        }


        public string EditCode(ClassModel classMode)
        {
            string updateSql = DapperDalGeneraotr.GetUpdateSql(classMode);
            return $"            string sql=\"{updateSql}\";\n            return Connection.Execute(sql, obj);\n";
        }



    }


 
    public class CsharpTemplateHelper
    {
        public const string EfCoreDefine = "NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP5_0 || NETSTANDARD2_0 || NETSTANDARD2_1\r\n";
        public const string EfDefine = "NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 \r\n";


        public static bool IsWpf { get; set; } = false;
        public static string EntityNamespace { get; set; }
        public static string ProgramName { get; set; }
        public static string Suffix { get; set; }
        public static string AssemblyName { get; set; }
        public static string NhMappNamespace { get; set; }
        public static string EfMappNamespace { get; set; }

        public static readonly Dictionary<Type, string> Types = new Dictionary<Type, string>
        {
            [typeof(string)] = "string",
            [typeof(int)] = "int",
            [typeof(int[])] = "int[]",
            [typeof(int?)] = "int?",
            [typeof(long)] = "long",
            [typeof(long?)] = "long?",
            [typeof(bool)] = "bool",
            [typeof(bool?)] = "bool?",
            [typeof(void)] = "void",
            [typeof(DateTime?)] = "System.DateTime?",
            [typeof(DateTime)] = "System.DateTime",
        };
        public static readonly Dictionary<Type, string> TypeIf = new Dictionary<Type, string>
        {
            [typeof(string)] = "if(!string.IsNullOrEmpty(@name))",
            [typeof(int)] = "if(@name!=0)",
            [typeof(int[])] = "if(@name!=null&&@name.Length>0)",
            [typeof(int?)] = "if(@name.HasValue)",
            [typeof(long)] = "if(@name!=0)",
            [typeof(long?)] = "if(@name.HasValue)",
            [typeof(bool)] = "if(!@name)",
            [typeof(bool?)] = "if(@name.HasValue)",
            [typeof(void)] = "void",
            [typeof(DateTime?)] = "if(@name.HasValue)",
            [typeof(DateTime)] = "if(@name!=default(System.DateTime))",
        };
        private enum Way
        {
            Ef,
            Dapper,
            Nhibernate,
            Wcf,
            Remote
        }

        /// <summary>查询 linq or ef 可以 niherbate 不支持 各种操作不同导致不同麻烦 </summary>
        public class EfOrNhibernateGeneraotr
        {
            public static string QueryWhere(ClassModel classModel, Type idType)
            {
                string w = string.Empty;
                List<ProModel> proModels = new List<ProModel>();
                proModels.AddRange(classModel.ProModels);
                proModels.AddRange(classModel.BaseProModels);
                foreach (var item in proModels)
                {
                    if (item.Custom) continue;
                    if (item.Identity)
                    {
                        string name = "obj." + item.ProName;
                        w += "            " + TypeIf[item.ProType].Replace("@name", name) + "\r\n";
                        w += "            {\r\n";
                        w += "               where = where.Or(it=>it." + item.ProName + "==obj." + item.ProName + ");\r\n";
                        w += "            }\r\n";
                        continue;
                    }
                    if (item.IsSingle || item.NhibernateProFlag == NhibernateProFlag.Single)
                    {
                        //if (classModel.AutoOne)
                        //{
                        //    string name = "obj." + item.ProName;
                        //    w += "            " + TypeIf[idType].Replace("@name", name) + "\r\n";
                        //    w += "            {\r\n";
                        //    w += "               where = where.Or(it=>it." + item.ProName + "Id==obj." + item.ProName + "Id);\r\n";
                        //    w += "            }\r\n";
                        //}
                        //else
                        //{
                        //    continue;
                        //}
                    }
                    else if (item.IsCollection)
                    {
                        continue;
                    }
                    else if (item.IsEnum)
                    {
                        string name = "obj." + item.ProName;
                        w += "            if(" + name + "!=" + (item.EnmumDefaultValue.IndexOf(".") > -1 ? item.EnmumDefaultValue : item.EnumTypeName + "." + item.EnmumDefaultValue) + ")\r\n";
                        w += "            {\r\n";
                        w += "               where = where.Or(it=>it." + item.ProName + "==obj." + item.ProName + ");\r\n";
                        w += "            }\r\n";
                    }
                    else
                    {
                        string name = "obj." + item.ProName;
                        w += "            " + TypeIf[item.ProType].Replace("@name", name) + "\r\n";
                        w += "            {\r\n";
                        w += "               where = where.Or(it=>it." + item.ProName + "==obj." + item.ProName + ");\r\n";
                        w += "            }\r\n";
                    }
                }
                return w;
            }





            public static string NhibernateQueryWhere(ClassModel classModel)
            {
                string str = "        /// <summary>\n" +
                "        /// 模糊查询 通用查询 默认实现\n" +
                "        /// </summary>\n" +
                "        /// <param name=\"criterias\"></param>\n" +
                "        /// <param name=\"obj\"></param>\n" +
                "        /// <returns></returns>\n" +
                "        protected override void QueryFilterByOr(List<NHibernate.Criterion.AbstractCriterion> criterias, " + classModel.ClassName + " obj)\n" +
                "        {\n";
                List<ProModel> proModels = new List<ProModel>();
                proModels.AddRange(classModel.ProModels);
                proModels.AddRange(classModel.BaseProModels);
                foreach (var item in proModels)
                {
                    if (item.Custom) continue;
                    if (item.Identity)
                    {
                        string name = "obj." + item.ProName;
                        str += "            " + TypeIf[item.ProType].Replace("@name", name) + "\n" +
                        "            {\n" +
                        "                criterias.Add(Expression.IdEq(" + name + "));\n" +
                        "            }\n";
                        continue;
                    }
                    if (item.IsSingle || item.NhibernateProFlag == NhibernateProFlag.Single)
                    {
                        //        string name = "obj." + item.ProName + "Id";
                        //        str +=
                        //"            if(obj." + item.ProName + "!=null)\n" +
                        //"            {\n" +
                        //"                criterias.Add(Expression.Eq(\"" + item.ProName + ".Id\"," + name + "));\n" +
                        //"            }\n";
                    }
                    else if (item.IsCollection)
                    {
                        continue;
                    }
                    else if (item.IsEnum)
                    {
                        string name = "obj." + item.ProName;
                        str += "            if(" + name + "!=" + (item.EnmumDefaultValue.IndexOf(".") > -1 ? item.EnmumDefaultValue : item.EnumTypeName + "." + item.EnmumDefaultValue) + ")\r\n" +
                        "            {\n" +
                        "                criterias.Add(Expression.Eq(\"" + item.ProName + "\"," + name + "));\n" +
                        "            }\n";
                    }
                    else
                    {
                        string name = "obj." + item.ProName;
                        str += "            " + TypeIf[item.ProType].Replace("@name", name) + "\n" +
                        "            {\n" +
                        "                criterias.Add(Expression.Eq(\"" + item.ProName + "\"," + name + "));\n" +
                        "            }\n";
                    }
                }
                str += "        }\r\n";
                str += "        /// <summary>\n" +
                "        /// 模糊查询 通用查询 默认实现\n" +
                "        /// </summary>\n" +
                "        /// <param name=\"criterias\"></param>\n" +
                "        /// <param name=\"obj\"></param>\n" +
                "        /// <returns></returns>\n" +
                "        protected override void QueryFilterByAnd(List<NHibernate.Criterion.AbstractCriterion> criterias, " + classModel.ClassName + " obj)\n" +
                "        {\n" +
                "        }\r\n";
                return str;
            }
        }
        public static string GetPrefixMappNameStatic(string className)
        {
            string name = className;
            string nameLower = className.ToLower();
            if (nameLower.IndexOf("info") == name.Length - 4
                || nameLower.IndexOf("bean") == name.Length - 4)
            {
                name = name.Substring(0, name.Length - 4);
            }
            else if (nameLower.IndexOf("model") == name.Length - 5
                || nameLower.IndexOf("entry") == name.Length - 5)
            {
                name = name.Substring(0, name.Length - 5);
            }
            else if (nameLower.IndexOf("entity") == name.Length - 6)
            {
                name = name.Substring(0, name.Length - 6);
            }
            return name;
        }
      
        public void GeneratorNhXmlCode(ClassModel classModel)
        {
            var str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\" default-lazy=\"false\">\r\n"
                   + "  <!-- nhibernate   " + CsharpTemplateHelper.ProgramName + " 实体类配置映射 -->\r\n"
              + "</hibernate-mapping>\r\n";

        }

        


        private static void CSharpNhibernateMapp(ClassModel classModel, ref string baseClassName, Dictionary<string, string> mapps)
        {
            List<string> proStrs = new List<string>();
            string name = GetPrefixMappNameStatic(classModel.ClassName);
            name = $"{name}Map";
            string comment = string.Empty;

            if (classModel.Base == null)
            {
                string str = "    /// <summary>nhibernate " + (string.IsNullOrEmpty(classModel.Comment) ? classModel.ClassName : classModel.Comment)
             + " xml mapp 必须虚方法(属性)不然错误 ,注解可以不需要 虚方法(属性) </summary>\r\n    public abstract class " + name + "<T> : NHibernate.Mapping.ByCode.Conformist.ClassMapping<T> where T : " +
                    classModel.ClassName + "        {\r\n            public "
                    + name + "(string tableName)\r\n            {\r\n                Table(tableName);\r\n                Lazy(false);\r\n" + string.Join("", proStrs.Select(it => it)) + "                proModel.Set();\r\n            }\r\n            protected abstract void Set();\r\n    }\r\n";
                baseClassName = name;
                mapps.Add(name, str);
            }
            else
            {
                string str = "    /// <summary> " + (string.IsNullOrEmpty(classModel.Comment) ? classModel.ClassName : classModel.Comment)
            + " </summary>\r\n    public  class " + name + " : " + baseClassName + "<" + classModel.ClassName + ">        {\r\n            public "
                   + name + "():base(\"" + classModel.ClassName + "\")\r\n            {\r\n            }\r\n            protected override void Set()\r\n            {\r\n" + string.Join("", proStrs.Select(it => it)) + "            }\r\n    }\r\n";
                mapps.Add(name, str);
            }
        }
        /// <summary>常量 静态  </summary>
        /// <param name="classModel"></param>
        /// <param name="builder"></param>
        /// <param name="i"></param>

        public static void GetConstOrStatic(ClassModel classModel, StringBuilder builder, ref int i)
        {
            //const
            if (classModel.ConstModels.Count > 0)
            {
                builder.Append("        #region ").Append("const").Append("\r\n");
                foreach (ConstModel constModel in classModel.ConstModels)
                {
                    string str = GetConstCode(constModel);
                    builder.Append(str);
                }
                builder.Append("        #endregion ").Append("const").Append("\r\n");
                i++;
            }
            //static
            if (classModel.StaticModels.Count > 0)
            {
                builder.Append("        #region ").Append("static").Append("\r\n");
                foreach (StaticModel staticModel in classModel.StaticModels)
                {
                    builder.Append(Static(staticModel, classModel));
                }
                builder.Append("        #endregion ").Append("static").Append("\r\n");
                i++;
            }
        }

        public static string GetConstCode(ConstModel constModel)
        {
            string str = constModel.Remark() + Const(constModel.ConstType, constModel.Name, constModel.Value, constModel.ScopeFlag);
            return str;
        }

        public static string Constractor(ConstractorModel constractorModel, ClassModel classModel)
        {
            string param = string.Empty;
            if (constractorModel.ArgumentModels.Count > 0)
            {
                
            }
            return constractorModel.Remark() + param + "        public " + classModel.ClassName + "()\r\n        {\r\n" + constractorModel.ConstractorCode + "        }\r\n\r\n";
        }

        public static string Constractor(List<ConstractorModel> constractorModels, ClassModel classModel)
        {
            return string.Join("", constractorModels.Select(it => Constractor(it, classModel)));
        }

        public static string CData(CDataModel cDataModel)
        {
            return "    /// <![CDATA[\r\n    /// " + cDataModel.Comment + "\r\n    /// ]]>\r\n";
        }

 

        /// <summary>参数注释 </summary>
        /// <param name="argumentModel"></param>
        /// <returns></returns>
        public string Remark(ArgumentModel argumentModel)
        {
            return "        /// <param name=\"" + argumentModel.Name + "\">" + (string.IsNullOrEmpty(argumentModel.Comment) ? argumentModel.Name : argumentModel.Comment) + "</param>\r\n";
        }

 
        /// <summary>参数</summary>
        /// <param name="argumentModel"></param>
        /// <returns></returns>
        public static string Param(ArgumentModel argumentModel)
        {
            return $"{Types[argumentModel.ArgumentType]} {argumentModel.Name}";
        }

        public static string Const(Type type, string name, string value, ScopeFlag scopeFlag)
        {
            string val = "\"" + value + "\"";//string
            return "        public const " + Types[type] + " " + name + " = " + val + ";\r\n";
        }

        public static string Static(StaticModel staticModel, ClassModel classModel)
        {
            if (staticModel.Get && staticModel.New && staticModel.Flag == StaticFlag.Property)
            {
                string str = "        /// <summary>" + (string.IsNullOrEmpty(staticModel.Comment) ? staticModel.Name : staticModel.Comment) + "</summary>\r\n        public static "
                      +
                      classModel.ClassName + " " + staticModel.Name + "\r\n        {\r\n            get\r\n            {\r\n                return new " + classModel.ClassName
                      + "();\r\n            }\r\n        }\r\n";
                return str;
            }
            else if (staticModel.Get && staticModel.Set && staticModel.Flag == StaticFlag.Property)
            {

                string str = "        /// <summary>" + (string.IsNullOrEmpty(staticModel.Comment) ? staticModel.Name : staticModel.Comment) + "</summary>\r\n        public static string " + staticModel.Name + " { get; set; }" + (StringHelper.IsBank(staticModel.StringValue) ? string.Empty : " = " + staticModel.StringValue + ";") + "\r\n";
                return str;
            }
            return string.Empty;
        }
    
    }
}
