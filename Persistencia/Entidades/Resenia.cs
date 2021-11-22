namespace Persistencia.Entidades
{
    public class Resenia
    {
        public Resenia()
        {
        }

        public Resenia(int puntaje, string resenia, int bandaId, int cancionId, int usuarioId)
        {
            Puntaje = puntaje;
            this.resenia = resenia;
            BandaId = bandaId;
            CancionId = cancionId;
            UsuarioId = usuarioId;
        }

        public Resenia(int id, int puntaje, string resenia, int bandaId, int cancionId, int usuarioId)
        {
            Id = id;
            Puntaje = puntaje;
            this.resenia = resenia;
            BandaId = bandaId;
            CancionId = cancionId;
            UsuarioId = usuarioId;
        }

        public int Id { get; set; }
        public int Puntaje { get; set; }
        public string resenia { get; set; }
        public int BandaId { get; set; }
        public int CancionId { get; set; }
        public int UsuarioId { get; set; }

    }
}
