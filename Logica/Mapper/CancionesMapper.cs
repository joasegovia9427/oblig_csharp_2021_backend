using Persistencia.Entidades;
using Logica.VO;

namespace Logica.Mapper
{
    public class CancionesMapper
    {

        public static VOCancion CancionToVOCancion(Cancion c)
        {
            return  new VOCancion(
                        c.Id,
                        c.Nombre,
                        c.Duracion,
                        c.Anio,
                        c.Dato,
                        c.Integrante.Id,
                        c.Genero.Nombre,
                        null
                    );
        }

        public static Cancion VOCancionToCancion(VOCancion vOCancion, Integrante integrante, GeneroMusical genero)
        {
            return new Cancion(
                vOCancion.Id,
                vOCancion.Nombre,
                vOCancion.Duracion,
                vOCancion.Anio,
                vOCancion.Dato,
                integrante, 
                genero
                );
        }
    }
}
