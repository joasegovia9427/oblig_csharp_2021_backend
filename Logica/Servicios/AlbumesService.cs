using System;
using System.Data.SqlClient;
using Persistencia.DAO;
using Persistencia.Entidades;
using Logica.VO;
using Logica.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace Logica.Servicios
{
    public class AlbumesService
    {
        private GeneroMusicalDAO daoGeneroMusical;
        private PersonasDAO daoPersonas;
        private IntegranteDAO daoIntegrante;
        private BandaDAO daoBanda;
        private AlbumDAO daoAlbum;
        private AlbumCancionDAO daoAlbumCancion;
        private CancionDAO daoCancion;
        private BandasService bandasService;

        public AlbumesService()
        {
            daoGeneroMusical = new GeneroMusicalDAO();
            daoPersonas = new PersonasDAO();
            daoBanda = new BandaDAO(daoGeneroMusical);
            daoIntegrante = new IntegranteDAO(daoPersonas, daoBanda);
            daoAlbum = new AlbumDAO(daoGeneroMusical, daoBanda);
            daoCancion = new CancionDAO(daoIntegrante, daoGeneroMusical);
            daoAlbumCancion = new AlbumCancionDAO(daoCancion);
            bandasService = new BandasService();
        }

        public List<VOAlbum> AlbumListado(SqlConnection connection)
        {
            List<VOAlbum> response = new List<VOAlbum>();
            List<Album> albumes = daoAlbum.ObtenerTodosLosAlbumes(connection);
            foreach(Album album in albumes)
            {
                response.Add(this.obtenerVOAlbumDeAlbum(album, connection));
            }
            return response;
        }

        public bool AlbumAlta(VOAlbum album, System.Data.SqlClient.SqlConnection connection)
        {
            if (daoAlbum.PerteneceAlbum(album.Id, connection))
            {
                Console.WriteLine("El Album ingresado ya existe en el sistema.");
                return false;
            } 

            GeneroMusical generoMusical = daoGeneroMusical.ObtenerGeneroMusical(album.Genero, connection);
            if(generoMusical == null)
            {
                int nuevoGenero = daoGeneroMusical.InsertarGeneroMusical(album.Genero, connection);
                generoMusical = new GeneroMusical(nuevoGenero, album.Genero);
            }
            Banda banda = null;
            if (album.Banda != null)
            {
                banda = daoBanda.ObtenerBanda(album.Banda.Id, connection);
                if (banda == null)
                {
                    Console.WriteLine("El Album ingresado tiene una banda incorrecta.");
                    return false;
                }
            }

            foreach(VOCancion cancion in album.Canciones)
            {
                if (!daoCancion.PerteneceCancion(cancion.Id, connection))
                {
                    Console.WriteLine("El album ingresado tiene canciones no validas");
                    return false;
                }
            }

            int albumId = daoAlbum.InsertarAlbum(new Album(album.Nombre, album.AnioCreacion, generoMusical, banda), connection);
            if(albumId == 0)
            {
                Console.WriteLine("Existio un error al persistir el nuevo album.");
                return false;
            }

            foreach (VOCancion cancion in album.Canciones)
            {
                daoAlbumCancion.InsertarAlbumCancion(new AlbumCancion(albumId, cancion.Id), connection);
            }

            return true;
        }

        public bool AlbumBaja(int Id, SqlConnection connection)
        {
            if (!daoAlbum.PerteneceAlbum(Id, connection))
            {
                Console.WriteLine("El Album ingresado no existe en el sistema.");
                return false;
            }
            daoAlbumCancion.BorrarAlbumCancionPorAlbumId(Id, connection);
            return daoAlbum.BorrarAlbum(Id, connection);
        }

        public bool AlbumModificacion(VOAlbum album, SqlConnection connection)
        {
            if (!daoAlbum.PerteneceAlbum(album.Id, connection))
            {
                Console.WriteLine("El Album ingresado no existe en el sistema.");
                return false;
            }
            Album albumBase = daoAlbum.ObtenerAlbum(album.Id, connection);
            albumBase.Nombre = album.Nombre;
            albumBase.AnioCreacion = album.AnioCreacion;

            GeneroMusical generoMusical = daoGeneroMusical.ObtenerGeneroMusical(album.Genero, connection);
            if (generoMusical == null)
            {
                int nuevoGenero = daoGeneroMusical.InsertarGeneroMusical(album.Genero, connection);
                generoMusical = new GeneroMusical(nuevoGenero, album.Genero);
            }
            albumBase.GeneroMusical = generoMusical;

            Banda banda = album.Banda == null? null : daoBanda.ObtenerBanda(album.Banda.Id, connection);
            albumBase.Banda = banda;

            foreach (VOCancion cancion in album.Canciones)
            {
                if (!daoCancion.PerteneceCancion(cancion.Id, connection))
                {
                    Console.WriteLine("El album ingresado tiene canciones no validas");
                    return false;
                }
            }

            if (!daoAlbum.ModificarAlbum(albumBase, connection))
            {
                Console.WriteLine("Existio un error al modificar el album.");
                return false;
            }

            Dictionary<int, Cancion> cancionesBase = daoAlbumCancion.ObtenerTodasLasCancionesDeAlbum(album.Id, connection).ToDictionary(a => a.Id, a => a);
            foreach(VOCancion cancion in album.Canciones)
            {
                if (!cancionesBase.ContainsKey(cancion.Id))
                {
                    daoAlbumCancion.InsertarAlbumCancion(new AlbumCancion(album.Id, cancion.Id), connection);
                }
                else
                {
                    cancionesBase.Remove(cancion.Id);
                }
            }

            if(cancionesBase.Count != 0)
            {
                foreach(KeyValuePair<int, Cancion> entry in cancionesBase)
                {
                    daoAlbumCancion.BorrarAlbumCancion(album.Id, entry.Key, connection);
                }
            }

            return true;
        }

        public VOAlbum AlbumObtener(int Id, SqlConnection connection)
        {
            Album album = daoAlbum.ObtenerAlbum(Id, connection);
            if (album != null)
            {
                return this.obtenerVOAlbumDeAlbum(album, connection);
            }
            return null;
        }

        public List<VOAlbum> AlbumListadoBanda(int Id, System.Data.SqlClient.SqlConnection connection)
        {
            List<Album> albumes = daoAlbum.ObtenerAlbumesDeBanda(Id, connection);
            
            List<Integrante> integrantes = daoIntegrante.ObtenerIntegrantesBanda(Id, connection);
            List<VOIntegrante> vOIntegrantes = new List<VOIntegrante>();
            integrantes.ForEach(i => vOIntegrantes.Add(IntegranteMapper.IntegranteToVOIntegrante(i)));

            VOBanda banda = BandaMapper.BandaToVOBanda(daoBanda.ObtenerBanda(Id, connection), vOIntegrantes);

            List<VOAlbum> response = new List<VOAlbum>();
            albumes.ForEach(a =>
            {
               response.Add(AlbumMapper.AlbumToVOAlbum(a, banda, this.AlbumCanionesListar(a.Id, connection)));
            });

            return response;
        }

        public List<VOCancion> AlbumCanionesListar(int AlbumId, SqlConnection connection)
        {
            List<VOCancion> canciones = new List<VOCancion>();
            if (!daoAlbum.PerteneceAlbum(AlbumId, connection))
            {
                Console.WriteLine("No existe el album buscado.");
            }
            else
            {
                daoAlbumCancion.ObtenerTodasLasCancionesDeAlbum(AlbumId, connection).ForEach(c => canciones.Add(CancionesMapper.CancionToVOCancion(c)));

            }
            return canciones;
        }

        public bool AlbumAgregarCancion(int AlbumId, int CancionId, SqlConnection connection)
        {
            if (!daoAlbum.PerteneceAlbum(AlbumId, connection))
            {
                Console.Write("El album ingresado no existe en el sistema.");
                return false;
            }

            if (!daoCancion.PerteneceCancion(CancionId, connection))
            {
                Console.Write("La cancion ingresada no existe en el sistema.");
                return false;
            }

            if(daoAlbumCancion.ExisteAlbumCancion(AlbumId, CancionId, connection))
            {
                return true;
            }

            return daoAlbumCancion.InsertarAlbumCancion(new AlbumCancion(AlbumId, CancionId), connection);
        }

        public bool AlbumEliminarCancion(int AlbumId, int CancionId, SqlConnection connection)
        {
            if (!daoAlbum.PerteneceAlbum(AlbumId, connection))
            {
                Console.Write("El album ingresado no existe en el sistema.");
                return false;
            }

            if (!daoCancion.PerteneceCancion(CancionId, connection))
            {
                Console.Write("La cancion ingresada no existe en el sistema.");
                return false;
            }

            return daoAlbumCancion.BorrarAlbumCancion(AlbumId, CancionId, connection);
           
        }


        private VOAlbum obtenerVOAlbumDeAlbum(Album album, SqlConnection connection)
        {

            List<VOIntegrante> vOIntegrantes = album.Banda == null ? new List<VOIntegrante>() : 
                bandasService.obtenerIntegrantesBanda(daoIntegrante.ObtenerIntegrantesBanda(album.Banda.Id, connection), connection);
            List<VOCancion> voCanciones = new List<VOCancion>();
            foreach (Cancion cancion in daoAlbumCancion.ObtenerTodasLasCancionesDeAlbum(album.Id, connection))
            {
                voCanciones.Add(CancionesMapper.CancionToVOCancion(cancion));
            }
            return
                new VOAlbum(
                album.Id,
                album.Nombre,
                album.AnioCreacion,
                album.GeneroMusical.Nombre,
                album.Banda == null ? null :
                new VOBanda(
                    album.Banda.Id,
                    album.Banda.Nombre,
                    album.Banda.AnioCreacion,
                    album.Banda.AnioSeparacion,
                    album.Banda.GeneroMusical.Nombre,
                    vOIntegrantes,
                    null),
                    voCanciones,
                    null
                 );
                
        }

    }
}
