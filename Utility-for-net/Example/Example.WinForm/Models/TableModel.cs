namespace Template.Api.Models
{
   // using Abp.Domain.Entities;
   // using Abp.Domain.Entities.Auditing;
    using System;
    using System.Collections.Generic;

    //表信息
    public class TableModel//:IEntity<long?>
    {
        public virtual bool IsSelected { get; set; }
        public virtual long? Id{get;set;}
        public virtual String ClassName{get;set;}
        public virtual String TablemName{get;set;}

        public virtual String Remark{get;set;}

         public virtual String Title{get;set;} 

         public virtual long? DatabaseId{get;set;}
        public virtual DatabaseModel Database{get;set;}

        public virtual ISet<ColumnModel> ColumnModels{get;set;}

        public virtual bool IsTransient(){
            return true;
        }
    }

    //表单 信息
    // public class TableListModel
    // {
        
    // }
}