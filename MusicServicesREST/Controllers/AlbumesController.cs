using Logica.EntryPoint;
using Logica.VO;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MusicServicesREST.Controllers
{
    [RoutePrefix("api/albumes")]
    public class AlbumesController : ApiController
    {
        private Facade fecade;
        public AlbumesController()
        {
            fecade = Facade.Instance;
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("obtener/{Id}")]
        public VOAlbum AlbumObtener(int Id)
        {
            return fecade.AlbumObtener(Id);
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("listado")]
        public List<VOAlbum> AlbumListado()
        {
            return fecade.AlbumListado();
        }

        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        [HttpPost]
        [Route("alta")]
        public bool AlbumAlta(VOAlbum album)
        {
            return fecade.AlbumAlta(album);
        }

        [EnableCors(origins: "*", headers: "*", methods: "DELETE")]
        [HttpDelete]
        [Route("baja/{Id}")]
        public bool AlbumBaja(int Id)
        {
            return fecade.AlbumBaja(Id);
        }

        [EnableCors(origins: "*", headers: "*", methods: "PUT")]
        [HttpPut]
        [Route("modificacion")]
        public bool AlbumModificar(VOAlbum album)
        {
            return fecade.AlbumModificacion(album);
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("cancionesListado/{Id}")]
        public List<VOCancion> AlbumCancionListado(int Id)
        {
            return fecade.AlbumCancionListado(Id);
        }

        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        [HttpPost]
        [Route("altaCancionAlbum")]
        public bool AlbumCancionAlta([FromUri] int AlbumId, [FromUri] int CancionId)
        {
            return fecade.AlbumCancionAlta(AlbumId, CancionId);
        }

        [EnableCors(origins: "*", headers: "*", methods: "DELETE")]
        [HttpDelete]
        [Route("bajaCancionAlbum")]
        public bool AlbumCancionBaja([FromUri] int AlbumId, [FromUri] int CancionId)
        {
            return fecade.AlbumCancionBaja(AlbumId, CancionId);
        }

    }
}
