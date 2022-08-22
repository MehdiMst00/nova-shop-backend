﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NovaShop.Infrastructure.Data;

#nullable disable

namespace NovaShop.Infrastructure.Migrations
{
    [DbContext(typeof(NovaShopDbContext))]
    [Migration("20220822001649_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NovaShop.ApplicationCore.CatalogAggregate.CatalogBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("CatalogBrands");
                });

            modelBuilder.Entity("NovaShop.ApplicationCore.CatalogAggregate.CatalogGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("int");

                    b.Property<int>("DisplayPriority")
                        .HasColumnType("int");

                    b.Property<string>("PictureFileName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PictureUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogItemId");

                    b.ToTable("CatalogGalleries");
                });

            modelBuilder.Entity("NovaShop.ApplicationCore.CatalogAggregate.CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CatalogBrandId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PictureFileName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("CatalogBrandId");

                    b.ToTable("CatalogItems");
                });

            modelBuilder.Entity("NovaShop.ApplicationCore.CatalogAggregate.CatalogGallery", b =>
                {
                    b.HasOne("NovaShop.ApplicationCore.CatalogAggregate.CatalogItem", "CatalogItem")
                        .WithMany("Galleries")
                        .HasForeignKey("CatalogItemId")
                        .IsRequired()
                        .HasConstraintName("FK_CatalogGalleries_CatalogItems");

                    b.Navigation("CatalogItem");
                });

            modelBuilder.Entity("NovaShop.ApplicationCore.CatalogAggregate.CatalogItem", b =>
                {
                    b.HasOne("NovaShop.ApplicationCore.CatalogAggregate.CatalogBrand", "CatalogBrand")
                        .WithMany("CatalogItems")
                        .HasForeignKey("CatalogBrandId")
                        .IsRequired()
                        .HasConstraintName("FK_CatalogItems_CatalogBrands");

                    b.Navigation("CatalogBrand");
                });

            modelBuilder.Entity("NovaShop.ApplicationCore.CatalogAggregate.CatalogBrand", b =>
                {
                    b.Navigation("CatalogItems");
                });

            modelBuilder.Entity("NovaShop.ApplicationCore.CatalogAggregate.CatalogItem", b =>
                {
                    b.Navigation("Galleries");
                });
#pragma warning restore 612, 618
        }
    }
}
