using System;
using Persistencia.DAO;
using Persistencia.Entidades;
using Logica.VO;
using Logica.Mapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Logica.Servicios
{
    public class CancionesService
    {
        private GeneroMusicalDAO daoGeneroMusical;
        private PersonasDAO daoPersonas;
        private IntegranteDAO daoIntegrante;
        private BandaDAO daoBanda;
        private AlbumDAO daoAlbum;
        private AlbumCancionDAO daoAlbumCancion;
        private CancionDAO daoCancion;

        public CancionesService()
        {
            daoGeneroMusical = new GeneroMusicalDAO();
            daoPersonas = new PersonasDAO();
            daoBanda = new BandaDAO(daoGeneroMusical);
            daoIntegrante = new IntegranteDAO(daoPersonas, daoBanda);
            daoAlbum = new AlbumDAO(daoGeneroMusical, daoBanda);
            daoCancion = new CancionDAO(daoIntegrante, daoGeneroMusical);
            daoAlbumCancion = new AlbumCancionDAO(daoCancion);
        }

        public List<VOCancion> CancionListado(SqlConnection connection)
        {
            List<VOCancion> response = new List<VOCancion>();
            foreach(Cancion cancion in this.daoCancion.ObtenerTodasLasCanciones(connection))
            {
                response.Add(CancionesMapper.CancionToVOCancion(cancion));
            }
            return response;
        }

        public bool CancionAlta(VOCancion voCancion, SqlConnection connection)
        {
            Integrante integrante = daoIntegrante.ObtenerIntegrante(voCancion.IntegranteId, connection);
            if(integrante == null)
            {
                Console.WriteLine("No se encontro integrante o interprete para la cancion");
                return false;
            }

            GeneroMusical genero = daoGeneroMusical.ObtenerGeneroMusical(voCancion.Genero, connection);
            if(genero == null)
            {
                int nuevoGeneroId = daoGeneroMusical.InsertarGeneroMusical(voCancion.Genero, connection);
                genero = daoGeneroMusical.ObtenerGeneroMusical(nuevoGeneroId, connection);
            }

            Cancion cancion = CancionesMapper.VOCancionToCancion(voCancion, integrante, genero);
            int canciondId = daoCancion.InsertarCancion(cancion, connection);
            if (canciondId == 0)
            {
                Console.WriteLine("No se pudo ingresar la nueva cancion");
                return false;
            }

            return true;
        }

        public bool CancionBaja(int Id, SqlConnection connection)
        {
            daoAlbumCancion.BorrarAlbumCancionParaCancionId(Id, connection);
            return daoCancion.BorrarCancion(Id, connection);
        }

        public bool CancionModificacion(VOCancion voCancion, SqlConnection connection)
        {
            Cancion cancion = daoCancion.ObtenerCancion(voCancion.Id, connection);
            if(cancion == null)
            {
                Console.WriteLine("No se puede encontrar la cancion a modificar.");
                return false;
            }

            Integrante integrante = daoIntegrante.ObtenerIntegrante(voCancion.IntegranteId, connection);
            if (integrante == null)
            {
                Console.WriteLine("No se encontro integrante o interprete modificar la cancion");
                return false;
            }

            GeneroMusical genero = daoGeneroMusical.ObtenerGeneroMusical(voCancion.Genero, connection);
            if (genero == null)
            {
                int nuevoGeneroId = daoGeneroMusical.InsertarGeneroMusical(voCancion.Genero, connection);
                genero = daoGeneroMusical.ObtenerGeneroMusical(nuevoGeneroId, connection);
            }

            cancion = CancionesMapper.VOCancionToCancion(voCancion, integrante, genero);

            return daoCancion.ModificarCancion(cancion, connection);
        }

        public VOCancion CancionObtener(int Id, SqlConnection connection)
        {
            Cancion cancion = daoCancion.ObtenerCancion(Id, connection);
            if(cancion != null)
            {
                return CancionesMapper.CancionToVOCancion(cancion);
            }
            return null;
        }

    }
}
