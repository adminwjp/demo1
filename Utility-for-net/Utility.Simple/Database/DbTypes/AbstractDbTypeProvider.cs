using System;
using Utility.Helpers;

namespace Utility.Database.DbTypes
{
    public class AbstractDbTypeProvider
    {
        public virtual string Parse(Type type, int length)
        {
            if (TypeHelper.IsDateTime(type))
            {
                return "DATETIME";//yyyyMMdd  hh:mm:ss
            }
            if (TypeHelper.IsBoolean(type))
            {
                return "BIT";
            }
            if (TypeHelper.IsGuid(type))
            {
                return $"VARCHAR({length})";
            }
            if (TypeHelper.IsString(type))
            {
                return $"VARCHAR({length})";
            }
            if (TypeHelper.IsInterger(type))
            {
                return "INT";
            }
            if (TypeHelper.IsChar(type))
            {
                return "CHAR";
            }
            if (TypeHelper.IsDecimal(type))
            {
                return "Decimal";
            }
            return null;
        }

        //各种数据库 类型 转 通用类型
        public virtual DbType Parse(string type, int length)
        {
            if (type.Contains("("))
            {
                type = type.Split("(".ToCharArray())[0];
            }
            switch (type)
            {
                case "datetime":
                case "date":
                    //oracle date
                    //sqlite text
                    return DbType.DateTime;

                case "char": 
                    return DbType.Char;
                case "tinyint":
                // case "int":
                case "boolean":
                    //mysql postgre sqlserver TINYINT
                    //oracle BOOLEAN
                    //sqlite integer
                    return DbType.Bit;
                case "decimal":
                case "double": 
                case "float":
                case "real":
                    //sqlserver decimal float money real smallmoney
                    //mysql decimal float double  real
                    //oracle number
                    return DbType.Number;
                case "int":
                case "integer":
                case "long":
                case "number":
                    //mysql int tryint bigint integer
                    //sqlserver bigint int tryint smallint
                    return DbType.Integer;
                case "varchar":
                case "text":
                default:return DbType.String;
            }
        }
    }

    public class MySqlDbTypeProvider: AbstractDbTypeProvider
    {
        public override string Parse(Type type, int length)
        {
            //if (TypeUtils.IsBoolean(type))
            //{
            //    //return "TINYINT";
            //}
            //if (TypeUtils.IsDecimal(type))
            //{
            //    //decimal float double  real
            //}
            //if (TypeUtils.IsDecimal(type))
            //{
            //    //int tryint bigint integer
            //}
            return base.Parse(type, length);
        }
        public static string IsNow(string val, double version)
        {
            if (val == null)
            {
                return null;
            }

            if (version != -1 && version >= 5.6 && val.ToLower() == "now")
            {
                return "now()";//datetime 不支持 >=5.6支持 
            }
            return string.Empty;
        }
    }

    public class SqliteDbTypeProvider : AbstractDbTypeProvider
    {
        public override string Parse(Type type, int length)
        {
            if (TypeHelper.IsChar(type))
            {
                return "TEXT";
            }
            if (TypeHelper.IsDateTime(type))
            {
                return "TEXT";//yyyyMMdd  hh:mm:ss
            }
            if (TypeHelper.IsBoolean(type))
            {
                return "INTEGER";
            }
            if (TypeHelper.IsDecimal(type))
                return "REAL";
            if (TypeHelper.IsInterger(type))
                return "INTEGER";
            return base.Parse(type, length);
        }
    }

    public class SqlServerDbTypeProvider : AbstractDbTypeProvider
    {
        public override string Parse(Type type, int length)
        {
            //if (TypeUtils.IsDecimal(type))
            //{
            //    //decimal float money real smallmoney
            //}
            //if (TypeUtils.IsInterger(type))
            //{
            //     //bigint int tryint smallint
            //}
            return base.Parse(type, length);
        }
    }
    public class OracleDbTypeProvider : AbstractDbTypeProvider
    {
        public override string Parse(Type type, int length)
        {
            if (TypeHelper.IsDateTime(type))
            {
                return "DATE";//yyyyMMdd  hh:mm:ss
            }
            if (TypeHelper.IsBoolean(type))
            {
                return "BOOLEAN";
            }
            if (TypeHelper.IsDecimal(type))
                return "NUMBER";
            if (TypeHelper.IsInterger(type))
                return "NUMBER";
            return base.Parse(type, length);
        }
    }
    public class PostgreDbTypeProvider : AbstractDbTypeProvider
    {
        public override string Parse(Type type, int length)
        {
            if (TypeHelper.IsChar(type))
            {
                return "VARCHAR(1)";
            }
            if (TypeHelper.IsBoolean(type))
            {
                return "TINYINT";
            }
            return base.Parse(type, length);
        }
    }
 
}

