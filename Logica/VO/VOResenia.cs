namespace Logica.VO
{
    public class VOResenia
    {
        public VOResenia()
        {
        }

        public VOResenia(int puntaje, string resenia, int bandaId, int cancionId, int usuarioId, string error)
        {
            Puntaje = puntaje;
            Resenia = resenia;
            BandaId = bandaId;
            CancionId = cancionId;
            UsuarioId = usuarioId;
            Error = error;
        }

        public VOResenia(int id, int puntaje, string resenia, int bandaId, int cancionId, int usuarioId, string error)
        {
            Id = id;
            Puntaje = puntaje;
            Resenia = resenia;
            BandaId = bandaId;
            CancionId = cancionId;
            UsuarioId = usuarioId;
            Error = error;
        }

        public int Id { get; set; }
        public int Puntaje { get; set; }
        public string Resenia { get; set; }
        public int BandaId { get; set; }
        public int CancionId { get; set; }
        public int UsuarioId { get; set; }
        public string Error { get; set; }

    }
}
