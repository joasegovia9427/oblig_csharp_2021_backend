using Persistencia.Entidades;
using Persistencia.Enums;

namespace Persistencia.Utils
{
    public class ResultadoErroneo
    {

        public static Resultado ErrorEnConexion()
        {
            return new Resultado(
                false,
                Constantes.DATABASE_CONNECTION,
                TipoError.NO_CONEXION
                );
        }
    }
}
