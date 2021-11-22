namespace Persistencia.Entidades
{
    public class GeneroMusical
    {

        public int Id { get ; set ; }
        public string Nombre { get ; set ; }
        public GeneroMusical()
        {
        }
        public GeneroMusical(int generoID, string nombre)
        {
            Id = generoID;
            Nombre = nombre;
        }
    }
}
