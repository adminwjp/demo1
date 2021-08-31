using Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.Ef.EntityMappings
{
    public  class RelationMap : BaseMap<RelationEntity> 
    { 

        public RelationMap():base(RelationEntity.Table)
        {
        }
     

        protected override void Set(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RelationEntity> builder){
            base.Set(builder); 
            builder.Property(x => x.Fk1).HasColumnName("fk1");
           builder.Property(x => x.Fk2).HasColumnName("fk2");
           builder.Property(x => x.Flag).HasColumnName("flag").HasMaxLength(50);
           

        }

      
    }
}