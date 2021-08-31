namespace Template.Api.Models
{
    //using Abp.Domain.Entities;
    //using Abp.Domain.Entities.Auditing;
    using System;
    using System.Collections.Generic;
    //数据库信息
    public class DatabaseModel//:IEntity<long?>
    {
        public virtual bool IsSelected { get; set; }
        public virtual long? Id{get;set;}
        public virtual String Name{get;set;}
        public virtual String ProgramName{get;set;}
        public virtual String Remark{get;set;}

        public virtual ISet<TableModel> TableModels{get;set;}

        public virtual bool IsTransient(){
            return true;
        }

    }

    
}