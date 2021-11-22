using System;
using System.Data;
using System.Collections.Generic;
using Persistencia.Utils;
using Persistencia.Entidades;
using System.Data.SqlClient;

namespace Persistencia.DAO
{
    public class IntegranteDAO
    {
        private PersonasDAO personasDAO;

        private BandaDAO bandaDAO;

        public IntegranteDAO(PersonasDAO daoPersonas, BandaDAO daoBanda)
        {
            personasDAO = daoPersonas;
            bandaDAO = daoBanda;
        }

        public bool PerteneceIntegrante(int Id, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindById(Constantes.INTEGRANTE, Id),
                                Constantes.INTEGRANTE,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public int InsertarIntegrante(Integrante Integrante, SqlConnection connection)
        {
            int bandaId = Integrante.Banda == null ? 0 : Integrante.Banda.Id;

            string query = String.Format("insert into dbo.Integrante(FechaNacimiento, Foto, PersonaId, BandaId)  output inserted.Id " +
                                "values('{0}', '{1}', {2}, {3})", 
                                Formateador.FormatearFecha(Integrante.FechaNacimiento),
                                Integrante.Foto,
                                Integrante.Persona.Id,
                                bandaId
                                );

            return AccesoBD.EjecutarQueryRetornoID(query, connection);
        }

        public bool BorrarIntegrante(int Id, SqlConnection connection)
        {
            if (this.PerteneceIntegrante(Id, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(QueryBuilder.DeleteById(Constantes.INTEGRANTE, Id), connection);
                return true;
            }
            return false;

        }

        public Integrante ObtenerIntegrante(int Id, SqlConnection connection)
        {
            if (this.PerteneceIntegrante(Id, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindById(Constantes.INTEGRANTE, Id),
                    Constantes.INTEGRANTE,
                    connection
                    );
                DataRow row = response.Rows[0];

                return ExtraerIntegrante(row, connection);
            }
            return null;
        }


        public bool ModificarIntegrante(Integrante integrante, SqlConnection connection)
        {
            if (this.PerteneceIntegrante(integrante.Id, connection))
            {
                int bandaId = integrante.Banda == null ? 0 : integrante.Banda.Id;
                string query = String.Format("UPDATE Integrante set FechaNacimiento = '{0}', Foto = '{1}', PersonaId = {2}, BandaId = {3} WHERE Id = {4}", 
                    Formateador.FormatearFecha(integrante.FechaNacimiento),
                    integrante.Foto,
                    integrante.Persona.Id,
                    bandaId,
                    integrante.Id
                    );
                AccesoBD.EjecutarQuerySinRetorno(query, connection);
                return true;
            }
            return false;
        }

        public bool ExistenIntegrantes(SqlConnection connection)
        {
            return AccesoBD.ExistenValoresEnTabla(Constantes.INTEGRANTE, connection);
        }

        public List<Integrante> ObtenerTodosLosIntegrantes(SqlConnection connection)
        {
            List<Integrante> integrantes = new List<Integrante>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindAll(Constantes.INTEGRANTE), Constantes.INTEGRANTE, connection);
            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    integrantes.Add(ExtraerIntegrante(row, connection));
                }
            }
            return integrantes;
        }

        public List<Integrante> ObtenerIntegrantesBanda(int bandaId, SqlConnection connection)
        {
            List<Integrante> integrantes = new List<Integrante>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                QueryBuilder.FindByAttributeInt(Constantes.INTEGRANTE, Constantes.BANDA_ID, bandaId),
                Constantes.INTEGRANTE,
                connection);
            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    integrantes.Add(ExtraerIntegrante(row, connection));
                }
            }
            return integrantes;
        }

        private Integrante ExtraerIntegrante(DataRow row, SqlConnection connection)
        {
            return new Integrante(
                Convert.ToInt32(row[Constantes.ID]),
                Convert.ToDateTime(row[Constantes.FECHA_NACIMIENTO]),
                Convert.ToString(row[Constantes.FOTO]),
                personasDAO.ObtenerPersona(Convert.ToInt32(row[Constantes.PERSONA_ID]), connection),
                bandaDAO.ObtenerBanda(Convert.ToInt32(row[Constantes.BANDA_ID]), connection)
                );
        }
    }
}
