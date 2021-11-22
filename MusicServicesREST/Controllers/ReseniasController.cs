using Logica.EntryPoint;
using Logica.VO;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MusicServicesREST.Controllers
{
    [RoutePrefix("api/resenias")]
    public class ReseniasController : ApiController
    {

        private Facade fecade;
        public ReseniasController()
        {
            fecade = Facade.Instance;
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("listado")]
        public List<VOResenia> ReseniaListado()
        {
            return fecade.ReseniaListado();
        }

        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        [HttpPost]
        [Route("alta")]
        public bool ReseniaAlta(VOResenia resenia)
        {
            return fecade.ReseniaAlta(resenia);
        }

        [EnableCors(origins: "*", headers: "*", methods: "DELETE")]
        [HttpDelete]
        [Route("baja/{Id}")]
        public bool ReseniaBaja(int Id)
        {
            return fecade.ReseniaBaja(Id);
        }

        [EnableCors(origins: "*", headers: "*", methods: "PUT")]
        [HttpPut]
        [Route("modificacion")]
        public bool ReseniaModificacion(VOResenia resenia)
        {
            return fecade.ReseniaModificacion(resenia);
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("obtener/{Id}")]
        public VOResenia ReseniaObtener(int Id)
        {
            return fecade.ReseniaObtener(Id);
        }
    }
}
