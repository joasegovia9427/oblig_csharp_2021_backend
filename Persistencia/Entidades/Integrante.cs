using System;

namespace Persistencia.Entidades
{
    public class Integrante
    {
        public Integrante() { }
        public Integrante(int id, DateTime fechaNacimiento, string foto, Persona persona, Banda banda)
        {
            Id = id;
            FechaNacimiento = fechaNacimiento;
            Foto = foto;
            Persona = persona;
            Banda = banda;
        }

        public Integrante(DateTime fechaNacimiento, string foto, Persona persona, Banda banda)
        {
            FechaNacimiento = fechaNacimiento;
            Foto = foto;
            Persona = persona;
            Banda = banda;
        }

        public int Id { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
        public Persona Persona { get; set; }
        public Banda Banda { get; set; }

    }
}
