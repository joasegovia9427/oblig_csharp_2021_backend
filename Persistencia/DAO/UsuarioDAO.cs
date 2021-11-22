using System;
using System.Data;
using Persistencia.Utils;
using Persistencia.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Persistencia.DAO
{
    public class UsuarioDAO
    {
        private PersonasDAO personasDAO;

        public UsuarioDAO(PersonasDAO daoPersonas)
        {
            personasDAO = daoPersonas;
        }

        public bool PerteneceUsuario(int Id, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindById(Constantes.USUARIO, Id),
                                Constantes.USUARIO,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public bool PerteneceUsuario(string usuarioNombre, SqlConnection connection)
        {
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                QueryBuilder.FindByAttributeString(Constantes.USUARIO, Constantes.NOMBRE_USUARIO, usuarioNombre),
                                Constantes.USUARIO,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public bool PerteneceUsuario(string usuarioNombre, string password, SqlConnection connection)
        {
            string query = String.Format("SELECT * from Usuario WHERE NombreUsuario = '{0}' AND Contrasenia = '{1}'", usuarioNombre, password);
            DataTable response = AccesoBD.EjecutarQueryConRetorno(
                                query,
                                Constantes.USUARIO,
                                connection);
            if (response.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }

        public int InsertarUsuario(Usuario usuario, SqlConnection connection)
        {
            if (!this.PerteneceUsuario(usuario.NombreUsuario, connection))
            {
                string query = String.Format("insert into dbo.Usuario(NombreUsuario, Contrasenia, PersonaId) " +
                                    " output inserted.Id values('{0}', '{1}', {2})",
                                    usuario.NombreUsuario,
                                    usuario.contrasenia,
                                    usuario.Persona.Id
                                    );

                return AccesoBD.EjecutarQueryRetornoID(query, connection);
            }
            return 0;
        }

        public bool BorrarUsuario(int Id, SqlConnection connection)
        {
            if (this.PerteneceUsuario(Id, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(QueryBuilder.DeleteById(Constantes.USUARIO, Id), connection);
                return true;
            }
            return false;
        }

        public bool BorrarUsuario(string username, SqlConnection connection)
        {
            if (this.PerteneceUsuario(username, connection))
            {
                AccesoBD.EjecutarQuerySinRetorno(
                    String.Format("DELETE from Usuario WHERE NombreUsuario ='{0}'", 
                    username),
                    connection
                    );
                return true;
            }
            return false;
        }


        public Usuario ObtenerUsuario(int Id, SqlConnection connection)
        {
            if (this.PerteneceUsuario(Id, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindById(Constantes.USUARIO, Id),
                    Constantes.USUARIO,
                    connection
                    );
                DataRow row = response.Rows[0];

                return ExtraerUsuario(row, connection);
            }
            return null;
        }

        public Usuario ObtenerUsuario(string username, SqlConnection connection)
        {
            if (this.PerteneceUsuario(username, connection))
            {
                DataTable response = AccesoBD.EjecutarQueryConRetorno(
                    QueryBuilder.FindByAttributeString(Constantes.USUARIO, Constantes.NOMBRE_USUARIO, username),
                    Constantes.USUARIO,
                    connection
                    );
                DataRow row = response.Rows[0];

                return ExtraerUsuario(row, connection);
            }
            return null;
        }

        public void ModificarUsuario(Usuario usuario, SqlConnection connection)
        {
            if (this.PerteneceUsuario(usuario.Id, connection))
            {
                string query = String.Format("UPDATE Usuario set NombreUsuario = '{0}', Contrasenia = '{1}', PersonaID = {2} WHERE Id = {3}", 
                    usuario.NombreUsuario,
                    usuario.contrasenia,
                    usuario.Persona.Id,
                    usuario.Id);
                AccesoBD.EjecutarQuerySinRetorno(query, connection);
            }
        }

        public bool ExistenUsuarios(SqlConnection connection)
        {
            return AccesoBD.ExistenValoresEnTabla(Constantes.USUARIO, connection);
        }

        public List<Usuario> ObtenerTodosLosUsuarios(SqlConnection connection)
        {
            List<Usuario> usuarios = new List<Usuario>();
            DataTable response = AccesoBD.EjecutarQueryConRetorno(QueryBuilder.FindAll(Constantes.USUARIO), Constantes.USUARIO, connection);
            if (response.Rows.Count != 0)
            {
                foreach (DataRow row in response.Rows)
                {
                    usuarios.Add(ExtraerUsuario(row, connection));
                }
            }
            return usuarios;
        }

        private Usuario ExtraerUsuario(DataRow row, SqlConnection connection)
        {
            return new Usuario(
                Convert.ToInt32(row[Constantes.ID]),
                Convert.ToString(row[Constantes.NOMBRE_USUARIO]),
                Convert.ToString(row[Constantes.CONTRASENIA]),
                personasDAO.ObtenerPersona(Convert.ToInt32(row[Constantes.PERSONA_ID]), connection)
               );
        }
    }
}

