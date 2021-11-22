using System;
using System.Collections.Generic;
using Persistencia.Entidades;
using System.Data;
using Persistencia.Utils;
using System.Data.SqlClient;

namespace Persistencia.DAO
{
    public class BandaDAO
    {
        private GeneroMusicalDAO generoMusicalDAO;

        public BandaDAO(GeneroMusicalDAO daoGeneroMusical)
        {
            generoMusicalDAO = daoGeneroMusical;
        }

        public bool PerteneceBanda(int Id, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindById(Constantes.BANDA, Id),
                                Constantes.BANDA,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public bool PerteneceBanda(string nombreBanda, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindByAttributeString(Constantes.BANDA, Constantes.NOMBRE, nombreBanda),
                                Constantes.BANDA,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public int InsertarBanda(Banda banda, SqlConnection connection)
        {
            String query = String.Format("insert into dbo.Banda(Nombre, AnioCreacion, AnioSeparacion, GeneroMusicalId)  output inserted.Id " +
                                "values('{0}', {1}, {2}, {3})",
                                banda.Nombre,
                                banda.AnioCreacion,
                                banda.AnioSeparacion,
                                banda.GeneroMusical.Id
                                );
            return AccesoBD.EjecutarQueryRetornoID(query, connection);
         
        }

        public bool BorrarBanda(int Id, SqlConnection connection)
        {
            if (this.PerteneceBanda(Id, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(
                    QueryBuilder.DeleteById(Constantes.BANDA, Id), 
                    connection);
                return true;
            }
            return false;
        }

        public Banda ObtenerBanda(int Id, SqlConnection connection)
        {
            if (PerteneceBanda(Id, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindById(Constantes.BANDA, Id),
                    Constantes.BANDA,
                    connection
                    );
                DataRow row = response.Rows[0];

                return ExtraerBanda(row, connection);
            }
            return null;
        }

        public Banda ObtenerBanda(string bandaName, SqlConnection connection)
        {
            if (PerteneceBanda(bandaName, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindByAttributeString(Constantes.BANDA, Constantes.NOMBRE, bandaName),
                    Constantes.BANDA,
                    connection
                    );
                DataRow row = response.Rows[0];

                return ExtraerBanda(row, connection);
            }
            return null;
        }


        public bool ModificarBanda(Banda banda, SqlConnection connection)
        {

            if (PerteneceBanda(banda.Id, connection))
            {
                string query = String.Format("UPDATE Banda set Nombre = '{0}', AnioCreacion = {1}, AnioSeparacion = {2}," +
                    "GeneroMusicalId = {3} WHERE Id = {4}",
                    banda.Nombre,
                    banda.AnioCreacion,
                    banda.AnioSeparacion,
                    banda.GeneroMusical.Id,
                    banda.Id
                    );
                AccesoBD.EjecutarQuerySinRetorno(query, connection);
                return true; 
            }
            return false;
        }

        public bool ExistenBandas(SqlConnection connection)
        {
            return AccesoBD.ExistenValoresEnTabla(Constantes.BANDA, connection);
        }

        public List<Banda> ObtenerTodasLasBandas(SqlConnection connection)
        {
            List<Banda> bandas = new List<Banda>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindAll(Constantes.BANDA), Constantes.BANDA, connection);
            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    bandas.Add(ExtraerBanda(row, connection));
                }
            }
            return bandas;
        }

        private Banda ExtraerBanda(DataRow row, SqlConnection connection)
        {
            return new Banda(
                Convert.ToInt32(row[Constantes.ID]),
                Convert.ToString(row[Constantes.NOMBRE]),
                Convert.ToInt32(row[Constantes.ANIO_CREACION]),
                Convert.ToInt32(row[Constantes.ANIO_SEPARACION]),
                generoMusicalDAO.ObtenerGeneroMusical(Convert.ToInt32(row[Constantes.GENERO_MUSICAL_ID]), connection)
                );
        }
    }
}
