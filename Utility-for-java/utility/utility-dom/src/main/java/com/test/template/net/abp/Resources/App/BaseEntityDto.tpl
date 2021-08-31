using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace {#programName}
{
    public class BaseEntityDto {
        protected virtual void Set<T>(ref T oldValue, T newValue, string propertyName)
        {
            oldValue = newValue;
        }
    }
    public class BaseUpdateEntityDto : Abp.Application.Services.Dto.EntityDto<string>
    {
        private string id;

        public new string Id { get => id; set { Set(ref id, value, "Id"); } }
        protected virtual void Set<T>(ref T oldValue, T newValue, string propertyName)
        {
            oldValue = newValue;
        }
    }

    public class BaseAllEntityDto : BaseUpdateEntityDto, IHasCreationTime, IHasModificationTime, IHasDeletionTime
    {
        private DateTime creationTime;
        private DateTime? lastModificationTime;
        private DateTime? deletionTime;
        private bool isDeleted;

        public DateTime CreationTime { get => creationTime; set { Set(ref creationTime, value, "CreationTime"); } }
        public DateTime? LastModificationTime { get => lastModificationTime; set { Set(ref lastModificationTime, value, "LastModificationTime"); } }
        public DateTime? DeletionTime { get => deletionTime; set { Set(ref deletionTime, value, "DeletionTime"); } }
        public bool IsDeleted { get => isDeleted; set { Set(ref isDeleted, value, "IsDeleted"); } }


    }

}
