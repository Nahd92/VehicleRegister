using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegister.Domain.Models;

namespace EntityFramework.Data.Data
{
    public class VehicleRegisterContext : DbContext
    {
        public VehicleRegisterContext(DbContextOptions<VehicleRegisterContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<AutoMotiveRepair> AutoMotive { get; set; }
        public DbSet<ServiceReservations> ServiceReservations { get; set; }
        public DbSet<VehicleServiceHistory> VehicleServiceHistory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
            modelBuilder.Entity<AutoMotiveRepair>().ToTable("AutoMotiveRepair");
            modelBuilder.Entity<ServiceReservations>().ToTable("ServiceReservations");
            modelBuilder.Entity<VehicleServiceHistory>().ToTable("VehicleServiceHistory");
        }

    }
}
