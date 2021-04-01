﻿// <auto-generated />
using System;
using EntityFramework.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFramework.Data.Migrations
{
    [DbContext(typeof(VehicleRegisterContext))]
    [Migration("20210401183112_InitialCommit")]
    partial class InitialCommit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VehicleRegister.Domain.Models.AutoMotiveRepair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("AutoMotiveRepair");
                });

            modelBuilder.Entity("VehicleRegister.Domain.Models.ServiceReservations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AutoMotiveRepairId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ServiceReservations");
                });

            modelBuilder.Entity("VehicleRegister.Domain.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DrivingBan")
                        .HasColumnType("bit");

                    b.Property<DateTime>("InTraffic")
                        .HasColumnType("datetime2");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegisterNumber")
                        .HasColumnType("int");

                    b.Property<bool>("ServiceBooked")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ServiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.Property<int>("YearlyFee")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vehicle");
                });
#pragma warning restore 612, 618
        }
    }
}
