using peliculas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace LibreriaWeb.AppDbContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });             
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);            
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Administrador" },
                new Role { Id = 2, Name = "Editor", Description = " Editor" },
                new Role { Id = 3, Name = "User", Description = "User" }
            );
        }

        public DbSet<Actor> Actores { get; set; }
        public DbSet<Director> Directores { get; set; }
        public DbSet<Genero> Generos { get; set; } = default!;
        public DbSet<Pelicula> Peliculas { get; set; } = default!;
        public DbSet<Usuario> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<SigningKey> SigningKeys { get; set; }

    }
}
