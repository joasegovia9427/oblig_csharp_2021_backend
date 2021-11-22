namespace Logica.VO
{
    public class VOUsuario
    {
        public VOUsuario()
        {
        }
        public VOUsuario(int id, string nombreUsuario, string contrasenia, VOPersona persona, string error)
        {
            Id = id;
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
            Persona = persona;
            Error = error;
        }
        public VOUsuario(string nombreUsuario, string contrasenia, VOPersona persona, string error)
        {
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
            Persona = persona;
            Error = error;
        }
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public VOPersona Persona { get; set; }
        public string Error { get; set; }

    }
}
