#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.IO;
using System.Reflection;
using Utility.Xml;
using Utility.Helpers;
using System.Text;
using System.Text.RegularExpressions;
using Utility.Database.Attributes;
using Utility.Database.Entities;

namespace Utility.Database.Mapping.Resolver
{
    /// <summary>entity mapping abstract resolver class   </summary>
    public  class EmptyResolver
    {
        public readonly static EmptyResolver Empty = new EmptyResolver();
        /// <summary>
        /// create table structure  ignore primary key 
        /// </summary>
        public bool IgnorePrimaryKey { get; set; }

        /// <summary>
        /// create table structure  ignore foreign key 
        /// </summary>
        public bool IgnoreForeignKey { get; set; }
        /// <summary>
        /// 支持 其他 注解 的 特性
        /// </summary>
        public Action<PropertyInfo,PropertyEntity> OnId { get; set; }
        /// <summary>
        /// 支持 其他 注解 的 特性
        /// </summary>
        public Action<PropertyInfo, PropertyEntity> OnForeignKey { get; set; }
        /// <summary>
        /// 支持 其他 注解 的 特性
        /// </summary>
        public Action<Type, ClassEntity> OnClass { get; set; }
        #region 默认解析
        /// <summary>
        /// mapping entity information  to table structure information
        /// </summary>
        /// <param name="entityType">entity type</param>
        /// <returns></returns>

        public  virtual  ClassEntity Resolver(Type entityType)
        {
            ClassEntity tableEntry = new ClassEntity() { ClassType = entityType };//table entity table structure information 
            tableEntry.Table = ResolverTable(entityType.Name);//get table name
            PropertyInfo[] propertyInfos = ReflectHelper.GetProperties(entityType);//according the classType, get property collection for sort  after 
            tableEntry.PropertyEntities = tableEntry.PropertyEntities ?? new Utility.Collections.Array<PropertyEntity>(propertyInfos.Length);
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                PropertyInfo property = propertyInfos[i];
                Resolver(tableEntry, property);
            }
            tableEntry.RefreshPrimaryKey();
            return tableEntry;
        }

        protected virtual void Resolver(ClassEntity tableEntry,PropertyInfo property)
        {
            //创建并添加到集合
            PropertyEntity columnEntry = new PropertyEntity(property);
            try
            {
                //property name suffix Id is primary key rule
                if (!this.IgnorePrimaryKey && IsPrimaryKey(property))
                {
                    if (tableEntry.PkQuantity >= 1)
                    {
                        //found primary key ,after primary key not need.

                    }
                    else
                    {
                        ExceptionHelper.ValidatePrimaryKey(property.PropertyType);//检测 主键 类型 是否符合
                        columnEntry.Flag = ColumnFlag.PrimaryKey;
                        columnEntry.Column = ResolverColumn(property.Name);
                        //添加进去 了 还 判断 毛线 bug  操作 老是写 错误
                        ExceptionHelper.ValidateColumn(tableEntry, columnEntry);//验证列 信息
                        tableEntry.PkQuantity++;//更新主键数量 处理逻辑 时用到
                        return;//found primary key,skip the loop 
                    }
                }
                //  property type is not original type  ,store unkown property,after need handler  unkown property 
                if (!TypeHelper.IsCommonType(property.PropertyType))
                {
                   
                    //未整理 未知 类型 的属性
                    columnEntry.Flag = ColumnFlag.Ignore;
                }
                else
                {
                    //如果是枚举
                    if (property.PropertyType.IsEnum)
                    {
                        columnEntry.IsEnum = true;
                        columnEntry.Column = ResolverColumn(property.Name);
                        ExceptionHelper.ValidateColumn(tableEntry, columnEntry);//验证列 信息
                        return;
                    }
                    columnEntry.Column = ResolverColumn(property.Name);
                    ExceptionHelper.ValidateColumn(tableEntry, columnEntry);//验证列 信息
                }
            }
            finally
            {
                tableEntry.PropertyEntities.Add(columnEntry);//不想写重复的 注定 出现问题 只能这样写了
            }
        }
        #endregion  默认解析

        protected virtual bool IsPrimaryKey(PropertyInfo property)
        {
            return property.Name.EndsWith("Id");
        }

        /// <summary> 解析表名 </summary>
        /// <param name="className">类名 转换</param>
        /// <returns></returns>
        public virtual string ResolverTable(string className)
        {
            return className;
        }

        /// <summary> 解析列名 </summary>
        /// <param name="className">属性名称 转换</param>
        /// <returns></returns>
        public virtual string ResolverColumn(string propertyName)
        {
            return propertyName;
        }

        /// <summary> 解析外键名称 </summary>
        /// <param name="className">属性名称 转换</param>
        /// <returns></returns>
        public virtual string ResolverFk(string propertyName)
        {
            return $"FK_{ propertyName}";
        }

       
        #region 注解

        public static Collections.Array<BaseAttribute> GetBaseAttributes(PropertyInfo property)
        {
            var objs  = property.GetCustomAttributes(true);
            if(objs==null)
            {
                return null;
            }
            Collections.Array<BaseAttribute> baseAttributes = new Collections.Array<BaseAttribute>(objs.Length);
            for (int i = 0; i < objs.Length; i++)
            {
                if(objs[i] is BaseAttribute baseAttribute)
                {
                    baseAttributes.Add(baseAttribute);
                }
            }
            return baseAttributes;
        }

        public static BaseAttribute GetBaseAttribute(Collections.Array<BaseAttribute> baseAttributes)
        {
            if (baseAttributes == null|| baseAttributes.Count==0)
            {
                return null;
            }
            return baseAttributes[baseAttributes.Count - 1];
        }

        public static Tuple<ColumnFlag,BaseAttribute> GetTuple(PropertyInfo property)
        {
            BaseAttribute  baseAttribute= GetBaseAttribute(GetBaseAttributes(property));
            if (baseAttribute == null) return null;
            ColumnFlag columnFlag = ColumnFlag.None;
            if (baseAttribute is IdAttribute)
            {
                ExceptionHelper.ValidatePrimaryKey(property.PropertyType);
                columnFlag = ColumnFlag.PrimaryKey;
            }
            if (baseAttribute is IdentityAttribute)
            {
                if (TypeHelper.IsInterger(property.PropertyType))
                {
                    throw new Exception($"class type {property.ReflectedType} property {property} type {property.PropertyType} error,not identity");
                }
                columnFlag = ColumnFlag.Column;
            }
            else if (baseAttribute is FKAttribute|| baseAttribute is OneToOneAttribute|| baseAttribute is OneToManyAttribute)
            {
                columnFlag = ColumnFlag.ForeignKey;
            }else if(baseAttribute is IgnoreAttribute)
            {
                columnFlag = ColumnFlag.Ignore;
            }
            else if (baseAttribute is PropertyAttribute)
            {
                columnFlag = ColumnFlag.Column;
            }
            return new Tuple<ColumnFlag, BaseAttribute>(columnFlag, baseAttribute);
        }

        /// <summary>
        /// 更新 注解 的 属性 值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyEntity"></param>
        /// <param name="attribute"></param>
        public static void Bind<T>(PropertyEntity propertyEntity,T attribute)where T : AbstractAttribute
        {
            propertyEntity.Length = attribute.Length;
            propertyEntity.DataType = attribute.DataType;
            propertyEntity.IsNull = attribute.IsNull;
            propertyEntity.Comment = attribute.Comment;
        } 

        /// <summary>
        /// 获取 主键 属性 先 获取 注解; 没有 则获取符合规则的, 有多个 则 为普通列, 否则为主键
        /// </summary>
        /// <param name="property"></param>
        /// <param name="pkCount"></param>
        /// <returns></returns>
        protected virtual PropertyEntity GetId(PropertyInfo property,int pkCount)
        {
            IdAttribute idAttribute = AttributeHelper.Get<IdAttribute>(property.GetCustomAttributes(false));//获取注解 IdAttribute
            PropertyEntity propertyEntity = new PropertyEntity() { Property=property};
            //获取注解 IdAttribute 声明为主键 
            if (idAttribute != null)
            {
                ExceptionHelper.ValidatePrimaryKey(property.PropertyType);//检测 主键 类型 是否符合
                propertyEntity.Flag = ColumnFlag.PrimaryKey;
                propertyEntity.PropertyName = !string.IsNullOrEmpty(idAttribute.Column) ? idAttribute.Column : ResolverColumn(propertyEntity.PropertyName);
                Bind(propertyEntity, idAttribute);
                return propertyEntity;
            }

            //非注解
            bool pk = IsPrimaryKey(property);//是否符合规则 
            //符合规则已有 多个按普通列存储 没有则 按 普通列存在 
            if ((pkCount >= 1 && pk) || !pk)
            {
                propertyEntity.Flag = TypeHelper.IsCommonType(property.PropertyType)?ColumnFlag.Column:ColumnFlag.Ignore;
            }
            else
            {
                ExceptionHelper.ValidatePrimaryKey(property.PropertyType);//检测 主键 类型 是否符合
                propertyEntity.Flag = ColumnFlag.PrimaryKey;
            }
            propertyEntity.PropertyName = ResolverColumn(propertyEntity.PropertyName);
            return propertyEntity;
        }

        /// <summary>
        /// 获取 普通列 属性 先 获取 注解; 没有 则获取符合规则的, 有 则 为普通列, 否则为未知 列 (可能外键 )
        /// </summary>
        /// <param name="property"></param>
        /// <param name="pkCount"></param>
        /// <returns></returns>
        protected virtual PropertyEntity GetColumn(PropertyInfo property )
        {
            PropertyAttribute propertyAttribute = AttributeHelper.Get<PropertyAttribute>(property.GetCustomAttributes(false));//获取注解 IdAttribute
            PropertyEntity propertyEntity = new PropertyEntity() { Property = property,Flag = ColumnFlag.Column };
            //获取注解 PropertyAttribute 声明为主键 
            if (propertyAttribute != null)
            {
                if (!TypeHelper.IsCommonType(property.PropertyType))
                {
                    throw new Exception($" class type {property.ReflectedType} property name {property.Name} target  type {property.PropertyType} not support! ");
                }
                propertyEntity.Flag = ColumnFlag.Column;
                propertyEntity.PropertyName = !string.IsNullOrEmpty(propertyAttribute.Column) ? propertyAttribute.Column : ResolverColumn(propertyEntity.PropertyName);
                Bind(propertyEntity, propertyAttribute);
                return propertyEntity;
            }

            //非注解
            propertyEntity.Flag = TypeHelper.IsCommonType(property.PropertyType) ? ColumnFlag.Column : ColumnFlag.Ignore;
            propertyEntity.PropertyName =  ResolverColumn(propertyEntity.PropertyName);
            return propertyEntity;
        }
        /// <summary>
        /// 注解 将实体信息映射为表结构信息 
        /// </summary>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>

        public virtual ClassEntity AnnotationResolver(Type entityType)
        {
            ClassAttribute classAttribute = AttributeHelper.Get<ClassAttribute>(entityType.GetCustomAttributes(false));//获取注解 ClassAttribute
            ClassEntity tableEntry = new ClassEntity() { ClassType = entityType };
            //不存在 注解 ClassAttribute 使用 类名 作为表名
            if (classAttribute == null)
            {
                tableEntry.Table = ResolverColumn(entityType.Name);
            }
            else
            {
                //存在注解 ClassAttribute 
                tableEntry.Table = !string.IsNullOrEmpty(classAttribute.Table) ? classAttribute.Table : ResolverColumn(entityType.Name);
            }
            if (OnClass != null)
            {
                OnClass?.Invoke(entityType, tableEntry);
            }
            PropertyInfo[] properties = ReflectHelper.GetProperties(entityType);
            tableEntry.PropertyEntities = tableEntry.PropertyEntities ?? new Utility.Collections.Array<PropertyEntity>(properties.Length);
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var tuple = GetTuple(property);
                if (tuple == null)
                {
                    //注解不存在 普通解析
                    Resolver(tableEntry,property);
                    continue;
                }
                PropertyEntity propertyEntity = new PropertyEntity(property);
                propertyEntity = GetId(property, tableEntry.PkQuantity);
                if(propertyEntity.Flag== ColumnFlag.PrimaryKey)
                {
                    OnId.Invoke(property,propertyEntity);
                    propertyEntity.Flag = ColumnFlag.PrimaryKey;
                }
                switch (tuple.Item1)
                {
                    case ColumnFlag.None:
                        break;
                    case ColumnFlag.Column:
                        {
                            propertyEntity.Flag = ColumnFlag.Column;
                            tableEntry.PropertyEntities.Add(propertyEntity);
                        }
                        break;
                    case ColumnFlag.PrimaryKey:
                        {
                            propertyEntity.Flag = ColumnFlag.PrimaryKey;
                            tableEntry.PropertyEntities.Add(propertyEntity);
                        }
                        break;
                    case ColumnFlag.ForeignKey:
                        {
                            if(tuple.Item2 is OneToOneAttribute oneAttribute)
                            {
                                //单 外键
                            }
                            else if (tuple.Item2 is OneToManyAttribute oneToManyAttribute)
                            {
                                //集合 外键
                            }
                            else
                            {
                                //普通外键
                            }
                            tableEntry.PropertyEntities.Add(propertyEntity);
                            propertyEntity.Flag = ColumnFlag.ForeignKey;

                        }
                        break;
                    case ColumnFlag.Ignore:
                        {
                            propertyEntity.Flag = ColumnFlag.Ignore;
                            tableEntry.PropertyEntities.Add(propertyEntity);
                        }
                        break;
                    default:
                        break;
                }
            }
            tableEntry.RefreshPrimaryKey();
            return tableEntry;
        }
#endregion  注解

#region 实现映射表基于xml(Xml.Linq.XDocment 或 XmlDocment)实现

        /// <summary>
        /// 将xml信息映射为表结构信息 名称不区分大小写 但 值区分(bool 不区分大小写)
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <returns></returns>
        public virtual ClassEntity[] XmlResolver(string path,bool xmlLinq=true)
        {
            Utility.Collections.Array<ClassEntity> tableEntries = new  Collections.Array<ClassEntity>();
            ValidateHelper.ValidateArgumentNull("path", path);
            if (File.Exists(path))
            {
                XmlHelper xmlHelper = new XmlHelper() { IsXmlDocment = !xmlLinq };
                xmlHelper.Initial(path);
                if (xmlHelper.Element.NameEqual("Mapping"))
                {
                    throw new Exception("Xml 根节点不是Mapping!");
                }

                string nameSpace = xmlHelper.Element.GetValue("Namespace");
                if (!xmlHelper.Element.HasNodes)
                {
                    return null;
                }
                foreach (var item in xmlHelper.Element.Nodes)
                {
                    if (item.NameEqual("Class"))
                    {
                        string typeName = item.GetValue("Type");
                        if (!string.IsNullOrEmpty(typeName))
                        {
                            throw new Exception("Class元素不存在属性Type且不能为空!");
                        }
                        if (!typeName.Contains(",") && string.IsNullOrEmpty(nameSpace))
                        {
                            typeName = $"{typeName},{nameSpace}";
                        }
                        Type type = Type.GetType(typeName);
                        if (type == null)
                        {
                            throw new Exception($"类型 {typeName} 找不到!");
                        }
                        ClassEntity tableEntry = new ClassEntity();
                        tableEntry.ClassType = type;
                        tableEntry.Table = item.GetValue("Table");
                        if (string.IsNullOrEmpty(tableEntry.Table))
                        {
                            tableEntry.Table = ResolverTable(type.Name);
                        }
                        SetColumn(tableEntry, item);
                        tableEntries.Add(tableEntry);
                    }
                }
            }
            return tableEntries.ToArray() ;
        }
        private void SetColumn(ClassEntity tableEntry, NodeEntity nodeEntity)
        {
            if (!nodeEntity.HasNodes) return;
            foreach (NodeEntity item in nodeEntity.Nodes)
            {
                PropertyEntity columnEntry = new PropertyEntity();
                tableEntry.PropertyEntities.Add(columnEntry);
                if (item.NameEqual("Id"))
                {
                    Set(tableEntry, columnEntry, item);

                }
                else if (item.NameEqual("Column"))
                {
                    Set(tableEntry, columnEntry, item);
                }
                else if (item.NameEqual("Ignore"))
                {
                    Set(tableEntry, columnEntry, item);
                }
                else if (item.NameEqual("ForeignKey"))
                {
                    Set(tableEntry, columnEntry, item);
                    if (item.HasNodes)
                    {
                        tableEntry.FkQuantity++;
                        foreach (var fk in item.Nodes)
                        {
                            if (fk.NameEqual("OneToOne"))
                            {
                                BaseEntity fKEntry = new BaseEntity();
                                FKColumnEntity foreignKeyColumn = GetFkColumn(tableEntry, fKEntry, item);
                                columnEntry.FKColumnEntity = foreignKeyColumn;
                                foreignKeyColumn.Single = fKEntry;
                            }
                            else if (fk.NameEqual("OneToMany"))
                            {
                                BaseEntity fKEntry = new BaseEntity();
                                FKColumnEntity foreignKeyColumn = GetFkColumn(tableEntry, fKEntry, item);
                                columnEntry.FKColumnEntity = foreignKeyColumn;
                                foreignKeyColumn.Many = fKEntry;
                            }
                            else if (fk.NameEqual("Basic"))
                            {
                                BaseEntity fKEntry = new BaseEntity();
                                FKColumnEntity foreignKeyColumn = GetFkColumn(tableEntry, fKEntry, item);
                                columnEntry.FKColumnEntity = foreignKeyColumn;
                                foreignKeyColumn.Basic = fKEntry;
                            }
                        }
                    }
                }

            }
        }

        private FKColumnEntity GetFkColumn(ClassEntity tableEntry, BaseEntity fKEntry, NodeEntity node)
        {
            FKColumnEntity foreignKeyColumn = tableEntry.GetFk(fKEntry);
            if (foreignKeyColumn == null)
            {
                foreignKeyColumn = new FKColumnEntity();
            }
            foreignKeyColumn.ReferenceTable = node.GetValue("ReferenceTable");
            string attribute = node.GetValue("ReferenceType");
            if (!string.IsNullOrEmpty(attribute))
            {
                foreignKeyColumn.ReferenceType = Type.GetType(attribute);
            }
            foreignKeyColumn.Constraint = node.GetValue("Constraint");
            return foreignKeyColumn;
        }

        private void Set(ClassEntity tableEntry, PropertyEntity columnEntry, NodeEntity node)
        {
            columnEntry.Column = node.GetValue("Column");
            columnEntry.Column = ResolverColumn(columnEntry.Column);
            if (string.IsNullOrEmpty(columnEntry.Column))
            {
                columnEntry.Column = ResolverColumn(columnEntry.PropertyName);
            }
            columnEntry.IsNull = true.ToString().ToLower().Equals(node.GetValue("IsNull"));
            columnEntry.Identity = true.ToString().ToLower().Equals(node.GetValue("Identity"));

            int.TryParse(node.GetValue("Length"), out int length);
            columnEntry.Length = length;
            columnEntry.Comment = node.GetValue("Comment");
            columnEntry.Default = node.GetValue("Default");
            columnEntry.Check = node.GetValue("Check");
        }


        protected virtual void ToCreatrTableXml(NodeEntity tableNode, ClassEntity tableEntry)
        {
            //ToCreatrTableXml(tableNode, "Sqlite", tableEntry.DropTableIfExists?.SqliteValue, tableEntry.CreateTableIfNotExists?.SqliteValue);
            //ToCreatrTableXml(tableNode,"MySql", tableEntry.DropTableIfExists?.MySqlValue, tableEntry.CreateTableIfNotExists?.MySqlValue);
            //ToCreatrTableXml(tableNode, "SqlServer", tableEntry.DropTableIfExists?.SqlServerValue, tableEntry.CreateTableIfNotExists?.SqlServerValue);
            //ToCreatrTableXml(tableNode, "Postgre", tableEntry.DropTableIfExists?.PostgreValue, tableEntry.CreateTableIfNotExists?.PostgreValue);
            //ToCreatrTableXml(tableNode, "Oracle", tableEntry.DropTableIfExists?.OracleValue, tableEntry.CreateTableIfNotExists?.OracleValue);
        }
        private void ToCreatrTableXml(NodeEntity table, string name,string dropVal,string createVal)
        {
            StringBuilder builder = new StringBuilder();
            var has= ToCreatrTableXml(builder,dropVal, createVal);
            if (has)
            {
                builder.Append("\r\n");
                string sql = builder.ToString();
                ToCreatrTableXml(table, name, sql);
            }
           
        }
        private void ToCreatrTableXml(NodeEntity table, string name,string sql)
        {
            //xml 显示好别扭 可读性差
            sql = Regex.Replace(sql, "(.*?)\r\n", it =>
            {
                return "  " + it.Value;
            });
            NodeEntity classElement = new NodeEntity() { Name = name, Value = "\r\n" + sql};
            table.Add(classElement);

        }
        protected virtual void ToCreatrTableXml(NodeEntity tableNode, ClassEntity[] tableEntries)
        {
            StringBuilder builderSqlite = new StringBuilder();
            StringBuilder builderMysql = new StringBuilder();
            StringBuilder builderSqlServer = new StringBuilder();
            StringBuilder builderPostgre = new StringBuilder();
            StringBuilder builderOracle = new StringBuilder();
            for (int i = 0; i < tableEntries.Length; i++)
            {
                ClassEntity tableEntry = tableEntries[i];
                //ToCreatrTableXml(builderSqlite, tableEntry.DropTableIfExists?.SqliteValue, tableEntry.CreateTableIfNotExists?.SqliteValue);
                //ToCreatrTableXml(builderMysql, tableEntry.DropTableIfExists?.MySqlValue, tableEntry.CreateTableIfNotExists?.MySqlValue);
                //ToCreatrTableXml(builderSqlServer, tableEntry.DropTableIfExists?.SqlServerValue, tableEntry.CreateTableIfNotExists?.SqlServerValue);
                //ToCreatrTableXml(builderPostgre, tableEntry.DropTableIfExists?.PostgreValue, tableEntry.CreateTableIfNotExists?.PostgreValue);
                //ToCreatrTableXml(builderOracle, tableEntry.DropTableIfExists?.OracleValue, tableEntry.CreateTableIfNotExists?.OracleValue);
            }

            string sqliteSql = builderSqlite.ToString();
            string mysqlSql = builderMysql.ToString();
            string sqlserverSql = builderSqlServer.ToString();
            string postgreSql = builderPostgre.ToString();
            string oracleSql = builderOracle.ToString();
            if(sqliteSql.Length>0)
            {
                ToCreatrTableXml(tableNode, "Sqlite", sqliteSql);
            }
            if (mysqlSql.Length > 0)
            {
                ToCreatrTableXml(tableNode, "MySql", mysqlSql);
            }
            if (sqlserverSql.Length > 0)
            {
                ToCreatrTableXml(tableNode, "SqlServer", sqlserverSql);
            }
            if (postgreSql.Length > 0)
            {
                ToCreatrTableXml(tableNode, "Postgre", postgreSql);
            }
            if (oracleSql.Length > 0)
            {
                ToCreatrTableXml(tableNode, "Oracle", oracleSql);
            }

        }

        private bool ToCreatrTableXml(StringBuilder builder, string dropVal, string createVal)
        {
            bool has = false;
            if (!string.IsNullOrEmpty(dropVal))
            {
                has = true;
                builder.Append(dropVal);
                builder.Append("\r\n");
            }
            if (!string.IsNullOrEmpty(createVal))
            {
                builder.Append(createVal);
                builder.Append("\r\n");
                has = true;
            }
            return has;
        }
        /// <summary>
        /// 将实体信息映射为表结构信息存储为xml 
        /// </summary>
        /// <param name="tableEntry">映射信息</param>
        protected virtual void ToXml(XmlHelper xmlHelper,ClassEntity tableEntry)
        {

            ElementEntity element = xmlHelper.Element;
           
            NodeEntity classElement = new NodeEntity() { Name = "Class" };
            element.Add(classElement);
            classElement.Add(new AttributeEntity("Namespace", tableEntry.ClassType.Namespace));
            classElement.Comment = tableEntry.Comment;//添加注释
            classElement.Add(new AttributeEntity("Type", tableEntry.ClassType.FullName));
            classElement.Add(new AttributeEntity("Table", tableEntry.Table));
            foreach (var item in tableEntry.PropertyEntities)
            {
                if (!item.Valid())
                {
                    continue;
                }
                NodeEntity column = new NodeEntity() { Comment = item.Comment };
                column.Add(new AttributeEntity("Column", item.Column));
                if (item.Flag != ColumnFlag.ForeignKey)
                {
                    column.Add(new AttributeEntity("IsNull", item.IsNull.ToString()));
                    column.Add(new AttributeEntity("Length", item.Length.ToString()));
                }
                column.Add(new AttributeEntity("Comment", item.Comment));
                column.Add(new AttributeEntity("Default", item.Default));
                column.Add(new AttributeEntity("Check", item.Check));
                classElement.Add(column);
                if (item.Flag == ColumnFlag.Column || item.Flag == ColumnFlag.PrimaryKey)
                {
                    if (item.Flag == ColumnFlag.PrimaryKey)
                    {
                        column.Name = "Id";
                    }
                    else
                    {
                        column.Name = "Column";
                    }
                    column.Add(new AttributeEntity("PropertyName", item.PropertyName));
                }
                else if (item.Flag == ColumnFlag.ForeignKey)
                {
                    column.Name = "ForeignKey";
                    FKColumnEntity foreignKeyColumn = item.FKColumnEntity;
                    column.Add(new AttributeEntity("ReferenceTable", foreignKeyColumn.ReferenceTable));
                    column.Add(new AttributeEntity("ReferenceId", foreignKeyColumn.ReferenceId));
                    column.Add(new AttributeEntity("ReferenceType", foreignKeyColumn.ReferenceType.FullName + "," + foreignKeyColumn.ReferenceType.Namespace));
                    column.Add(new AttributeEntity("Constraint", foreignKeyColumn.Constraint));

            
                    if (foreignKeyColumn.Single != null)
                    {
                        NodeEntity column1 = new NodeEntity();
                        column.Add(column1);
                        column1.Name = "OneToOne";
                        column1.Add(new AttributeEntity("PropertyName", foreignKeyColumn.Single.PropertyName));
                    }
                    if (foreignKeyColumn.Many != null)
                    {
                        NodeEntity column1 = new NodeEntity();
                        column.Add(column1);
                        column1.Name = "OneToMany";
                        column1.Add(new AttributeEntity("PropertyName", foreignKeyColumn.Many.PropertyName));
                    }
                    if (foreignKeyColumn.Basic != null)
                    {
                        NodeEntity column1 = new NodeEntity();
                        column.Add(column1);
                        column1.Name = "Basic";
                        column1.Add(new AttributeEntity("PropertyName", foreignKeyColumn.Basic.PropertyName));
                    }
               
                }
            }
         
        }

        /// <summary>
        /// 将实体信息映射为表结构信息存储为xml 
        /// </summary>
        /// <param name="tableEntry">映射信息</param>
        public virtual string ToXml(ClassEntity tableEntry, bool xmlLinq = true)
        {
            XmlHelper xmlHelper = new XmlHelper { IsXmlDocment = !xmlLinq };

            ElementEntity element = new ElementEntity("Mapping") { };

            NodeEntity tableNode = new NodeEntity() { Name = "Table" };
            element.Add(tableNode);
            ToCreatrTableXml(tableNode, tableEntry);

            xmlHelper.Element = element;
            ToXml(xmlHelper, tableEntry);
            return xmlHelper.ToXml();
        }

        /// <summary>
        /// 将实体信息映射为表结构信息存储为xml 
        /// </summary>
        /// <param name="tableEntry">映射信息</param>
        public virtual string ToXml(ClassEntity[] tableEntries, bool xmlLinq = true)
        {
            XmlHelper xmlHelper = new XmlHelper { IsXmlDocment = !xmlLinq };
            ElementEntity element = new ElementEntity("Mapping") { };

            NodeEntity tableNode = new NodeEntity() { Name = "Table" };
            element.Add(tableNode);
            ToCreatrTableXml(tableNode, tableEntries);

            xmlHelper.Element = element;
            foreach (ClassEntity tableEntry in tableEntries)
            {
                ToXml(xmlHelper, tableEntry);
            }
            return xmlHelper.ToXml();
        }
#endregion 实现映射表基于xml(Xml.Linq.XDocment 或 XmlDocment)实现
    }


   
}
#endif