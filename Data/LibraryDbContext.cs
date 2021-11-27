using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajaYaAPI.Data.Entities;

namespace TrabajaYaAPI.Data
{
    public class LibraryDbContext : IdentityDbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PublishEntity> Publish { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            :base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<UserEntity>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserEntity>().HasMany(u => u.Publications).WithOne(p => p.User);

            modelBuilder.Entity<PublishEntity>().ToTable("Publications");
            modelBuilder.Entity<PublishEntity>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PublishEntity>().HasOne(p => p.User).WithMany(u => u.Publications);

        }
    }
}
