using System;
using System.Data;
using Persistencia.Utils;
using Persistencia.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistencia.DAO
{
    public class AlbumDAO
    {
        private GeneroMusicalDAO generoMusicalDAO;

        private BandaDAO bandaDAO;

        public AlbumDAO(GeneroMusicalDAO daoGeneroMusical, BandaDAO daoBanda)
        {
            generoMusicalDAO = daoGeneroMusical;
            bandaDAO = daoBanda;
        }

        public bool PerteneceAlbum(int Id, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindById(Constantes.ALBUM, Id),
                                Constantes.ALBUM,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public bool PerteneceAlbum(string albumNombre, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindByAttributeString(Constantes.ALBUM, Constantes.NOMBRE, albumNombre),
                                Constantes.ALBUM,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public int InsertarAlbum(Album album, SqlConnection connection)
        {
            if (!this.PerteneceAlbum(album.Nombre, connection))
            {
                int bandaId = album.Banda == null ? 0 : album.Banda.Id;
                string query = "insert into dbo.Album(Nombre, AnioCreacion, GeneroMusicalId, BandaId) output inserted.Id" +
                                    " values('" + album.Nombre + "', " + album.AnioCreacion + ", " + album.GeneroMusical.Id+ ", " + bandaId +") " ;
                                Console.WriteLine(query);

                return AccesoBD.EjecutarQueryRetornoID(query, connection);
            }
            return 0;
        }

        public bool BorrarAlbum(int Id, SqlConnection connection)
        {
            if (this.PerteneceAlbum(Id, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(QueryBuilder.DeleteById(Constantes.ALBUM, Id), connection);
                return true;
            }
            return false;
        }

        public Album ObtenerAlbum(int Id, SqlConnection connection)
        {
            if (this.PerteneceAlbum(Id, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindById(Constantes.ALBUM, Id),
                    Constantes.ALBUM,
                    connection
                    );
                DataRow row = response.Rows[0];

                return ExtraerAlbum(row, connection);
            }
            return null;
        }


        public bool ModificarAlbum(Album album, SqlConnection connection)
        {
            if (this.PerteneceAlbum(album.Id, connection))
            {
                int bandaId = album.Banda == null ? 0 : album.Banda.Id;
                string query = "UPDATE Album set Nombre = '" + album.Nombre + "', AnioCreacion = " + album.AnioCreacion + ", GeneroMusicalId =" + album.GeneroMusical.Id+
                    ", BandaId ="+ bandaId +" WHERE Id =" + album.Id;
                AccesoBD.EjecutarQuerySinRetorno(query, connection);
                return true;
            }
            return false;
        }

        public bool ExistenAlbumes(SqlConnection connection)
        {
            return AccesoBD.ExistenValoresEnTabla(Constantes.ALBUM, connection);
        }

        public List<Album> ObtenerTodosLosAlbumes(SqlConnection connection)
        {
            List<Album> Albumes = new List<Album>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindAll(Constantes.ALBUM), Constantes.ALBUM, connection);
            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    Albumes.Add(ExtraerAlbum(row, connection));
                }
            }
            return Albumes;
        }

        public List<Album> ObtenerAlbumesDeBanda(int bandaId, SqlConnection connection)
        {
            List<Album> Albumes = new List<Album>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindByAttributeInt(Constantes.ALBUM, Constantes.BANDA_ID, bandaId), Constantes.ALBUM, connection);
            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    Albumes.Add(ExtraerAlbum(row, connection));
                }
            }
            return Albumes;
        }

        private Album ExtraerAlbum(DataRow row, SqlConnection connection)
        {
            return new Album(
                    Convert.ToInt32(row[Constantes.ID]),
                    Convert.ToString(row[Constantes.NOMBRE]),
                    Convert.ToInt32(row[Constantes.ANIO_CREACION]),
                    generoMusicalDAO.ObtenerGeneroMusical(Convert.ToInt32(row[Constantes.GENERO_MUSICAL_ID]), connection),
                    bandaDAO.ObtenerBanda(Convert.ToInt32(row[Constantes.BANDA_ID]), connection)
                    );
        }
    }
}
