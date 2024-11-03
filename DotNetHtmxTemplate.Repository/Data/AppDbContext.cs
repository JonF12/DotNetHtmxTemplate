using DotNetHtmxTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetHtmxTemplate.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MovieModel> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=../DotNetHtmxTemplate.Repository/Data/App.db");
        }
    }
}
