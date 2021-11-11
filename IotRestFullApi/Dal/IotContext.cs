using IotRestFullApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace IotRestFullApi.Dal
{
    public class IotContext : DbContext
    {
        public IotContext(DbContextOptions<IotContext> options) : base(options)
        {
        }
        public DbSet<Command> Command { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Action> Action { get; set; }
        public DbSet<Stats> Stats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasMany(_ => _.Actions)
                .WithOne(_ => _.Device);
            modelBuilder.Entity<Device>()
                .HasMany(_ => _.Commands)
                .WithOne(_ => _.Device);
            modelBuilder.Entity<Device>()
                .HasMany(_ => _.Stats)
                .WithOne(_ => _.Device);
        }
    }
}
