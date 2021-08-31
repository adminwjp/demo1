{#referencrNamespace}
using System.Data.Entity;

namespace {#programName}.EntityFramework.EntityMappings
{
    /// <summary>{#comment}实体映射 abp 未包装 自己去实现 </summary>
    public class {#classMap} : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<{#CalssEntityName}>
    {
        public  {#classMap}():base({#tableName})
        {
        }
        protected override void Set()
        {
            {#SetStringCode}
		}
    }
}
