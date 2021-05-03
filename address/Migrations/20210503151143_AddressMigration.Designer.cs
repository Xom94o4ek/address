﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using address.Data;

namespace address.Migrations
{
    [DbContext(typeof(AddressSystemContext))]
    [Migration("20210503151143_AddressMigration")]
    partial class AddressMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("address.Models.Areas", b =>
                {
                    b.Property<int>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AreaName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<int?>("RegionsRegionId")
                        .HasColumnType("int");

                    b.HasKey("AreaId");

                    b.HasIndex("RegionsRegionId");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("address.Models.Districts", b =>
                {
                    b.Property<int>("DistrictId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DistrictName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LocalitiesLocalityId")
                        .HasColumnType("int");

                    b.Property<int>("LocalityId")
                        .HasColumnType("int");

                    b.HasKey("DistrictId");

                    b.HasIndex("LocalitiesLocalityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("address.Models.Houses", b =>
                {
                    b.Property<int>("HouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Entrances")
                        .HasColumnType("int");

                    b.Property<int>("Flats")
                        .HasColumnType("int");

                    b.Property<int>("Floors")
                        .HasColumnType("int");

                    b.Property<string>("HouseNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<int>("StreetId")
                        .HasColumnType("int");

                    b.Property<int?>("StreetsStreetId")
                        .HasColumnType("int");

                    b.HasKey("HouseId");

                    b.HasIndex("StreetsStreetId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("address.Models.Localities", b =>
                {
                    b.Property<int>("LocalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("AreasAreaId")
                        .HasColumnType("int");

                    b.Property<string>("LocalityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocalityId");

                    b.HasIndex("AreasAreaId");

                    b.ToTable("Localities");
                });

            modelBuilder.Entity("address.Models.Regions", b =>
                {
                    b.Property<int>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RegionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegionId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("address.Models.Streets", b =>
                {
                    b.Property<int>("StreetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<int?>("DistrictsDistrictId")
                        .HasColumnType("int");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StreetId");

                    b.HasIndex("DistrictsDistrictId");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("address.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Group")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("address.Models.Areas", b =>
                {
                    b.HasOne("address.Models.Regions", "Regions")
                        .WithMany()
                        .HasForeignKey("RegionsRegionId");

                    b.Navigation("Regions");
                });

            modelBuilder.Entity("address.Models.Districts", b =>
                {
                    b.HasOne("address.Models.Localities", "Localities")
                        .WithMany()
                        .HasForeignKey("LocalitiesLocalityId");

                    b.Navigation("Localities");
                });

            modelBuilder.Entity("address.Models.Houses", b =>
                {
                    b.HasOne("address.Models.Streets", "Streets")
                        .WithMany()
                        .HasForeignKey("StreetsStreetId");

                    b.Navigation("Streets");
                });

            modelBuilder.Entity("address.Models.Localities", b =>
                {
                    b.HasOne("address.Models.Areas", "Areas")
                        .WithMany()
                        .HasForeignKey("AreasAreaId");

                    b.Navigation("Areas");
                });

            modelBuilder.Entity("address.Models.Streets", b =>
                {
                    b.HasOne("address.Models.Districts", "Districts")
                        .WithMany()
                        .HasForeignKey("DistrictsDistrictId");

                    b.Navigation("Districts");
                });
#pragma warning restore 612, 618
        }
    }
}