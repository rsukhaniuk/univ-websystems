﻿// <auto-generated />
using Lab1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lab1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lab1.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "1 name"
                        },
                        new
                        {
                            Id = 2,
                            Name = "2 name"
                        },
                        new
                        {
                            Id = 3,
                            Name = "3 name"
                        },
                        new
                        {
                            Id = 4,
                            Name = "4 name"
                        },
                        new
                        {
                            Id = 5,
                            Name = "5 name"
                        },
                        new
                        {
                            Id = 6,
                            Name = "6 name"
                        },
                        new
                        {
                            Id = 7,
                            Name = "7 name"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}