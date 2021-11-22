using System;

namespace Persistencia.Entidades
{
    public class Persona
    {
        public Persona()
        {
        }

        public Persona(int id, string nombre, string apellido)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
        }

        public Persona(string nombre, string apellido)
        {
            Nombre = nombre;
            Apellido = apellido;
        }
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
    }
}
