using Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.Ef.EntityMappings
{
    public  class CatagoryMap : BaseMap<CompanyCatagoryEntity> 
    { 
      
        public CatagoryMap():base(CompanyCatagoryEntity.Table)
        {
          
        }
  

        protected override void Set(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CompanyCatagoryEntity> builder){
            base.Set(builder);
            builder.Property(x => x.ButtonHref1).HasColumnName("button_href1").HasMaxLength(50);
            builder.Property(x => x.ButtonName1).HasColumnName("button_name1").HasMaxLength(50);
            builder.Property(x => x.ButtonHref2).HasColumnName("button_href2").HasMaxLength(50);
            builder.Property(x => x.ButtonName2).HasColumnName("button_name2").HasMaxLength(50);
            builder.Property(x => x.BackgroundImage).HasColumnName("background_image").HasMaxLength(50);

            builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(500);
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50);
            builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(50);
            builder.Property(x => x.Flag).HasColumnName("flag");
            builder.Property(x => x.Icon).HasColumnName("icon");
            builder.Property(x => x.Body).HasColumnName("body").HasMaxLength(50);
           builder.Property(x => x.Review).HasColumnName("review");
           builder.Property(x => x.Color).HasColumnName("color").HasMaxLength(50);
           builder.Property(x => x.Process).HasColumnName("process").HasMaxLength(50);
           builder.Property(x => x.Style).HasColumnName("style").HasMaxLength(50);
           builder.Property(x => x.BackgroundImage).HasColumnName("background_image").HasMaxLength(50);
           builder.Property(x => x.Href).HasColumnName("href").HasMaxLength(50);
           builder.Property(x => x.Feature).HasColumnName("feature").HasMaxLength(50);
           builder.Property(x => x.Filter).HasColumnName("filter").HasMaxLength(50);
            builder.HasOne(x => x.Parent).WithMany(it => it.Children).HasForeignKey("parent_id");
            builder.HasMany(x => x.Children).WithOne(it => it.Parent).HasForeignKey("parent_id");
            builder.Property(x => x.parent_id).HasColumnName("parent_id").HasMaxLength(50);
           builder.Property(x => x.Tel).HasColumnName("tel").HasMaxLength(50);
           builder.Property(x => x.Logo).HasColumnName("logo").HasMaxLength(50);
           builder.Property(x => x.Logo1).HasColumnName("logo1").HasMaxLength(50);

        }

      
    }
}