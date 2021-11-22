using System.Collections.Generic;
using Persistencia.Entidades;
using Logica.VO;

namespace Logica.Mapper
{
    public class BandaMapper
    {
        public static Banda VOBandaToBanda(VOBanda banda, GeneroMusical genero)
        {
            return new Banda(
                banda.Id,
                banda.Nombre,
                banda.AnioCreacion,
                banda.AnioSeparacion,
                genero
                );
        }

        public static VOBanda BandaToVOBanda(Banda banda, List<VOIntegrante> integrantes)
        {
            return new VOBanda(
                banda.Id,
                banda.Nombre,
                banda.AnioCreacion,
                banda.AnioSeparacion,
                banda.GeneroMusical.Nombre,
                integrantes, 
                null
                );
        }
    }
}
