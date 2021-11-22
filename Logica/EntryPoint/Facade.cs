using System.Data.SqlClient;
using System.Collections.Generic;
using Logica.VO;
using Logica.Servicios;
using Logica.Enums;

namespace Logica.EntryPoint
{
    public sealed class Facade
    {
        private static Facade instance = null;

        private BandasService bandasService;
        private GeneroService generoService;
        private IntegrantesService integrantesService;
        private CancionesService cancionService;
        private AlbumesService albumesService;
        private UsersService usersService;
        private ReseniasService reseniasService;
        private SqlConnection connection;
        private Facade()
        {
            bandasService = new BandasService();
            generoService = new GeneroService();
            integrantesService = new IntegrantesService();
            cancionService = new CancionesService();
            albumesService = new AlbumesService();
            usersService = new UsersService();
            reseniasService = new ReseniasService();
            connection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=musicmanagements;Integrated Security=True");
        }

        public static Facade Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Facade();
                }
                return instance;
            }
        }

        //SERVICIOS DE GENERO MUSICAL
        public bool GeneroMusicalAlta(string genero)
        {
            return this.generoService.GeneroMusicalAlta(genero, connection);
        }

        public bool GeneroMusicalModificacion(int Id, string genero)
        {
            return this.generoService.GeneroMusicalModificacion(Id, genero, connection);
        }

        public bool GeneroMusicalBaja(int Id)
        {
            return this.generoService.GeneroMusicalBaja(Id, connection);
        }

        public List<VOGeneroMusical> GeneroMusicalListado()
        {
            return this.generoService.GeneroMusicalListado(connection);
        }

        //SERVICIOS DE INTEGRANTE
        public List<VOIntegrante> IntegrantesListado()
        {
            return this.integrantesService.IntegrantesListado(connection);
        }

        public bool IntegranteAlta(VOIntegrante integrante)
        {
            return this.integrantesService.IntegranteAlta(integrante, connection);
        }

        public bool IntegranteBaja(int Id)
        {
            return this.integrantesService.IntegranteBaja(Id, connection);
        }

        public bool IntegranteModificacion(VOIntegrante integrante)
        {
            return this.integrantesService.IntegranteModificacion(integrante, connection);
        }

        public VOIntegrante IntegranteObtener(int Id)
        {
            return this.integrantesService.IntegranteObtener(Id, connection);
        }

        //SERVICIOS DE BANDA 
        public VOBanda obtenerBanda(int Id)
        {
            VOBanda response = bandasService.obtenerBanda(Id, connection);
            if (response == null)
            {
                response.Error = TipoError.ID_BANDA_INVALIDO.ToString();
            }
            return response;
        }

        public List<VOBanda> obtenerBandas()
        {
            return bandasService.ObtenerTodasLasBandas(connection);
        }

        public bool altaDeBanda(VOBanda banda)
        {
            return bandasService.AltaDeBanda(banda, connection);
        }

        public bool bajaDeBanda(int Id)
        {
            return bandasService.BajaDeBanda(Id, connection);
        }

        public bool modificarBanda(VOBanda banda)
        {
            return bandasService.ModificarBanda(banda, connection);
        }

        //SERVICIOS DE CANCION 
        public List<VOCancion> CancionListado()
        {
            return cancionService.CancionListado(connection);
        }

        public bool CancionAlta(VOCancion cancion)
        {
            return cancionService.CancionAlta(cancion, connection);
        }

        public bool CancionBaja(int Id)
        {
            return cancionService.CancionBaja(Id, connection);
        }

        public bool CancionModificacion(VOCancion cancion)
        {
            return cancionService.CancionModificacion(cancion, connection);
        }

        public VOCancion CancionObtener(int Id)
        {
            return cancionService.CancionObtener(Id, connection);
        }

        //SERVICIOS DE ALBUM 

        public List<VOAlbum> AlbumListado()
        {
            return albumesService.AlbumListado(connection);
        }

        public bool AlbumAlta(VOAlbum album)
        {
            return albumesService.AlbumAlta(album, connection);
        }

        public bool AlbumBaja(int Id)
        {
            return albumesService.AlbumBaja(Id, connection);
        }

        public bool AlbumModificacion(VOAlbum album)
        {
            return albumesService.AlbumModificacion(album, connection);
        }

        public VOAlbum AlbumObtener(int Id)
        {
            return albumesService.AlbumObtener(Id, connection);
        }

        //SERVICIOS MIXTOS
        public List<VOIntegrante> BandaIntegranteListado(int Id)
        {
            return bandasService.BandaIntegrantes(Id, connection);
        }
        public bool BandaIntegranteAlta(int BandaId, int IntegranteId)
        {
            return bandasService.IntegranteAltaEnBanda(BandaId, IntegranteId, connection);
        }

        public bool BandaIntegranteBaja(int IntegranteId)
        {
            return bandasService.IntegranteBajaEnBanda(IntegranteId, connection);
        }
        public List<VOAlbum> BandaAlbumListado(int Id)
        {
            return albumesService.AlbumListadoBanda(Id, connection);
        }

        public List<VOCancion> AlbumCancionListado(int Id)
        {
            return albumesService.AlbumCanionesListar(Id, connection);
        }

        public bool AlbumCancionAlta(int AlbumId, int CancionId)
        {
            return albumesService.AlbumAgregarCancion(AlbumId, CancionId, connection);
        }

        public bool AlbumCancionBaja(int AlbumId, int CancionId)
        {
            return albumesService.AlbumEliminarCancion(AlbumId, CancionId, connection);
        }

        //SERVICIOS DE USUARIO
        public bool Login(string username, string password)
        {
            return usersService.login(username, password, connection);
        }

        public bool UsuarioAlta(VOUsuario usuario)
        {
            return usersService.UsuarioAlta(usuario, connection);
        }

        public bool UsuarioBaja(string username)
        {
            return usersService.UsuarioBaja(username, connection);
        }

        public VOUsuario UsuarioObtener(int Id)
        {
            return usersService.UsuarioObtener(Id, connection);
        }

        public List<VOUsuario> UsuariosListado()
        {
            return usersService.UsuariosListado(connection);
        }

        public int UsuarioIdentifier(string username)
        {
            return usersService.UsuarioIdentifier(username, connection);
        }

        //SERVICIOS DE RESENIAS

        public List<VOResenia> ReseniaListado()
        {
            return reseniasService.ReseniaListado(connection);
        }

        public bool ReseniaAlta(VOResenia resenia)
        {
            return reseniasService.ReseniaAlta(resenia, connection);
        }

        public bool ReseniaBaja(int Id)
        {
            return reseniasService.ReseniaBaja(Id, connection);
        }

        public bool ReseniaModificacion (VOResenia resenia)
        {
            return reseniasService.ReseniaModificacion(resenia, connection);
        }

        public VOResenia ReseniaObtener(int Id)
        {
            return reseniasService.ReseniaObtener(Id, connection);
        }

    }
}
