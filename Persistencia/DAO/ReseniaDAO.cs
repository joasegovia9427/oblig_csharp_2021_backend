using System;
using System.Data;
using Persistencia.Utils;
using Persistencia.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistencia.DAO
{
    public class ReseniaDAO
    {
        public ReseniaDAO()
        {
        }

        public bool PerteneceResenia(int Id, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindById(Constantes.RESENIA, Id),
                                Constantes.RESENIA,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public int InsertarResenia(Resenia resenia, SqlConnection connection)
        {
            string idBanda = (resenia.BandaId == 0) ? "null" : resenia.BandaId.ToString();
            string idCancion = (resenia.CancionId == 0) ? "null" : resenia.CancionId.ToString();
            string query = "insert into dbo.Resenia(Puntaje, Resenia, UsuarioId, BandaId, CancionId) output inserted.Id " +
                                "values(" + resenia.Puntaje + ", '" + resenia.resenia + "', " + resenia.UsuarioId + "" +
                                ", " + idBanda + ", " + idCancion + ") ";
            return AccesoBD.EjecutarQueryRetornoID(query, connection);
            
        }

        public bool BorrarResenia(int Id, SqlConnection connection)
        {
            if (this.PerteneceResenia(Id, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(QueryBuilder.DeleteById(Constantes.RESENIA, Id), connection);
                return true;
            }
            return false;
        }

        public Resenia ObtenerResenia(int Id, SqlConnection connection)
        {
            if (this.PerteneceResenia(Id, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindById(Constantes.RESENIA, Id),
                    Constantes.RESENIA,
                    connection
                    );
                DataRow row = response.Rows[0];

                return ExtraerResenia(row, connection);
            }
            return null;
        }


        public bool ModificarResenia(Resenia resenia, SqlConnection connection)
        {
            if (this.PerteneceResenia(resenia.Id, connection))
            {
                string query = "UPDATE Resenia set Puntaje = " + resenia.Puntaje + ", Resenia = '" + resenia.resenia+ "', UsuarioId =" + resenia.UsuarioId +
                    ", BandaId =" + resenia.BandaId + ", CancionId =" + resenia.CancionId + " WHERE Id =" + resenia.Id;
                AccesoBD.EjecutarQuerySinRetorno(query, connection);
                return true;
            }
            return false;
        }

        public bool ExistenResenias(SqlConnection connection)
        {
            return AccesoBD.ExistenValoresEnTabla(Constantes.RESENIA, connection);
        }

        public List<Resenia> ObtenerTodasLasResenias(SqlConnection connection)
        {
            List<Resenia> resenias = new List<Resenia>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindAll(Constantes.RESENIA), Constantes.RESENIA, connection);
            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    resenias.Add(ExtraerResenia(row, connection));
                }
            }
            return resenias;
        }

        private Resenia ExtraerResenia(DataRow row, SqlConnection connection)
        {
            return new Resenia(
                    Convert.ToInt32(row[Constantes.ID]),
                    Convert.ToInt32(row[Constantes.PUNTAJE]),
                    Convert.ToString(row[Constantes.RESENIA]),
                    Convert.ToInt32(row[Constantes.USUARIO]),
                    Convert.ToInt32(row.IsNull(Constantes.BANDA_ID) ? 0 : row[Constantes.BANDA_ID]),
                    Convert.ToInt32(row.IsNull(Constantes.CANCION_ID) ? 0 : row[Constantes.CANCION_ID])
                    );
        }
    }
}
