using Microsoft.EntityFrameworkCore;
using NutriTEc_Backend.Models;

namespace NutriTEc_Backend.Repository
{
    public class NutriTEcDataContext : DbContext
    {
        public DbSet<Vitamin> Vitamins { get; set; }

        public NutriTEcDataContext(DbContextOptions<NutriTEcDataContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

    }
}
