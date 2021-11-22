namespace Logica.VO
{
    public class VOGeneroMusical
    {
        public VOGeneroMusical() { }
            
        public VOGeneroMusical(int id, string genero, string error)
        {
            Id = id;
            this.genero = genero;
            Error = error;

        }

        public int Id { get; set; }
        public string genero { get; set; }
        public string Error { get; set; }

    }

}
