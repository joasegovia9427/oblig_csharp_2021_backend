using Logica.EntryPoint;
using Logica.VO;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MusicServicesREST.Controllers
{
    [RoutePrefix("api/bandas")]
    public class BandasController : ApiController
    {
        private Facade fecade;
        public BandasController()
        {
            fecade = Facade.Instance;
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("obtener/{Id}")]
        public VOBanda BandaObtener(int Id)
        {
            return fecade.obtenerBanda(Id);
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("listado")]
        public List<VOBanda> BandaListado()
        {
            return fecade.obtenerBandas();
        }

        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        [HttpPost]
        [Route("alta")]
        public bool BandaAlta(VOBanda banda)
        {
            return fecade.altaDeBanda(banda);
        }

        [EnableCors(origins: "*", headers: "*", methods: "DELETE")]
        [HttpDelete]
        [Route("baja/{Id}")]
        public bool BandaBaja(int Id)
        {
            return fecade.bajaDeBanda(Id);
        }

        [EnableCors(origins: "*", headers: "*", methods: "PUT")]
        [HttpPut]
        [Route("modificacion")]
        public bool BandaModificar(VOBanda banda)
        {
            return fecade.modificarBanda(banda);
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("obtenerIntegrantes/{Id}")]
        public List<VOIntegrante> BandaIntegranteListado(int Id)
        {
            return fecade.BandaIntegranteListado(Id);
        }

        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        [HttpPost]
        [Route("altaIntegrante")]
        public bool BandaIntegranteAlta([FromUri] int BandaId, [FromUri] int IntegranteId)
        {
            return fecade.BandaIntegranteAlta(BandaId, IntegranteId);
        }

        [EnableCors(origins: "*", headers: "*", methods: "PUT")]
        [HttpPut]
        [Route("bajaIntegrante/{IntegranteId}")]
        public bool BandaIntegranteBaja(int IntegranteId)
        {
            return fecade.BandaIntegranteBaja(IntegranteId);
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("albumesListado/{Id}")]
        public List<VOAlbum> BandaAlbumListado(int Id)
        {
            return fecade.BandaAlbumListado(Id);
        }

    }
}
