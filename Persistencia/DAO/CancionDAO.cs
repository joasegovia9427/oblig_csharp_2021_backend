using System;
using System.Data;
using Persistencia.Utils;
using Persistencia.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistencia.DAO
{
    public class CancionDAO
    {
        private IntegranteDAO integranteDAO;

        private GeneroMusicalDAO generoDAO;

        public CancionDAO(IntegranteDAO daoIntegrante, GeneroMusicalDAO daoGenero)
        {
            integranteDAO = daoIntegrante;
            generoDAO = daoGenero;
        }

        public bool PerteneceCancion(int Id, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindById(Constantes.CANCION, Id),
                                Constantes.CANCION,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public bool PerteneceCancion(string cancionNombre, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindByAttributeString(Constantes.CANCION, Constantes.NOMBRE, cancionNombre),
                                Constantes.CANCION,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public int InsertarCancion(Cancion cancion, SqlConnection connection)
        {
            if (!this.PerteneceCancion(cancion.Nombre.ToLower(), connection) )
            {
                string query = String.Format("insert into dbo.Cancion(Nombre, Duracion, Anio, Dato, IntegranteId, GeneroMusicalId) " +
                                    "output inserted.Id values('" + cancion.Nombre + "', " + cancion.Duracion.ToString().Replace(',','.') + ", " + cancion.Anio + ", '"
                                    + cancion.Dato + "', " + cancion.Integrante.Id + "," + cancion.Genero.Id +") ",
                                    cancion.Nombre,
                                    cancion.Duracion.ToString().Replace(',', '.'),
                                    cancion.Anio,
                                    cancion.Integrante.Id,
                                    cancion.Genero.Id
                                    );
                return AccesoBD.EjecutarQueryRetornoID(query, connection);
            }
            return 0;
        }

        public bool BorrarCancion(int Id, SqlConnection connection)
        {
            if (this.PerteneceCancion(Id, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(QueryBuilder.DeleteById(Constantes.CANCION, Id), connection);
                return true;
            }
            return false;
        }

        public Cancion ObtenerCancion(int Id, SqlConnection connection)
        {
            if (this.PerteneceCancion(Id, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindById(Constantes.CANCION, Id),
                    Constantes.CANCION,
                    connection
                    );
                DataRow row = response.Rows[0];

                return ExtraerCancion(row, connection);
            }
            return null;
        }


        public bool ModificarCancion(Cancion cancion, SqlConnection connection)
        {
            if (this.PerteneceCancion(cancion.Id, connection))
            {
                string query = String.Format(
                    "UPDATE Cancion set Nombre = '{0}', Duracion = {1}, Anio = {2}, Dato = '{3}', GeneroMusicalId = {4}, IntegranteId = {5} WHERE Id = {6}",
                    cancion.Nombre,
                    cancion.Duracion.ToString().Replace(',', '.'),
                    cancion.Anio,
                    cancion.Dato,
                    cancion.Genero.Id,
                    cancion.Integrante.Id,
                    cancion.Id
                    );
                AccesoBD.EjecutarQuerySinRetorno(query, connection);
                return true;
            }
            return false;
        }

        public bool ExistenCanciones(SqlConnection connection)
        {
            return AccesoBD.ExistenValoresEnTabla(Constantes.CANCION, connection);
        }

        public List<Cancion> ObtenerTodasLasCanciones(SqlConnection connection)
        {
            List<Cancion> canciones = new List<Cancion>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindAll(Constantes.CANCION), Constantes.CANCION, connection);
            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    canciones.Add(ExtraerCancion(row, connection));
                }
            }
            return canciones;
        }

        private Cancion ExtraerCancion(DataRow row, SqlConnection connection)
        {
            return new Cancion(
                Convert.ToInt32(row[Constantes.ID]),
                Convert.ToString(row[Constantes.NOMBRE]),
                Convert.ToInt32(row[Constantes.DURACION]),
                Convert.ToInt32(row[Constantes.ANIO]),
                Convert.ToString(row[Constantes.DATO]),
                integranteDAO.ObtenerIntegrante(Convert.ToInt32(row[Constantes.INTEGRANTE_ID]), connection),
                generoDAO.ObtenerGeneroMusical(Convert.ToInt32(row[Constantes.GENERO_MUSICAL_ID]), connection)
            );
        }

    }
}
