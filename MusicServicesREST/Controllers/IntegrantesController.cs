using Logica.EntryPoint;
using Logica.VO;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MusicServicesREST.Controllers
{
    [RoutePrefix("api/integrantes")]
    public class IntegrantesController : ApiController
    {
        private Facade fecade;
        public IntegrantesController()
        {
            fecade = Facade.Instance;
        }

        [HttpGet]
        [EnableCors(origins:"*", headers:"*", methods:"GET")]
        [Route("listado")]
        public List<VOIntegrante> IntegrantesListado()
        {
            return fecade.IntegrantesListado();
        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        [Route("alta")]
        public bool IntegranteAlta([FromBody] VOIntegrante integrante)
        {
            return fecade.IntegranteAlta(integrante);
        }

        [HttpDelete]
        [EnableCors(origins: "*", headers: "*", methods: "DELETE")]
        [Route("baja/{Id}")]
        public bool IntegranteBaja(int Id)
        {
            return fecade.IntegranteBaja(Id);
        }

        [HttpPut]
        [Route("modificacion")]
        [EnableCors(origins: "*", headers: "*", methods: "PUT")]
        public bool IntegranteModificacion([FromBody] VOIntegrante integrante)
        {
            return fecade.IntegranteModificacion(integrante);
        }

        [HttpGet]
        [Route("obtener/{Id}")]
        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        public VOIntegrante IntegranteObtener(int Id)
        {
            return fecade.IntegranteObtener(Id);
        }
    }
}
