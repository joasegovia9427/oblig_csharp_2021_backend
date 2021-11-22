using Logica.EntryPoint;
using Logica.VO;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MusicServicesREST.Controllers
{
    [RoutePrefix("api/generos")]
    public class GeneroMusicalController : ApiController
    {
        private Facade fecade;
        public GeneroMusicalController()
        {
            fecade = Facade.Instance;
        }

        [HttpPost]
        [Route("alta")]
        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        public bool GeneroMusicalAlta([FromBody]string genero)
        {
            return fecade.GeneroMusicalAlta(genero);
        }

        [HttpPut]
        [Route("modificacion")]
        [EnableCors(origins: "*", headers: "*", methods: "PUT")]
        public bool GeneroMusicalModificacion([FromBody]VOGeneroMusical genero)
        {
            return fecade.GeneroMusicalModificacion(genero.Id, genero.genero);
        }

        [HttpDelete]
        [Route("baja/{Id}")]
        [EnableCors(origins: "*", headers: "*", methods: "DELETE")]
        public bool GeneroMusicalBaja(int Id)
        {
            return fecade.GeneroMusicalBaja(Id);
        }

        [HttpGet]
        [Route("listado")]
        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        public List<VOGeneroMusical> GeneroMusicalListado()
        {
            return fecade.GeneroMusicalListado();
        }


    }
}
