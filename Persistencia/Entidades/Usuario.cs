namespace Persistencia.Entidades
{
    public class Usuario
    {
        public Usuario()
        {
        }

        public Usuario(int id, string nombreUsuario, string contrasenia, Persona persona)
        {
            Id = id;
            NombreUsuario = nombreUsuario;
            this.contrasenia = contrasenia;
            Persona = persona;
        }

        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string contrasenia { get; set; }
        public Persona Persona { get; set; }
    }
}
