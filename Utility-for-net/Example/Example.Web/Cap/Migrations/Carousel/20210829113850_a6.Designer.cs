﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Cap.Api.Infrastracture;

namespace Example.Web.Cap.Migrations.Carousel
{
    [DbContext(typeof(CarouselDbContext))]
    [Migration("20210829113850_a6")]
    partial class a6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Shop.Cap.Api.Models.CarouselModel", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Background")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("background");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("creation_time");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("deletion_time");

                    b.Property<string>("Desc")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT")
                        .HasColumnName("desc");

                    b.Property<bool>("Enable")
                        .HasColumnType("INTEGER")
                        .HasColumnName("enable");

                    b.Property<int>("Flag")
                        .HasColumnType("INTEGER")
                        .HasColumnName("flag");

                    b.Property<string>("ImageId")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("image_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modification_time");

                    b.Property<string>("Link")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("link");

                    b.Property<int>("Orders")
                        .HasColumnType("INTEGER")
                        .HasColumnName("orders");

                    b.Property<string>("Remark")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("remark");

                    b.Property<string>("Src")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("src");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("t_carousel");
                });
#pragma warning restore 612, 618
        }
    }
}
