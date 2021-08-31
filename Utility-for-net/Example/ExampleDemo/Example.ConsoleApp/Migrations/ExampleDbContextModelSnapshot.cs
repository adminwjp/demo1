﻿// <auto-generated />
using Example;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Example.Migrations
{
    [DbContext(typeof(ExampleDbContext))]
    partial class ExampleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Example.TypeInfo", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Des")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Example.UserInfo", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("LoginDate")
                        .HasColumnType("bigint");

                    b.Property<long>("LoginFailCount")
                        .HasColumnType("bigint");

                    b.Property<long>("LoginIp")
                        .HasColumnType("bigint");

                    b.Property<long>("ModifyDate")
                        .HasColumnType("bigint");

                    b.Property<string>("Pwd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RegDate")
                        .HasColumnType("bigint");

                    b.Property<long>("RegIp")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("TimeSpan")
                        .IsConcurrencyToken()
                        .HasColumnType("bigint");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Example.UserLogInfo", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long>("CreateDate")
                        .HasColumnType("bigint");

                    b.Property<long>("Flag")
                        .HasColumnType("bigint");

                    b.Property<long>("LoginDate")
                        .HasColumnType("bigint");

                    b.Property<long>("LoginIp")
                        .HasColumnType("bigint");

                    b.Property<long>("ModifyDate")
                        .HasColumnType("bigint");

                    b.Property<long>("NewPwd")
                        .HasColumnType("bigint");

                    b.Property<long>("OldPwd")
                        .HasColumnType("bigint");

                    b.Property<long>("RegIp")
                        .HasColumnType("bigint");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogs");
                });

            modelBuilder.Entity("Example.UserLogInfo", b =>
                {
                    b.HasOne("Example.UserInfo", "User")
                        .WithMany("UserLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
