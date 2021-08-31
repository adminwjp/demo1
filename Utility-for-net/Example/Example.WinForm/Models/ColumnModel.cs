namespace Template.Api.Models
{
   // using Abp.Domain.Entities;
   // using Abp.Domain.Entities.Auditing;
    using System;
    using System.Collections.Generic;

    //列信息
    public class ColumnModel//:IEntity<long?>
    {
        public virtual bool IsSelected { get; set; }
        public virtual long? Id{get;set;}
        public virtual String CloumName{get;set;}
        public virtual String PropertyName{get;set;}

        public virtual String Remark{get;set;}

        public virtual  String CsharepPropertyType{get;set;}
        public virtual long Length { get; set; }

         public virtual String Title{get;set;} 
        public virtual long? TableId{get;set;}
        public virtual TableModel Table{get;set;}
        public virtual bool IsTransient(){
            return true;
        }
    }
}