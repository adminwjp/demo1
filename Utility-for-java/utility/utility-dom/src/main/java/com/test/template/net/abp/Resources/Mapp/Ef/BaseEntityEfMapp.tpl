{#referencrNamespace}
using System.Data.Entity;
using {#programName}.Domain.Entities;

namespace {#programName}.EntityFramework.EntityMappings
{
    /// <summary>BaseEntity 基类 ef 实体映射 abp 未包装 自己去实现 </summary>
    public class BaseEntityEfMapp : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<T> where T:BaseEntity
    {
        public  {#classMap}(string tableName)
        {
            ToTable(tableName);
            HasKey(x => x.Id);// 主键
            Property(x => x.Id).HasMaxLength(20)
            Property(x => x.CreationTime).IsRequired();//创建时间
            Property(x => x.LastModificationTime).IsRequired();//更新时间
            Property(x => x.IsDeleted).IsRequired();//防止删除，将IsDeleted设置为true，然后更新数据库中的实体。它还不会通过自动过滤从数据库中检索（选择）软删除的实体
            Property(x => x.DeletionTime).IsRequired();//软删除时间

            builder.HasOne(x => x.Parent).WithMany(it=>it.Children).HasForeignKey("ParentId").OnDelete(DeleteBehavior.Cascade);//父菜单
            builder.HasMany(x => x.Children).WithOne(it=>it.Parent).HasForeignKey("ParentId").OnDelete(DeleteBehavior.Cascade);//菜单子集
        }
        protected abstract void Set();
    }
}
