﻿// <auto-generated />
using System;
using BinanceFeed.Infrastructure.SQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BinanceFeed.Infrastructure.SQL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231130212531_NextMigration")]
    partial class NextMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BinanceFeed.Infrastructure.SQL.Models.Entities.TickerPriceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weighted24Avg")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("TickerPriceEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
