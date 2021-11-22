using Persistencia.Enums;

namespace Persistencia.Entidades
{
    public class Resultado
    {
        public Resultado()
        {
        }
        public Resultado(bool esExitoso, string entidad, TipoError error)
        {
            this.esExitoso = esExitoso;
            this.entidad = entidad;
            this.error = error;
        }

        public bool esExitoso { get; set; }
        public string entidad { get; set; }
        public TipoError error { get; set; }
    }
}
