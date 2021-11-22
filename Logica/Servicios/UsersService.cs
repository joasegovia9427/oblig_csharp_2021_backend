using System;
using System.Data.SqlClient;
using Persistencia.DAO;
using Persistencia.Entidades;
using Logica.VO;
using Logica.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace Logica.Servicios
{
    public class UsersService
    {

        private PersonasDAO daoPersona;
        private UsuarioDAO daoUsuario;

        public UsersService()
        {
            daoPersona = new PersonasDAO();
            daoUsuario = new UsuarioDAO(daoPersona);
        }

        public Boolean login(string nombreUsuario, string password, SqlConnection connection)
        {
            return daoUsuario.PerteneceUsuario(nombreUsuario, password, connection);
        }

        public bool UsuarioAlta(VOUsuario usuario, SqlConnection connection)
        {
            if (daoUsuario.PerteneceUsuario(usuario.NombreUsuario, connection))
            {
                return false;
            }

            Persona persona = null;

            if (usuario.Persona != null)
            {
                if(usuario.Persona.Id != 0)
                {
                    persona = daoPersona.ObtenerPersona(usuario.Persona.Id, connection);
                }
                
                if(persona == null && usuario.Persona.Nombre != null && usuario.Persona.Apellido != null)
                {
                    persona = daoPersona.ObtenerPersona(usuario.Persona.Nombre, usuario.Persona.Apellido, connection);
                    if (persona == null)
                    {
                        int personaId = daoPersona.InsertarPersona(new Persona(0, usuario.Persona.Nombre, usuario.Persona.Apellido), connection);
                        persona = new Persona(personaId, usuario.Persona.Nombre, usuario.Persona.Apellido);
                    }
                }

                if (persona != null && daoUsuario.InsertarUsuario(UsuarioMapper.VOUsuarioToUsuario(usuario, persona), connection) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool UsuarioBaja(string username, SqlConnection connection)
        {
            return daoUsuario.BorrarUsuario(username, connection);
        }

        public VOUsuario UsuarioObtener(int Id, SqlConnection connection)
        {
            if (daoUsuario.PerteneceUsuario(Id, connection))
            {
                Usuario usuario = daoUsuario.ObtenerUsuario(Id, connection);
                return UsuarioMapper.UsuarioToVOUsuario(
                    usuario,
                    new VOPersona(
                        usuario.Persona.Id,
                        usuario.Persona.Nombre,
                        usuario.Persona.Apellido,
                        null
                        )
                    );
            }
            return null;
        }

        public List<VOUsuario> UsuariosListado(SqlConnection connection)
        {
            List<Usuario> usuarios = daoUsuario.ObtenerTodosLosUsuarios(connection);
            List<VOUsuario> vOUsuarios = new List<VOUsuario>();
            usuarios.ForEach(usr =>
            {
                Persona persona = daoPersona.ObtenerPersona(usr.Persona.Nombre, usr.Persona.Apellido, connection);
                vOUsuarios.Add(UsuarioMapper.UsuarioToVOUsuario(usr, new VOPersona(persona.Id, persona.Nombre, persona.Apellido, null)));
            });

            return vOUsuarios;
        }

        public int UsuarioIdentifier(string username, SqlConnection connection)
        {
            Usuario usuario = daoUsuario.ObtenerUsuario(username, connection);
            if(usuario != null)
            {
                return usuario.Id;
            }
            return 0;
        }

    }
}
