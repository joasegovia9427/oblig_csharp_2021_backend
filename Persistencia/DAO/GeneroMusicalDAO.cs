using System;
using Persistencia.Entidades;
using System.Data;
using System.Collections.Generic;
using Persistencia.Utils;
using System.Data.SqlClient;

namespace Persistencia.DAO
{
    public class GeneroMusicalDAO
    {
        public GeneroMusicalDAO()
        {
        }

        public int InsertarGeneroMusical(string genero, SqlConnection connection)
        {
            if (!this.PerteneceGeneroMusical(genero, connection))
            {
                return AccesoBD.EjecutarQueryRetornoID(
                    String.Format("insert into dbo.GeneroMusical(Nombre) output inserted.Id values('{0}')",
                        genero.ToLower()),
                        connection
                    );
            }
            return 0;
        }
        public bool PerteneceGeneroMusical(int Id, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                QueryBuilder.FindById(Constantes.GENERO_MUSICAL, Id),
                Constantes.GENERO_MUSICAL,
                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public bool BorrarGeneroMusical(int id, SqlConnection connection)
        {
            if (PerteneceGeneroMusical(id, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(QueryBuilder.DeleteById(Constantes.GENERO_MUSICAL, id), connection);
                return true;
            }
            return false;
        }

        public GeneroMusical ObtenerGeneroMusical(string nombre, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindByAttributeString(Constantes.GENERO_MUSICAL, Constantes.NOMBRE, nombre.ToLower()), Constantes.GENERO_MUSICAL, connection);
            if(response.Rows.Count != 0)
            {
                DataRow rowResponse = response.Rows[0];
                return ExtraerGenero(rowResponse, connection);
            }
            return null;
        }

        public GeneroMusical ObtenerGeneroMusical(int Id, SqlConnection connection)
        {
            if (this.PerteneceGeneroMusical(Id, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindById(Constantes.GENERO_MUSICAL, Id),
                    Constantes.GENERO_MUSICAL,
                    connection);
                DataRow rowResponse = response.Rows[0];
                return ExtraerGenero(rowResponse, connection);
            }
            return null;
        }

        public bool PerteneceGeneroMusical(string generoName, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                QueryBuilder.FindByAttributeString(Constantes.GENERO_MUSICAL, Constantes.NOMBRE, generoName.ToLower()),
                Constantes.GENERO_MUSICAL,
                connection);
            
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public bool ModificarGeneroMusical(GeneroMusical genero, SqlConnection connection)
        {
            if (this.PerteneceGeneroMusical(genero.Id, connection))
            {
                string query = "Update GeneroMusical set Nombre = '"+ genero.Nombre.ToLower() +"' WHERE Id = " + genero.Id;
                AccesoBD.EjecutarQuerySinRetorno(query, connection);
                return true;
            }
            return false;
        }

        public bool ExistenGenerosMusicales(SqlConnection connection)
        {
            return AccesoBD.ExistenValoresEnTabla(Constantes.GENERO_MUSICAL, connection);
        }

        public List<GeneroMusical> ObtenerTodosLosGeneros(SqlConnection connection)
        {
            List<GeneroMusical> generosMusicales = new List<GeneroMusical>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindAll(Constantes.GENERO_MUSICAL), Constantes.GENERO_MUSICAL, connection);
            if(response.Rows.Count != 0)
            {
                foreach(DataRow row in response.Rows)
                {
                    generosMusicales.Add(ExtraerGenero(row, connection));
                }
            }
            return generosMusicales;
        }

        private GeneroMusical ExtraerGenero(DataRow row, SqlConnection connection)
        {
            return new GeneroMusical(
                            Convert.ToInt32(row[Constantes.ID]),
                            Convert.ToString(row[Constantes.NOMBRE])
                        );
        }
    }
}
