using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Logica.EntryPoint;
using Logica.VO;

namespace MusicServiceSOAP
{
    /// <summary>
    /// Descripción breve de MusicUDE_Serice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class MusicUDE_Serice : System.Web.Services.WebService
    {
        private Facade fecade = Facade.Instance;

        //SERVICIOS DE GENERO MUSICAL
        [WebMethod]
        public bool GeneroMusicalAlta(string genero)
        {
            return fecade.GeneroMusicalAlta(genero);
        }

        [WebMethod]
        public bool GeneroMusicalModificacion(int Id, string genero)
        {
            return fecade.GeneroMusicalModificacion(Id, genero);
        }
        [WebMethod]
        public bool GeneroMusicalBaja(int Id)
        {
            return fecade.GeneroMusicalBaja(Id);
        }
        [WebMethod]
        public List<VOGeneroMusical> GeneroMusicalListado()
        {
            return fecade.GeneroMusicalListado();
        }

        //SERVICIOS DE INTEGRANTES
        [WebMethod]
        public List<VOIntegrante> IntegrantesListado()
        {
            return fecade.IntegrantesListado();
        }
        [WebMethod]
        public bool IntegranteAlta(VOIntegrante integrante)
        {
            return fecade.IntegranteAlta(integrante);
        }
        [WebMethod]
        public bool IntegranteBaja(int Id)
        {
            return fecade.IntegranteBaja(Id);
        }
        [WebMethod]
        public bool IntegranteModificacion(VOIntegrante integrante)
        {
            return fecade.IntegranteModificacion(integrante);
        }
        [WebMethod]
        public VOIntegrante IntegranteObtener(int Id)
        {
            return fecade.IntegranteObtener(Id);
        }

        //SERVICIOS DE BANDA 
        [WebMethod]
        public VOBanda BandaObtener(int Id)
        {
            return fecade.obtenerBanda(Id);
        }

        [WebMethod]
        public List<VOBanda> BandaListado()
        {
            return fecade.obtenerBandas();
        }

        [WebMethod]
        public bool BandaAlta(VOBanda banda)
        {
            return fecade.altaDeBanda(banda);
        }

        [WebMethod]
        public bool BandaBaja(int Id)
        {
            return fecade.bajaDeBanda(Id);
        }

        [WebMethod]
        public bool BandaModificar(VOBanda banda)
        {
            return fecade.modificarBanda(banda);
        }

        //SERVICIOS DE CANCION
        [WebMethod]
        public List<VOCancion> CancionListado()
        {
            return fecade.CancionListado();
        }
        [WebMethod]
        public bool CancionAlta(VOCancion cancion)
        {
            return fecade.CancionAlta(cancion);
        }
        [WebMethod]
        public bool CancionBaja(int Id)
        {
            return fecade.CancionBaja(Id);
        }
        [WebMethod]
        public bool CancionModificacion(VOCancion cancion)
        {
            return fecade.CancionModificacion(cancion);
        }
        [WebMethod]
        public VOCancion CancionObtener(int Id)
        {
            return fecade.CancionObtener(Id);
        }

        //SERVICIOS ALBUM

        [WebMethod]
        public VOAlbum AlbumObtener(int Id)
        {
            return fecade.AlbumObtener(Id);
        }

        [WebMethod]
        public List<VOAlbum> AlbumListado()
        {
            return fecade.AlbumListado();
        }

        [WebMethod]
        public bool AlbumAlta(VOAlbum album)
        {
            return fecade.AlbumAlta(album);
        }

        [WebMethod]
        public bool AlbumBaja(int Id)
        {
            return fecade.AlbumBaja(Id);
        }

        [WebMethod]
        public bool AlbumModificar(VOAlbum album)
        {
            return fecade.AlbumModificacion(album);
        }


        //SERVICIOS MIXTOS 

        [WebMethod]
        public List<VOIntegrante> BandaIntegranteListado(int Id)
        {
            return fecade.BandaIntegranteListado(Id);
        }

        [WebMethod]
        public bool BandaIntegranteAlta(int BandaId, int IntegranteId)
        {
            return fecade.BandaIntegranteAlta(BandaId, IntegranteId);
        }

        [WebMethod]
        public bool BandaIntegranteBaja(int IntegranteId)
        {
            return fecade.BandaIntegranteBaja(IntegranteId);
        }

        [WebMethod]
        public List<VOAlbum> BandaAlbumListado(int Id)
        {
            return fecade.BandaAlbumListado(Id);
        }

        [WebMethod]
        public List<VOCancion> AlbumCancionListado(int Id)
        {
            return fecade.AlbumCancionListado(Id);
        }

        [WebMethod]
        public bool AlbumCancionAlta(int AlbumId, int CancionId)
        {
            return fecade.AlbumCancionAlta(AlbumId, CancionId);
        }

        [WebMethod]
        public bool AlbumCancionBaja(int AlbumId, int CancionId)
        {
            return fecade.AlbumCancionBaja(AlbumId, CancionId);
        }

        //SERVICIOS USUARIO 
        [WebMethod]
        public bool Login(string username, string password)
        {
            return fecade.Login(username, password);
        }
        [WebMethod]
        public bool UsuarioAlta(VOUsuario usuario)
        {
            return fecade.UsuarioAlta(usuario);
        }
        [WebMethod]
        public bool UsuarioBaja(string username)
        {
            return fecade.UsuarioBaja(username);
        }
        [WebMethod]
        public VOUsuario UsuarioObtener(int Id)
        {
            return fecade.UsuarioObtener(Id);
        }

        //SERVICIOS DE RESENIA
        [WebMethod]
        public List<VOResenia> ReseniaListado()
        {
            return fecade.ReseniaListado();
        }
        [WebMethod]
        public bool ReseniaAlta(VOResenia resenia)
        {
            return fecade.ReseniaAlta(resenia);
        }
        [WebMethod]
        public bool ReseniaBaja(int Id)
        {
            return fecade.ReseniaBaja(Id);
        }
        [WebMethod]
        public bool ReseniaModificacion(VOResenia resenia)
        {
            return fecade.ReseniaModificacion(resenia);
        }
        [WebMethod]
        public VOResenia ReseniaObtener(int Id)
        {
            return fecade.ReseniaObtener(Id);
        }
    }
}
