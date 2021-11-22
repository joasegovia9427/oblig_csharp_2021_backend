namespace Persistencia.Entidades
{
    public class Banda
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int AnioCreacion { get; set; }
        public int AnioSeparacion { get; set; }
        public GeneroMusical GeneroMusical { get; set; }
        public Banda()
        {
        }

        public Banda(int id, string nombre, int anioCreacion, int anioSeparacion, GeneroMusical generoMusical)
        {
            Id = id;
            Nombre = nombre;
            AnioCreacion = anioCreacion;
            AnioSeparacion = anioSeparacion;
            GeneroMusical = generoMusical;
        }
    }
}
