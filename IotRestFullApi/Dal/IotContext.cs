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
            modelBuilder.Entity<Action>()
                .HasOne(b => b.Device)
                .WithMany(a => a.Actions);
            modelBuilder.Entity<Command>()
                .HasOne(b => b.Device)
                .WithMany(a => a.Commands);
            modelBuilder.Entity<Stats>()
                .HasOne(b => b.Device)
                .WithMany(a => a.Stats);
        }
    }
}
