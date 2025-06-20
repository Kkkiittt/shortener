﻿// <auto-generated />
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

using PlanManager.Infrastructure.Contexts;

#nullable disable

namespace PlanManager.DataAccess.Migrations
{
	[DbContext(typeof(PlanDbContext))]
    [Migration("20250602105136_BasePlan")]
    partial class BasePlan
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PlanManager.Domain.Entities.Plan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.PrimitiveCollection<int[]>("Actions")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxLinkCount")
                        .HasColumnType("integer");

                    b.Property<int>("MaxLinkLifetime")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Plans");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Actions = new[] { 1, 3, 4 },
                            Created = new DateTime(2022, 5, 12, 7, 12, 12, 123, DateTimeKind.Utc),
                            Description = "Free tier",
                            MaxLinkCount = 3,
                            MaxLinkLifetime = 7,
                            Name = "Free",
                            Updated = new DateTime(2022, 5, 12, 7, 12, 12, 123, DateTimeKind.Utc)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
