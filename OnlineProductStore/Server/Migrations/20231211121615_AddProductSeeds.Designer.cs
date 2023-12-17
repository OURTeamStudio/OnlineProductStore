﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineProductStore.Server.Data;

#nullable disable

namespace OnlineProductStore.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231211121615_AddProductSeeds")]
    partial class AddProductSeeds
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineProductStore.Shared.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Дуже свіжі",
                            ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj",
                            Price = 20m,
                            Title = "Помидори"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Смачно капець",
                            ImageUrl = "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg",
                            Price = 60m,
                            Title = "Огірочічки"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Дуже свіжі",
                            ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj",
                            Price = 20m,
                            Title = "Помидори"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Смачно капець",
                            ImageUrl = "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg",
                            Price = 60m,
                            Title = "Огірочічки"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Дуже свіжі",
                            ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj",
                            Price = 20m,
                            Title = "Помидори"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Смачно капець",
                            ImageUrl = "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg",
                            Price = 60m,
                            Title = "Огірочічки"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}