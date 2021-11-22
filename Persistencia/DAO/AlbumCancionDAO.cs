using System;
using System.Data;
using Persistencia.Utils;
using Persistencia.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;
using Persistencia.Enums;

namespace Persistencia.DAO
{
    public class AlbumCancionDAO
    {
        private CancionDAO cancionDAO;

        public AlbumCancionDAO(CancionDAO daoCancion)
        {
            cancionDAO = daoCancion;
        }

        public Resultado ExisteCancionEnAlbum(int albumId, SqlConnection connection)
        {
            return Existe(QueryBuilder.FindByAttributeInt(
                        Constantes.ALBUM_CANCION,
                        Constantes.ALBUM_ID,
                        albumId),
                    connection);
        }

        public Resultado ExisteCancionEnAlbumCancion(int CancionId, SqlConnection connection)
        {
            return Existe(QueryBuilder.FindByAttributeInt(
                        Constantes.ALBUM_CANCION,
                        Constantes.CANCION_ID,
                        CancionId), 
                    connection);
        }

        public bool ExisteAlbumCancion(int albumId, int cancionId, SqlConnection connection)
        {
            string query = String.Format("SELECT * from AlbumCancion WHERE AlbumId = {0} AND CancionId = {1}",
                            albumId, cancionId);
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                    query,
                                    Constantes.ALBUM_CANCION,
                                    connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public bool InsertarAlbumCancion(AlbumCancion albumCancion, SqlConnection connection)
        {
            if (!ExisteAlbumCancion(albumCancion.AlbumId, albumCancion.CancionId, connection))
            {
                string query = String.Format(
                    "insert into dbo.AlbumCancion(AlbumId, CancionId) values({0}, {1}) ",
                    albumCancion.AlbumId, 
                    albumCancion.CancionId
                    );

                AccesoBD.EjecutarQuerySinRetorno(query, connection);
                return true;
            }
            return false;
        }

        public bool BorrarAlbumCancion(int albumId, int cancionId, SqlConnection connection)
        {
            if (ExisteAlbumCancion(albumId, cancionId, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(
                    QueryBuilder.DeleteByCompundId(
                        Constantes.ALBUM_CANCION, 
                        Constantes.ALBUM_ID, 
                        albumId, 
                        Constantes.CANCION_ID, 
                        cancionId
                        ), 
                    connection);
                return true;
            }
            return false;
        }

        public bool ExistenAlbumCanciones(SqlConnection connection)
        {
            return AccesoBD.ExistenValoresEnTabla(Constantes.ALBUM_CANCION, connection);
        }

        public List<Cancion> ObtenerTodasLasCancionesDeAlbum(int albumId, SqlConnection connection)
        {
            List<Cancion> canciones = new List<Cancion>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                QueryBuilder.FindByAttributeInt(
                    Constantes.ALBUM_CANCION, 
                    Constantes.ALBUM_ID, 
                    albumId), 
                Constantes.ALBUM_CANCION, 
                connection);

            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    canciones.Add(new Cancion(
                        cancionDAO.ObtenerCancion(
                            Convert.ToInt32(row[Constantes.CANCION_ID]), connection)
                    ));
                }
            }
            return canciones;
        }

        public bool BorrarAlbumCancionPorAlbumId(int albumId, SqlConnection connection)
        {
            if (ExisteCancionEnAlbum(albumId, connection).esExitoso)
            {
                AccesoBD.EjecutarQuerySinRetorno(
                    QueryBuilder.DeleteByAttributeInt(
                        Constantes.ALBUM_CANCION, 
                        Constantes.ALBUM_ID, 
                        albumId
                        ),
                    connection);
                return true;
            }
            return false;
        }

        public bool BorrarAlbumCancionParaCancionId(int cancionId, SqlConnection connection)
        {
            if (ExisteCancionEnAlbumCancion(cancionId, connection).esExitoso)
            {
                AccesoBD.EjecutarQuerySinRetorno(
                    QueryBuilder.DeleteByAttributeInt(
                        Constantes.ALBUM_CANCION,
                        Constantes.CANCION_ID,
                        cancionId
                        ),
                    connection);
                return true;
            }
            return false;
        }


        public Resultado Existe(string query, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(query, Constantes.ALBUM_CANCION, connection);
            if (response.Rows == null)
            {
                return ResultadoErroneo.ErrorEnConexion();
            }

            Resultado resultado = new Resultado();
            resultado.entidad = Constantes.ALBUM_CANCION;
            if (response.Rows.Count != 0)
            {
                resultado.esExitoso = true;
                return resultado;
            }

            resultado.esExitoso = false;
            resultado.error = TipoError.NO_EXISTE;
            return resultado;
        }
    }
}
