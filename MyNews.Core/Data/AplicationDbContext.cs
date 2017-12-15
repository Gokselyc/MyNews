using Microsoft.EntityFrameworkCore;
using MyNews.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyNews.Core.Data
{
    public class AplicationDbContext: DbContext
    {

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options)
        {

        }

        public DbSet<News>News { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; } 

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasAlternateKey(c => c.Email)
                .HasName("AlternateKey_UserEmail");
        }



    }
}
