using Persistencia.Entidades;
using Logica.VO;

namespace Logica.Mapper
{
    public class ReseniaMapper
    {

        public static Resenia VOReseniaToResenia(VOResenia vOResenia)
        {
            return new Resenia(
                vOResenia.Id,
                vOResenia.Puntaje,
                vOResenia.Resenia,
                vOResenia.BandaId,
                vOResenia.CancionId,
                vOResenia.UsuarioId
                );
        }

        public static VOResenia ReseniaToVOResenia(Resenia resenia)
        {
            return new VOResenia(
                resenia.Id,
                resenia.Puntaje,
                resenia.resenia,
                resenia.BandaId,
                resenia.CancionId,
                resenia.UsuarioId,
                null
                );
        }
    }
}
