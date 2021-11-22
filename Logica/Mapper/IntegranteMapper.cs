using Persistencia.Entidades;
using Logica.VO;

namespace Logica.Mapper
{
    public class IntegranteMapper
    {
        public static Integrante VOIntegranteToIntegrante(VOIntegrante integrante, Persona persona, Banda banda)
        {
            if(integrante.Id == 0)
            {
                return new Integrante(integrante.FechaNacimiento, integrante.Foto, persona, banda);
            }
            return new Integrante(integrante.Id, integrante.FechaNacimiento, integrante.Foto, persona, banda);
        }

        public static VOIntegrante IntegranteToVOIntegrante(Integrante integrante)
        {
            return new VOIntegrante(
                integrante.Id,
                integrante.FechaNacimiento,
                integrante.Foto,
                new VOPersona(integrante.Persona.Id, integrante.Persona.Nombre, integrante.Persona.Apellido, null),
                integrante.Banda == null ? 0 : integrante.Banda.Id,
                null);
        }
    }
}
