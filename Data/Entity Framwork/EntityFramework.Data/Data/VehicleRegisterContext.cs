using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleRegister.Domain.Models;


namespace EntityFramework.Data.Data
{
    public class VehicleRegisterContext : IdentityDbContext
    {

        public VehicleRegisterContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<AutoMotiveRepair> AutoMotive { get; set; }
        public DbSet<ServiceReservations> ServiceReservations { get; set; }
        public DbSet<VehicleServiceHistory> VehicleServiceHistory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
