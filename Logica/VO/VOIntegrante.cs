using System;
namespace Logica.VO
{
    public class VOIntegrante
    {
        public VOIntegrante()
        {
        }

        public VOIntegrante(int id, DateTime fechaNacimiento, string foto, VOPersona persona, int bandaId, string error)
        {
            Id = id;
            FechaNacimiento = fechaNacimiento;
            Foto = foto;
            Persona = persona;
            BandaId = bandaId;
            Error = error;
        }
        public VOIntegrante(DateTime fechaNacimiento, string foto, VOPersona persona, int bandaId, string error)
        {
            FechaNacimiento = fechaNacimiento;
            Foto = foto;
            Persona = persona;
            BandaId = bandaId;
            Error = error;
        }
        public int Id{ get; set; }
        public DateTime FechaNacimiento { get; set; }
        public String Foto { get; set; }
        public VOPersona Persona { get; set; }
        public int BandaId { get; set; }
        public string Error { get; set; }

    }
}
