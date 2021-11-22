using System;
using System.Data.SqlClient;
using Persistencia.DAO;
using Persistencia.Entidades;
using Logica.VO;
using System.Collections.Generic;

namespace Logica.Servicios
{
    public class GeneroService
    {
        private GeneroMusicalDAO daoGeneroMusical;

        public GeneroService()
        {
            daoGeneroMusical = new GeneroMusicalDAO();
        }

        public bool GeneroMusicalAlta(string genero, SqlConnection connection)
        {
            if(genero == null){
                return false;
            }
            return daoGeneroMusical.InsertarGeneroMusical(genero, connection) != 0;
        }

        public bool GeneroMusicalModificacion(int Id, string genero, SqlConnection connection)
        {
            if (genero == null || Id == 0)
            {
                return false;
            }
            GeneroMusical generoMusical = daoGeneroMusical.ObtenerGeneroMusical(Id, connection);
            if (generoMusical != null)
            {
                generoMusical.Nombre = genero;
                return daoGeneroMusical.ModificarGeneroMusical(generoMusical, connection);
            }
            return false;
        }

        public bool GeneroMusicalBaja(int Id, SqlConnection connection)
        {
            if (Id == 0)
            {
                return false;
            }
            return daoGeneroMusical.BorrarGeneroMusical(Id, connection);
        }

        public List<VOGeneroMusical> GeneroMusicalListado(SqlConnection connection)
        {
            List<VOGeneroMusical> response = new List<VOGeneroMusical>();
            List<GeneroMusical> generosMusicales = daoGeneroMusical.ObtenerTodosLosGeneros(connection);
            foreach(GeneroMusical genero in generosMusicales)
            {
                response.Add(new VOGeneroMusical(genero.Id, genero.Nombre, null));
            }
            return response;
        }
    }
}
