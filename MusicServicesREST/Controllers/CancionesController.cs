using Logica.EntryPoint;
using Logica.VO;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;


namespace MusicServicesREST.Controllers
{
    [RoutePrefix("api/canciones")]
    public class CancionesController : ApiController
    {
        private Facade fecade;
        public CancionesController()
        {
            fecade = Facade.Instance;
        }

        [HttpGet]
        [Route("listado")]
        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        public List<VOCancion> CancionListado()
        {
            return fecade.CancionListado();
        }

        [HttpPost]
        [Route("alta")]
        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        public bool CancionAlta(VOCancion cancion)
        {
            return fecade.CancionAlta(cancion);
        }

        [HttpDelete]
        [Route("baja/{Id}")]
        [EnableCors(origins: "*", headers: "*", methods: "DELETE")]
        public bool CancionBaja(int Id)
        {
            return fecade.CancionBaja(Id);
        }

        [HttpPut]
        [Route("modificacion")]
        [EnableCors(origins: "*", headers: "*", methods: "PUT")]
        public bool CancionModificacion(VOCancion cancion)
        {
            return fecade.CancionModificacion(cancion);
        }

        [HttpGet]
        [Route("obtener/{Id}")]
        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        public VOCancion CancionObtener(int Id)
        {
            return fecade.CancionObtener(Id);
        }
    }
}
