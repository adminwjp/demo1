using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace {#programName}
{
    public class BaseEntity:Entity<string>,IHasCreationTime,IHasModificationTime,IHasDeletionTime
    {
		public virtual DateTime CreationTime { get; set; }
		public virtual DateTime? LastModificationTime { get; set; }
		public virtual DateTime? DeletionTime { get; set; }
		public virtual bool IsDeleted { get; set; }
	}
}
