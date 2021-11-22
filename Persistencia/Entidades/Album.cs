namespace Persistencia.Entidades
{
    public class Album
    {
        public Album()
        {
        }

        public Album(int id, string nombre, int anioCreacion, GeneroMusical generoMusical, Banda banda)
        {
            Id = id;
            Nombre = nombre;
            AnioCreacion = anioCreacion;
            GeneroMusical = generoMusical;
            Banda = banda;
        }

        public Album(string nombre, int anioCreacion, GeneroMusical generoMusical, Banda banda)
        {
            Nombre = nombre;
            AnioCreacion = anioCreacion;
            GeneroMusical = generoMusical;
            Banda = banda;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int AnioCreacion { get; set; }
        public GeneroMusical GeneroMusical { get; set; }
        public Banda Banda { get; set; }
    }
}
