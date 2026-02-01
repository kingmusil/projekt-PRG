using Microsoft.EntityFrameworkCore;
using MusilZavěrečnýProjekt.Models;

namespace MusilZavěrečnýProjekt
{
    public class RezervaceDBContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<rezervace> reservations { get; set; }
        public DbSet<Seat> seats { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=4c1_musiljakub_db2;uid=musiljakub;password=123456");
        }
    }
}
