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
    public class ServiceRepository : BaseEnterpriseLibraryRepository<ServiceEntity, string>, IRepository<ServiceEntity, string>
    {

        const string Name="mysql";

        /// <summary>
        /// 
        /// </summary>
        public ServiceRepository()
        {

            this.ConnectionStringName = Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="obj"></param>
        protected override int AddOrUpdate(DbCommand command, ServiceEntity obj)
        {
            if (string.IsNullOrEmpty(obj.Id))
            {
                obj.Id = Guid.NewGuid().ToString("N");
                string sql = "INSERT INTO Service(Id,Name,Ip,Port,Status,CreateDate) VALUES(@Id,@Name,@Ip,@Port,@Status,@CreateDate);";
                command.CommandText = sql;
                //这种方式代码量多 
                Set(command, obj, true);
            }
            else
            {
                string sql = "UPDATE  Service SET Name=@Name,Ip=@Ip,Port=@Port,Status=@Status,LastDate=@LastDate  WHERE Id=@Id;";
                command.CommandText = sql;
                //这种方式代码量多 
                Set(command, obj, false);
            }
          return  base.AddOrUpdate(command, obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetTable()
        {
            return ServiceEntity.TableName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string QuerySelectColumn()
        {
            return "Id,Name,Ip,Port,Status,CreateDate,LastDate";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected override List<ServiceEntity> Reader(IDataReader reader)
        {
            List<ServiceEntity> datas = new List<ServiceEntity>();
            while (reader.Read())
            {
                ServiceEntity it = new ServiceEntity();
                it.Id = reader.GetString(0);
                it.Name = reader.GetString(1);
                it.Ip = reader.GetString(2);
                it.Port = reader.GetValue(3) is DBNull ? 0 : reader.GetInt32(3);
                it.Status = reader.GetValue(4) is DBNull ? 0 : (ServiceStatus)reader.GetInt32(4);
                it.CreateDate = reader.GetValue(5) is DBNull ? (System.DateTime?)null : reader.GetDateTime(5);
                it.LastDate = reader.GetValue(6) is DBNull ? (System.DateTime?)null : reader.GetDateTime(6);
                datas.Add(it);//yield return it;
            }
            reader.Dispose();
            reader.Close();
            return datas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="obj"></param>
        /// <param name="insert"></param>
        private void Set(DbCommand command, ServiceEntity obj, bool insert)
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
        protected override  void GetWhere(ServiceEntity obj,DbCommand command)
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
            if (!string.IsNullOrEmpty(obj.Ip))
            {
                where += " OR Ip= @Ip   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@Ip";
                parameter.Value = obj.Ip;
            }
            if (obj.Port>0)
            {
                where += " OR Port= @Port   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@Port";
                parameter.Value = obj.Port;
            }
            if (obj.Status>0)
            {
                where += " OR Status= @Status   ";
                DbParameter parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
                parameter.ParameterName = "@Status";
                parameter.Value = obj.Status;
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