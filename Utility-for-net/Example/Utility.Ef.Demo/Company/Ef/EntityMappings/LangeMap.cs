using Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.Ef.EntityMappings
{
    public  class LangeMap : BaseMap<LangeEntity> 
    {
        public LangeMap() : base(LangeEntity.Table) { }
  

        protected override void Set(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LangeEntity> builder){
            base.Set(builder);
           builder.Property(x => x.Val1).HasColumnName("val1").HasMaxLength(50);
           builder.Property(x => x.Val2).HasColumnName("val2").HasMaxLength(50);
           builder.Property(x => x.Val3).HasColumnName("val3").HasMaxLength(50);
           builder.Property(x => x.Val4).HasColumnName("val4").HasMaxLength(50);
           builder.Property(x => x.Val5).HasColumnName("val5").HasMaxLength(50);
           builder.Property(x => x.Val6).HasColumnName("val6").HasMaxLength(50);
           builder.Property(x => x.Val7).HasColumnName("val7").HasMaxLength(50);
           builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(50);
           builder.Property(x => x.RelationId).HasColumnName("relation_id");
           builder.Property(x => x.RelationTable).HasColumnName("relation_table").HasMaxLength(50);
           builder.Ignore(x => x.Flag);

        }

      
    }
}