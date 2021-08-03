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
        public DbSet<Stats> Stats { get; set; }
    }
}
