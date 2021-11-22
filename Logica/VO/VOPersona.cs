namespace Logica.VO
{
    public class VOPersona
    {
        public VOPersona()
        {
        }
        public VOPersona(string nombre, string apellido, string error)
        {
            Nombre = nombre;
            Apellido = apellido;
            Error = error;
        }

        public VOPersona(int id, string nombre, string apellido, string error)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Error = error;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Error { get; set; }

    }
}
