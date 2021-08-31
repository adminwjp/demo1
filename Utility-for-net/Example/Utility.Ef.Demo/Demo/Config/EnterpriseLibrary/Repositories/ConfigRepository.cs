#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Utility.EnterpriseLibrary.Repositories;
using Utility.Domain.Repositories;
using Config.Domain.Entities;

namespace Config.EnterpriseLibrary.Repositories
{
    /// <summary>服务信息 企业库 数据访问层接口 实现  </summary>
    public class ConfigRepository : BaseEnterpriseLibraryRepository<ConfigEntity, string>, IRepository<ConfigEntity, string>
    {
        Microsoft.Practices.EnterpriseLibrary.Data.Database database;

        const string Name="mysql";

        /// <summary>
        /// 
        /// </summary>
        public ConfigEnterpriseLibraryDAL()
        {
            this.ConnectionStringName = Name;
            //EntLibContrib.Data.MySql.Configuration.MySqlDatabaseData
            database = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="obj"></param>
        protected override void AddOrUpdate(DbCommand command, ConfigEntity obj)
        {
            if (string.IsNullOrEmpty(obj.Id))
            {
                obj.Id = Guid.NewGuid().ToString("N");
                string sql = "INSERT INTO Config(Id,Name,Ip,Port,AddressTempalte,User,Pwd,Flag,Status,Description,CreateDate) VALUES(@Id,@Name,@Ip,@Port,@AddressTempalte,@User,@Pwd,@Flag,@Status,@Description,@CreateDate);";
                command.CommandText = sql;
                //这种方式代码量多 
                Set(command, obj, true);
            }
            else
            {
                string sql = "UPDATE  Service SET Name=@Name,Ip=@Ip,Port=@Port,AddressTempalte=@AddressTempalte,,User=@User,Pwd=@Pwd,Flag=@Flag,Status=@Status,Description=@Description,LastDate=@LastDate  WHERE Id=@Id;";
                command.CommandText = sql;
                //这种方式代码量多 
                Set(command, obj, false);
            }
            base.AddOrUpdate(command, obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetTable()
        {
            return ConfigEntity.TableName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string QuerySelectColumn()
        {
            return "Id,Name,Ip,Port,AddressTempalte,User,Pwd,Flag,Status,Description,CreateDate,LastDate";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override List<ConfigEntity> Reader(IDataReader reader)
        {
            List<ConfigEntity> datas = new List<ConfigEntity>();
            while (reader.Read())
            {
                ConfigEntity it = new ConfigEntity();
                it.Id = reader.GetString(0);
                it.Name = reader.GetString(1);
                it.Ip = reader.GetString(2);
                it.Port = reader.GetInt32(3);
                it.AddressTemplate = reader.GetString(4);
                it.User = reader.GetValue(5) is DBNull ? null : reader.GetString(5);
                it.Pwd = reader.GetValue(6) is DBNull ? null : reader.GetString(6);
                it.Flag = reader.GetValue(7) is DBNull ? string.Empty : reader.GetString(7);
                it.Status = reader.GetValue(8) is DBNull ? 0 : (Config.Model.ConfigStatus)reader.GetInt32(8);
                it.Description = reader.GetValue(9) is DBNull ? null : reader.GetString(9);
                it.CreateDate = reader.GetValue(10) is DBNull ? (System.DateTime?)null : reader.GetDateTime(10);
                it.LastDate = reader.GetValue(11) is DBNull ? (System.DateTime?)null : reader.GetDateTime(11);
                datas.Add(it);//yield return it;
            }
            reader.Dispose();
            reader.Close();
            return datas;
        }
        private void Set(DbCommand command, ConfigEntity obj, bool insert)
        {
            DbParameter  parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.ParameterName = "@Id";
            parameter.Value = obj.Id;

            parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.ParameterName = "@Name";
            parameter.Value = obj.Name;

            parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.ParameterName = "@Ip";
            parameter.Value = obj.Ip;

            parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.ParameterName = "@Port";
            parameter.Value = obj.Port;

            parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.ParameterName = "@AddressTemplate";
            parameter.Value = obj.AddressTemplate;

            parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.ParameterName = "@User";
            parameter.Value = obj.User;


            parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.ParameterName = "@Pwd";
            parameter.Value = obj.Pwd;

            parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.ParameterName = "@Flag";
            parameter.Value = obj.Flag;

                        parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.ParameterName = "@Status";
            parameter.Value = obj.Status;

            parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            if (insert)
            {
                parameter.ParameterName = "@CreateDate";
                parameter.Value = obj.CreateDate;
            }
            else
            {
                parameter.ParameterName = "@LastDate";
                parameter.Value = obj.LastDate;
            } 
        }

        

        /// <summary>查询wehere sql </summary>
        /// <param name="obj">服务信息</param>
        /// <param name="command"></param>
        /// <returns></returns>
        protected override  void GetWhere(ConfigEntity obj,DbCommand command)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(obj.Id))
            {
                where += " OR Id= @Id   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@Id";
                parameter.Value = obj.Id;
            }
            if (!string.IsNullOrEmpty(obj.Name))
            {
                where += " OR Name= @Name   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@Name";
                parameter.Value = obj.Name;
            }
            if (!string.IsNullOrEmpty(obj.AddressTemplate))
            {
                where += " OR Address= @AddressTemplate   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@Address";
                parameter.Value = obj.AddressTemplate;
            }
            if (!string.IsNullOrEmpty(obj.User))
            {
                where += " OR User= @User   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@User";
                parameter.Value = obj.User;
            }
            if (!string.IsNullOrEmpty(obj.Pwd))
            {
                where += " OR Pwd= @Pwd   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@Pwd";
                parameter.Value = obj.Pwd;
            }
            if (!string.IsNullOrEmpty(obj.Flag))
            {
                where += " OR Flag= @Flag   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@Flag";
                parameter.Value = obj.Flag;
            }
            if (obj.CreateDate.HasValue)
            {
                where += " OR CreateDate= @CreateDate   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@CreateDate";
                parameter.Value = obj.CreateDate;
            }
            if (obj.LastDate.HasValue)
            {
                where += " OR LastDate= @LastDate   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@LastDate";
                parameter.Value = obj.LastDate;
            }
            where = where.Length > 0 ? $" WHERE {where.Substring(4)}"  : where;
            command.CommandText= where;
        }
       
    }
}
#endif