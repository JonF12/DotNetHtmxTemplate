using DotNetHtmxTypescriptTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetHtmxTypescriptTemplate.Repository.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MovieModel> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=../DotNetHtmxTypescriptTemplate.Repository/Data/App.db");
        }
    }
}
