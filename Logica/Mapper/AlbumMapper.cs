using Persistencia.Entidades;
using Logica.VO;
using System.Collections.Generic;

namespace Logica.Mapper
{
    public class AlbumMapper
    {
        public static VOAlbum AlbumToVOAlbum(Album album, VOBanda banda, List<VOCancion> canciones)
        {
            return new VOAlbum(
                album.Id,
                album.Nombre,
                album.AnioCreacion,
                album.GeneroMusical.Nombre,
                banda, 
                canciones,
                null
                );
        }
    }
}
