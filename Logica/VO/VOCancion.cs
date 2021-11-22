namespace Logica.VO
{
    public class VOCancion
    {
        public VOCancion()
        {
        }
        public VOCancion(int id, string nombre, float duracion, int anio, string dato, int integranteId, string genero, string error)
        {
            Id = id;
            Nombre = nombre;
            Duracion = duracion;
            Anio = anio;
            Dato = dato;
            IntegranteId = integranteId;
            Genero = genero;
            Error = error;
        }
        public VOCancion(string nombre, 
            float duracion, 
            int anio, 
            string dato, 
            int integranteId, 
            string genero,
            string error)
        {
            Nombre = nombre;
            Duracion = duracion;
            Anio = anio;
            Dato = dato;
            IntegranteId = integranteId;
            Genero = genero;
            Error = error;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Duracion { get; set; }
        public int Anio { get; set; }
        public string Dato { get; set; }
        public int IntegranteId { get; set; }
        public string Genero { get; set; }
        public string Error { get; set; }

    }
}
