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

        public DbSet<Actor> Actores { get; set; }
        public DbSet<Director> Directores { get; set; }
        public DbSet<Genero> Generos { get; set; } = default!;
        public DbSet<Pelicula> Peliculas { get; set; } = default!;
    }
}
