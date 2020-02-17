using System;
using Microsoft.EntityFrameworkCore;

namespace TechhuddleWarehouse.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductRecord> Products { get; set; }
        public DbSet<CapacityRecord> CapacityRecords { get; set; }
        public DbSet<WarehouseEntry> WarehouseEntries { get; set; }
    }
}
