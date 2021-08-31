{#referencrNamespace}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using {#programName}.Domain.Entities;

namespace {#programName}.EntityFrameworkCore.EntityMappings
{
    /// <summary>BaseEntity 基类 ef core 实体映射 abp 未包装 自己去实现 </summary>
    public class BaseEntityEfMapp : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<T> where T:BaseEntity
    {

        public void Configure(EntityTypeBuilder<{#CalssEntityName}> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Id);// 主键
            builder.Property(x => x.Id).HasMaxLength(20);
            this.Set(builder);
            builder.Property(x => x.CreationTime).IsRequired();//创建时间
            builder.Property(x => x.LastModificationTime).IsRequired();//更新时间
            builder.Property(x => x.IsDeleted).IsRequired();//防止删除，将IsDeleted设置为true，然后更新数据库中的实体。它还不会通过自动过滤从数据库中检索（选择）软删除的实体
            builder.Property(x => x.DeletionTime).IsRequired();//软删除时间

            builder.HasOne(x => x.Parent).WithMany(it=>it.Children).HasForeignKey("ParentId").OnDelete(DeleteBehavior.Cascade);//父菜单
            builder.HasMany(x => x.Children).WithOne(it=>it.Parent).HasForeignKey("ParentId").OnDelete(DeleteBehavior.Cascade);//菜单子集
        }
        protected string TableName { get; set; }
        protected abstract void Set(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder);
    }
}
