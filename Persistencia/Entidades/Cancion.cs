namespace Persistencia.Entidades
{
    public class Cancion
    {
        public Cancion()
        {
        }
        public Cancion(int id, string nombre, float duracion, int anio, string dato, Integrante integrante, GeneroMusical generoMusical)
        {
            Id = id;
            Nombre = nombre;
            Duracion = duracion;
            Anio = anio;
            Dato = dato;
            Integrante = integrante;
            Genero = generoMusical;
        }
        public Cancion(Cancion other)
        {
            Id = other.Id;
            Nombre = other.Nombre;
            Duracion = other.Duracion;
            Anio = other.Anio;
            Dato = other.Dato;
            Integrante = other.Integrante;
            Genero = other.Genero;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Duracion { get; set; }
        public int Anio { get; set; }
        public string Dato { get; set; }
        public Integrante Integrante { get; set; }
        public GeneroMusical Genero { get; set; }
    }
}
