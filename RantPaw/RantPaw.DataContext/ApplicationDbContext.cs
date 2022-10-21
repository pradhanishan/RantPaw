using Microsoft.EntityFrameworkCore;
using RantPaw.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.DataContext
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

    }
}
