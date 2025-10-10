using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace peliculas.Models
{
    public class Pelicula
    {
        public Int16 Id { get; set; }
        public string Titulo { get; set; }
        public string Sipnosis { get; set; }
        public DateTime FechaEstreno { get; set; }
        public Int16 Duracion { get; set; } // Duracion en minutos
        public Int16 GeneroId { get; set; }
        public Int16 DirectorId { get; set; }
        public List<Actor> ?Actores { get; set; }
        public byte[] ?Imagen { get; set; }
        [NotMapped]
        [DisplayName("Cargar Imagen")]
        public IFormFile ?ImagenFile { get; set; }
        [NotMapped]
        public string ?Genero { get; set; }
        [NotMapped]
        public string ?Director { get; set; }
    }
}
