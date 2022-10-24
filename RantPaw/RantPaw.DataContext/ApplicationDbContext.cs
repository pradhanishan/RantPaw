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

            // User

            builder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            // Reaction

            builder.Entity<Reaction>().ToTable("Reactions", schema: "ref");

            builder.Entity<Reaction>().HasData(
                new Reaction { Id = 1, Name = "like" },
                new Reaction { Id = 2, Name = "dislike" }
                );

            builder.Entity<Reaction>().HasIndex(r => r.Name).IsUnique();


            // PostReaction

            builder.Entity<PostReaction>().HasIndex(pr => new { pr.ReactionId, pr.UserId, pr.PostId }).IsUnique();

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Reaction> Reactions { get; set; }

        public DbSet<PostReaction> PostReactions { get; set; }

    }
}
