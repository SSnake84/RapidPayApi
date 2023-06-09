﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RapidPayApi.Data.Models;

#nullable disable

namespace RapidPayApi.Data.Migrations
{
    [DbContext(typeof(RapidPayContext))]
    [Migration("20230417152010_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("RapidPayApi.Data.Models.CreditCard", b =>
                {
                    b.Property<string>("CardNumber")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("TEXT")
                        .IsFixedLength();

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

                    b.HasKey("CardNumber");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("RapidPayApi.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
