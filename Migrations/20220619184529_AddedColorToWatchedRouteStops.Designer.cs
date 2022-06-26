﻿// <auto-generated />
using System;
using BusBoiBackend;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusBoiBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220619184529_AddedColorToWatchedRouteStops")]
    partial class AddedColorToWatchedRouteStops
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusBoiBackend.Models.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgencyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LongName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RouteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("BusBoiBackend.Models.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StopId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("BusBoiBackend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusBoiBackend.Models.WatchedRouteStop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RouteId")
                        .HasColumnType("int");

                    b.Property<int?>("StopId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("StopId");

                    b.HasIndex("UserId");

                    b.ToTable("WatchedRouteStops");
                });

            modelBuilder.Entity("RouteStop", b =>
                {
                    b.Property<int>("RoutesId")
                        .HasColumnType("int");

                    b.Property<int>("StopsId")
                        .HasColumnType("int");

                    b.HasKey("RoutesId", "StopsId");

                    b.HasIndex("StopsId");

                    b.ToTable("RouteStop");
                });

            modelBuilder.Entity("BusBoiBackend.Models.WatchedRouteStop", b =>
                {
                    b.HasOne("BusBoiBackend.Models.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId");

                    b.HasOne("BusBoiBackend.Models.Stop", "Stop")
                        .WithMany()
                        .HasForeignKey("StopId");

                    b.HasOne("BusBoiBackend.Models.User", "User")
                        .WithMany("WatchedRouteStops")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("Stop");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RouteStop", b =>
                {
                    b.HasOne("BusBoiBackend.Models.Route", null)
                        .WithMany()
                        .HasForeignKey("RoutesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusBoiBackend.Models.Stop", null)
                        .WithMany()
                        .HasForeignKey("StopsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusBoiBackend.Models.User", b =>
                {
                    b.Navigation("WatchedRouteStops");
                });
#pragma warning restore 612, 618
        }
    }
}
