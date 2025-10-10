namespace peliculas.Models
{
    public class Actor
    {
        public Int16 Id { get; set; }
        public string Nombre { get; set; }
        public string Biografia { get; set; }
        public DateOnly FechaNacimiento { get; set; }
    }
}
