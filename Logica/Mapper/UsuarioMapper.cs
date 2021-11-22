using Persistencia.Entidades;
using Logica.VO;

namespace Logica.Mapper
{
    public class UsuarioMapper
    {

        public static Usuario VOUsuarioToUsuario(VOUsuario usuario, Persona persona)
        {
            return new Usuario(
                    usuario.Id,
                    usuario.NombreUsuario,
                    usuario.Contrasenia,
                    persona
                );
        }

        public static VOUsuario UsuarioToVOUsuario(Usuario usuario, VOPersona persona)
        {
            return new VOUsuario(
                    usuario.Id,
                    usuario.NombreUsuario,
                    usuario.contrasenia,
                    persona,
                    null
                );
        }
    }
}
