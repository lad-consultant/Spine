﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheSpine.Infrastructure.DataAccess;

#nullable disable

namespace TheSpine.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230601214909_OrderingIndexAdded")]
    partial class OrderingIndexAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TheSpine.Core.ItemDetailedInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HtmlContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SegmentItemId")
                        .HasColumnType("int");

                    b.Property<string>("TextContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ItemDetailedInfos");
                });

            modelBuilder.Entity("TheSpine.Core.Node", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderingIndex")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Nodes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderingIndex = 0,
                            Title = "Pursue"
                        },
                        new
                        {
                            Id = 2,
                            OrderingIndex = 1,
                            Title = "Secure"
                        },
                        new
                        {
                            Id = 3,
                            OrderingIndex = 2,
                            Title = "Visualize"
                        },
                        new
                        {
                            Id = 4,
                            OrderingIndex = 3,
                            Title = "Exchange"
                        },
                        new
                        {
                            Id = 5,
                            OrderingIndex = 4,
                            Title = "Resolve"
                        },
                        new
                        {
                            Id = 6,
                            OrderingIndex = 5,
                            Title = "Administer"
                        },
                        new
                        {
                            Id = 7,
                            OrderingIndex = 6,
                            Title = "Market"
                        },
                        new
                        {
                            Id = 8,
                            OrderingIndex = 7,
                            Title = "Manage"
                        },
                        new
                        {
                            Id = 9,
                            OrderingIndex = 8,
                            Title = "Design"
                        },
                        new
                        {
                            Id = 11,
                            OrderingIndex = 0,
                            ParentId = 9,
                            Title = "Capture"
                        },
                        new
                        {
                            Id = 12,
                            OrderingIndex = 1,
                            ParentId = 9,
                            Title = "Create"
                        },
                        new
                        {
                            Id = 13,
                            OrderingIndex = 2,
                            ParentId = 9,
                            Title = "Plan"
                        },
                        new
                        {
                            Id = 14,
                            OrderingIndex = 3,
                            ParentId = 9,
                            Title = "Visualize"
                        },
                        new
                        {
                            Id = 15,
                            OrderingIndex = 4,
                            ParentId = 9,
                            Title = "Compute"
                        },
                        new
                        {
                            Id = 16,
                            OrderingIndex = 5,
                            ParentId = 9,
                            Title = "Simulate"
                        },
                        new
                        {
                            Id = 17,
                            OrderingIndex = 6,
                            ParentId = 9,
                            Title = "Analyze"
                        },
                        new
                        {
                            Id = 18,
                            OrderingIndex = 7,
                            ParentId = 9,
                            Title = "Illustrate"
                        },
                        new
                        {
                            Id = 19,
                            OrderingIndex = 8,
                            ParentId = 9,
                            Title = "Delineate"
                        },
                        new
                        {
                            Id = 20,
                            OrderingIndex = 9,
                            ParentId = 9,
                            Title = "Narrate"
                        },
                        new
                        {
                            Id = 21,
                            OrderingIndex = 10,
                            ParentId = 9,
                            Title = "Specify"
                        });
                });

            modelBuilder.Entity("TheSpine.Core.Segment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Segments");

                    b.HasData(
                        new
                        {
                            Id = 31,
                            Title = "Rendering/Animation"
                        },
                        new
                        {
                            Id = 32,
                            Title = "Reports/Postproduction"
                        });
                });

            modelBuilder.Entity("TheSpine.Core.SegmentItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Differentiator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EaseOfUse")
                        .HasColumnType("int");

                    b.Property<string>("Licensing")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NodeId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("SegmentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SegmentItems");
                });
#pragma warning restore 612, 618
        }
    }
}
