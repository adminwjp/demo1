//#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
#if  NET40 ||NET45 || NET451 || NET452 || NET46 ||NET461 || NET462|| NET47 || NET471 || NET472|| NET48 ||  NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#else
using Microsoft.EntityFrameworkCore;
#endif

namespace {{.namespace}}
{


#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
 //ef
    /// <summary>{{.comment}} </summary>
    public  class {{.mappName}}  :
        System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<{{.className}}>
    {
        /// <summary>
        ///{{.comment}}
        /// </summary>
        /// <param name="tableName">表名</param>
        public {{.mappName}}("{{.tableName}}")
        {
            {{ rang .pros}}
            {{ if eq .MappFlag 0}}
            {{else}}

            {{ if eq .MappFlag 1}}
            {{else}}

            {{ if eq .MappFlag 3}}
            {{else}}

            {{ if eq .MappFlag 4}}
            {{else}}


            {{end}}

            {{ if eq .MappFlag 5}}
            {{else}}

            {{ if eq .MappFlag 6}}
            {{end}}

            {{end}}

            {{end}}

            {{end}}

            {{end}}
            //编号
            if (typeof(Key).IsValueType)
            {
                HasKey(it => it.Id);//.HasAnnotation("MySql: ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
                //Property(it => it.Id).ValueGeneratedOnAdd();
            }
            else
            {
                HasKey(it => it.Id);
                {if  eq pro.PropertyName pro.ColumnName}
                Property(it => it.Id).HasMaxLength(36).IsRequired();
                {endif}
            }
            {{ end}}
        }

    }


 #else
//efcore
    /// <summary>{{.comment}}  </summary>
    public  class {{.mappName}} :
        Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<{{.className}}>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<{{.className}}> builder)
        {
            {{ rang .pros}}
            {{ if eq .MappFlag 0}}
            {{else}}

            {{ if eq .MappFlag 1}}
            {{else}}

            {{ if eq .MappFlag 3}}
            {{else}}

            {{ if eq .MappFlag 4}}
            {{else}}


            {{end}}

            {{ if eq .MappFlag 5}}
            {{else}}

            {{ if eq .MappFlag 6}}
            {{end}}

            {{end}}

            {{end}}

            {{end}}

            {{end}}

            builder.ToTable("{{.tableName}}");
            //编号
            if (typeof(Key).IsValueType)
            {
                builder.HasKey(it => it.Id);//.HasAnnotation("MySql: ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            }
            else
            {
                builder.HasKey(it => it.Id);
                builder.Property(it => it.Id).HasMaxLength(36).IsRequired(false);//.ValueGeneratedOnAdd();
            }
            this.Set(builder);
        }


   }

#endif
#endif

using Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shop.EntityFrameworkCore.EntityMappings
{
    public  class ProductCatagoryMap :Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ProductCatagory>
    {
        public ProductCatagoryMap(string tableName)
        {
            this.TableName=tableName;
        }
        public ProductCatagoryMap()
        {
            this.TableName="t_product_catagory";
        }
        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductCatagory> builder)
        {
            builder.ToTable(this.TableName);
            //builder.HasKey(it => it.Id).HasName("id");//.HasAnnotation("MySql: ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            //builder.HasKey(it => it.Id);
            //builder.Property(it => it.Id).HasColumnName("id").HasMaxLength(36).IsRequired(false);//.ValueGeneratedOnAdd();
            this.Set(builder);

        }

        protected virtual void Set(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductCatagory> builder){
            builder.HasKey(it => it.Id).HasName("id");//.HasAnnotation("MySql: ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50);
            builder.Property(x => x.Picture).HasColumnName("picture").HasMaxLength(50);
            builder.Property(x => x.Code).HasColumnName("code").HasMaxLength(50);
            builder.Property(x => x.Href).HasColumnName("href").HasMaxLength(50);
            builder.Property(x => x.Orders).HasColumnName("orders").HasMaxLength(50);
            builder.Property(x => x.parent_id).HasColumnName("parent_id");

            builder.HasOne(x => x.Parent).WithMany(it=>it.Children).HasForeignKey("parent_id");
            builder.HasMany(x => x.Children).WithOne(it=>it.Parent).HasForeignKey("parent_id");

            builder.Property(x => x.CreationTime).HasColumnName("create_date");
            builder.Property(x => x.LastModificationTime).HasColumnName("update_date");
            builder.Property(x => x.DeletionTime).HasColumnName("delete_time");
            builder.Property(x => x.IsDeleted).HasColumnName("is_delete");
        }
        protected string TableName { get; set; }


    }
 }