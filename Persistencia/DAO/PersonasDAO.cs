using System;
using System.Data;
using System.Collections.Generic;
using Persistencia.Entidades;
using Persistencia.Utils;
using System.Data.SqlClient;

namespace Persistencia.DAO
{
    public class PersonasDAO
    {
        public PersonasDAO()
        {
        }
    
        public bool PertenecePersona(int Id, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                QueryBuilder.FindById(Constantes.PERSONA, Id),
                Constantes.PERSONA,
                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public int InsertarPersona(Persona persona, SqlConnection connection)
        {
            if (!PertenecePersona(persona.Id, connection))
            {
                return AccesoBD.EjecutarQueryRetornoID(
                    String.Format(
                        "insert into dbo.Persona(Nombre, Apellido) output inserted.Id values('{0}', '{1}')", 
                        persona.Nombre, 
                        persona.Apellido
                        ), 
                    connection);
            }
            return 0;
        }

        public void BorrarPersona(int Id, SqlConnection connection)
        {
            if (PertenecePersona(Id, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(
                    QueryBuilder.DeleteById(
                        Constantes.PERSONA, 
                        Id), 
                    connection);
            }
        }

        public Persona ObtenerPersona(int Id, SqlConnection connection)
        {
            if (PertenecePersona(Id, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindById(Constantes.PERSONA, Id),
                    Constantes.PERSONA,
                    connection
                    );
                return ExtraerPersona(response.Rows[0]);
            }
            return null;
        }

        public Persona ObtenerPersona(string nombre, string apellido, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                String.Format(
                    "Select * from Persona WHERE Nombre ='{0}' AND Apellido='{1}'", 
                    nombre, 
                    apellido),
                Constantes.PERSONA,
                connection
                );

            if (response.Rows.Count != 0)
            {
                return ExtraerPersona(response.Rows[0]);
            }
            return null;
        }



        public void ModificarPersona(Persona persona, SqlConnection connection)
        {

            if (PertenecePersona(persona.Id, connection))
            {
                string query = String.Format("UPDATE Personas set Nombre = '{0}', Apellido = '{1}' WHERE Id ={2}", 
                    persona.Nombre, 
                    persona.Apellido, 
                    persona.Id
                    );
                AccesoBD.EjecutarQuerySinRetorno(query, connection);
            }
        }

        public bool ExistenPersonas(SqlConnection connection)
        {
            return AccesoBD.ExistenValoresEnTabla(Constantes.PERSONA, connection);
        }

        public List<Persona> ObtenerTodasLasPersonas(SqlConnection connection)
        {
            List<Persona> personas = new List<Persona>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindAll(Constantes.PERSONA), Constantes.PERSONA, connection);
            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    personas.Add(ExtraerPersona(row));
                }
            }
            return personas;
        }

        private Persona ExtraerPersona(DataRow row)
        {
            return new Persona(
                Convert.ToInt32(row[Constantes.ID]),
                Convert.ToString(row[Constantes.NOMBRE]),
                Convert.ToString(row[Constantes.APELLIDO])
                );
        }
    }
}
