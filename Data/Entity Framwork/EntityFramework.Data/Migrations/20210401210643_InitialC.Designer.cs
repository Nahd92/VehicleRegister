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
    [Migration("20210401210643_InitialC")]
    partial class InitialC
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

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganisationNumber")
                        .HasColumnType("int");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int?>("ServiceReservationsId")
                        .HasColumnType("int");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceReservationsId");

                    b.ToTable("AutoMotiveRepair");
                });

            modelBuilder.Entity("VehicleRegister.Domain.Models.ServiceReservations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime>("InTraffic")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDrivingBan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsServiceBooked")
                        .HasColumnType("bit");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegisterNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("ServiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ServiceReservationsId")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.Property<int>("YearlyFee")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceReservationsId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("VehicleRegister.Domain.Models.AutoMotiveRepair", b =>
                {
                    b.HasOne("VehicleRegister.Domain.Models.ServiceReservations", null)
                        .WithMany("AutoMotiveRepair")
                        .HasForeignKey("ServiceReservationsId");
                });

            modelBuilder.Entity("VehicleRegister.Domain.Models.Vehicle", b =>
                {
                    b.HasOne("VehicleRegister.Domain.Models.ServiceReservations", null)
                        .WithMany("Vehicle")
                        .HasForeignKey("ServiceReservationsId");
                });

            modelBuilder.Entity("VehicleRegister.Domain.Models.ServiceReservations", b =>
                {
                    b.Navigation("AutoMotiveRepair");

                    b.Navigation("Vehicle");
                });
#pragma warning restore 612, 618
        }
    }
}
