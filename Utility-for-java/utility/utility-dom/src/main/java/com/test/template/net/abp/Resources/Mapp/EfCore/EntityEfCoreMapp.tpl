{#referencrNamespace}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using {#programName}.Domain.Entities;

namespace {#programName}.EntityFrameworkCore.EntityMappings
{
    /// <summary>{#comment}实体映射 abp 未包装 自己去实现 </summary>
    public class {#classMap} : BaseEntityEfMapp<{#CalssEntityName}>
    {

        public {#classMap}()
        {
           this.TableName={#tableName};
        }
        protected override void Set(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<{#CalssEntityName}> builder)
        {
            {#SetStringCode}
		}
    }
}
