using System.Collections.Generic;

namespace Logica.VO
{
    public class VOAlbum
    {
        public VOAlbum()
        {
        }
        public VOAlbum(int id, string nombre, int anioCreacion, string genero, VOBanda banda, List<VOCancion> canciones, string error)
        {
            Id = id;
            Nombre = nombre;
            AnioCreacion = anioCreacion;
            Genero = genero;
            Banda = banda;
            Canciones = canciones;
            Error = error;
        }

        public VOAlbum(string nombre, int anioCreacion, string genero, VOBanda banda, List<VOCancion> canciones, string error)
        {
            Nombre = nombre;
            AnioCreacion = anioCreacion;
            Genero = genero;
            Banda = banda;
            Canciones = canciones;
            Error = Error;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int AnioCreacion { get; set; }
        public string Genero { get; set; }
        public VOBanda Banda { get; set; }
        public List<VOCancion> Canciones { get; set; }
        public string Error { get; set; }

    }
}
