#if   NET40 ||NET45 || NET451 || NET452 || NET46 ||NET461 || NET462|| NET47 || NET471 || NET472|| NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Utility.Ef.EntityMappings;
using Utility.Demo.Domain.Entities;
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endif
#if   NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif

namespace Utility.Demo.Ef.EntityMappings
{
    /// <summary>菜单实体映射 abp 未包装 自己去实现 </summary>
    public class MenuMap: BaseEfMapp<MenuEntity> 
    {
        public MenuMap() :base(MenuEntity.TabelName)
        {
        }
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
        protected override void Set(EntityTypeBuilder<MenuEntity> builder)
        {
            builder.HasKey(x => x.Id);// 主键
            builder.Property(x => x.Orders).IsRequired();//排序
            builder.Property(x => x.CreationTime).IsRequired();//创建时间
            builder.Property(x => x.LastModificationTime).IsRequired();//更新时间
            builder.Property(x => x.IsDeleted).IsRequired();//删除软删除实体时，ASP.NET Boilerplate会检测到此情况
            builder.Property(x => x.DeletionTime).IsRequired();//软删除时间                    

            builder.HasOne(it => it.Parent).WithMany(it => it.Children).HasForeignKey("parent_id").HasConstraintName("fk_parent_id");//Parent

            builder.HasMany(it => it.Children).WithOne(it => it.Parent).HasForeignKey("parent_id").HasConstraintName("fk_parent_id");//Children

            builder.Property(x => x.Text).HasMaxLength(MenuEntity.MaxNameLength).IsRequired();//名称
            builder.Property(x => x.State).HasMaxLength(6);//state：节点状态，'open' 或 'closed'，默认：'open'。如果为'closed'的时候，将不自动展开该节点。
            builder.Property(x => x.Checked);// checked：表示该节点是否被选中。
            builder.Property(x => x.AttributesJson).HasMaxLength(MenuEntity.MaxDescriptionLength).IsRequired();//attributes: 被添加到节点的自定义属性。
            builder.Property(x => x.IconCls).HasMaxLength(MenuEntity.MaxIconLength).IsRequired();//图标


            builder.Property(x => x.Name).HasMaxLength(MenuEntity.MaxNameLength).IsRequired();//名称
            builder.Property(x => x.Collpse);//是否折叠
            builder.Property(x => x.Groups).HasMaxLength(MenuEntity.MaxNameLength);// 分组名称
            builder.Property(x => x.Description).HasMaxLength(MenuEntity.MaxDescriptionLength).IsRequired();//描述
            builder.Property(x => x.Icon).HasMaxLength(MenuEntity.MaxIconLength).IsRequired();//图标
            builder.Property(x => x.Href).HasMaxLength(50).IsRequired();//地址

            //builder.Property(x => x.Name).HasMaxLength(MenuEntity.MaxNameLength).IsRequired();//名称
            builder.Property(x => x.IdName).HasMaxLength(10);//id选择器名称
            builder.Property(x => x.HuiIcon).HasMaxLength(MenuEntity.MaxIconLength).IsRequired();//图标
                                                                                                 //builder.Property(x => x.Href).HasMaxLength(50).IsRequired();//地址

            //builder.Property(x => x.Name).HasMaxLength(MenuEntity.MaxNameLength).IsRequired();//名称
            builder.Property(x => x.AceIcon).HasMaxLength(MenuEntity.MaxIconLength).IsRequired();//图标
            //builder.Property(x => x.Href).HasMaxLength(50).IsRequired();//地址

        }
#endif

#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
        protected override void Set()
        {
            HasKey(x => x.Id);// 主键
            Property(x => x.Orders).IsRequired();//排序
            Property(x => x.CreationTime).IsRequired();//创建时间
            Property(x => x.LastModificationTime).IsRequired();//更新时间
            Property(x => x.IsDeleted).IsRequired();//ASP.NET Boilerplate开箱即用地实现了软删除模式。删除软删除实体时，ASP.NET Boilerplate会检测到此情况
            Property(x => x.DeletionTime).IsRequired();//软删除时间                                //，防止删除，将IsDeleted设置为true，然后更新数据库中的实体。它还不会通过自动过滤从数据库中检索（选择）软删除的实体

            HasOptional(x => x.Parent).WithMany(it => it.Children).HasForeignKey(it => it.ParentId).WillCascadeOnDelete();//父菜单
            HasMany(x => x.Children).WithOptional(it => it.Parent).HasForeignKey(it => it.ParentId).WillCascadeOnDelete();//菜单子集

            Property(x => x.Text).HasMaxLength(MenuEntity.MaxNameLength).IsRequired();//名称
            Property(x => x.State).HasMaxLength(6);//state：节点状态，'open' 或 'closed'，默认：'open'。如果为'closed'的时候，将不自动展开该节点。
            Property(x => x.Checked);// checked：表示该节点是否被选中。
            Property(x => x.AttributesJson).HasMaxLength(MenuEntity.MaxDescriptionLength).IsRequired();//attributes: 被添加到节点的自定义属性。
            Property(x => x.IconCls).HasMaxLength(MenuEntity.MaxIconLength).IsRequired();//图标

               Property(x => x.Name).HasMaxLength(MenuEntity.MaxNameLength).IsRequired();//名称
            Property(x => x.Collpse);//是否折叠
            Property(x => x.Groups).HasMaxLength(MenuEntity.MaxNameLength);// 分组名称
            Property(x => x.Description).HasMaxLength(MenuEntity.MaxDescriptionLength).IsRequired();//描述
            Property(x => x.Icon).HasMaxLength(MenuEntity.MaxIconLength).IsRequired();//图标
            Property(x => x.Href).HasMaxLength(50).IsRequired();//地址

            //Property(x => x.Name).HasMaxLength(MenuEntity.MaxNameLength).IsRequired();//名称
            Property(x => x.IdName).HasMaxLength(10);//id选择器名称
            Property(x => x.HuiIcon).HasMaxLength(MenuEntity.MaxIconLength).IsRequired();//图标
            //Property(x => x.Href).HasMaxLength(50).IsRequired();//地址

              //Property(x => x.Name).HasMaxLength(MenuEntity.MaxNameLength).IsRequired();//名称
            Property(x => x.IceIcon).HasMaxLength(MenuEntity.MaxIconLength).IsRequired();//图标
            //Property(x => x.Href).HasMaxLength(50).IsRequired();//地址
        }
#endif

    }
}
#endif