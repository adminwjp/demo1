using Company.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.Ef.EntityMappings
{
    public  class BaseMap<T> :Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<T>
   where T: BaseEntity
    { 
        public BaseMap(string tableName)
        {
            this.TableName=tableName;
        }
       
        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder)
        { 
            builder.ToTable(this.TableName);
            //builder.HasKey(it => it.Id).HasName("id");//.HasAnnotation("MySql: ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            //builder.HasKey(it => it.Id);
            //builder.Property(it => it.Id).HasColumnName("id").HasMaxLength(36).IsRequired(false);//.ValueGeneratedOnAdd();   
            this.Set(builder);

        }

        protected virtual void Set(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder){
            builder.HasKey(it => it.Id).HasName("id");//.HasAnnotation("MySql: ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
           builder.Property(x => x.Lanage).HasColumnName("lanage").HasMaxLength(50);
           builder.Property(x => x.CreateDate).HasColumnName("create_date");
           builder.Property(x => x.ModifyDate).HasColumnName("modify_date");
           builder.Property(x => x.Enable).HasColumnName("enable");
           builder.Ignore(x => x.DomainEvents);

        }
        protected string TableName { get; set; }

      
    }
}