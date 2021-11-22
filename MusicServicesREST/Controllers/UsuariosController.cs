using Logica.EntryPoint;
using Logica.VO;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MusicServicesREST.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : ApiController
    {
        private Facade fecade;
        public UsuariosController()
        {
            fecade = Facade.Instance;
        }

        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        [HttpPost]
        [Route("login")]
        public bool Login([FromUri] string username, [FromUri] string password)
        {
            return fecade.Login(username, password);
        }

        [EnableCors(origins: "*", headers: "*", methods: "POST")]
        [HttpPost]
        [Route("alta")]
        public bool UsuarioAlta(VOUsuario usuario)
        {
            return fecade.UsuarioAlta(usuario);
        }

        [EnableCors(origins: "*", headers: "*", methods: "DELETE")]
        [HttpDelete]
        [Route("baja/{username}")]
        public bool UsuarioBaja(string username)
        {
            return fecade.UsuarioBaja(username);
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("obtener/{Id}")]
        public VOUsuario UsuarioObtener(int Id)
        {
            return fecade.UsuarioObtener(Id);
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("listado")]
        public List<VOUsuario> UsuariosListado()
        {
            return fecade.UsuariosListado();
        }

        [EnableCors(origins: "*", headers: "*", methods: "GET")]
        [HttpGet]
        [Route("identificador/{username}")]
        public int UsuarioIdentificador(string username)
        {
            return fecade.UsuarioIdentifier(username);
        }
    }
}
