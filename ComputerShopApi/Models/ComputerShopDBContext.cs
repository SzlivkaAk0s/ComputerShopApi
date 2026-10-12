using Microsoft.EntityFrameworkCore;

namespace ComputerShopApi.Models
{
    public class ComputerShopDBContext : DbContext
    {
        public ComputerShopDBContext(DbContextOptions options) : base(options)
        {
        }

        public ComputerShopDBContext()
        {
        }

        public DbSet<Osystem> Osystems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = "server=localhost;database=computershop;uid=root;password=";

            optionsBuilder.UseMySql(conn,ServerVersion.AutoDetect(conn));
        }
    }
}
