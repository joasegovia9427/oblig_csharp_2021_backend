namespace Persistencia.Entidades
{
    public class AlbumCancion
    {
        public AlbumCancion()
        {
        }

        public AlbumCancion(int albumID, int cancionID)
        {
            AlbumId = albumID;
            CancionId = cancionID;
        }

        public int AlbumId { get; set; }
        public int CancionId { get; set; }

    }
}
