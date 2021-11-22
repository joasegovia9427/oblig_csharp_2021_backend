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
    public class ReseniasService
    {
        private ReseniaDAO daoResenia;

        private BandaDAO daoBanda;

        private UsuarioDAO daoUsuario;

        private CancionDAO daoCancion;

        public ReseniasService()
        {
            PersonasDAO daoPersonas = new PersonasDAO();
            GeneroMusicalDAO daoGenero = new GeneroMusicalDAO();
            daoResenia = new ReseniaDAO();
            daoBanda = new BandaDAO(daoGenero);
            IntegranteDAO daoIntegrantes = new IntegranteDAO(daoPersonas, daoBanda);
            daoUsuario = new UsuarioDAO(daoPersonas);
            daoCancion = new CancionDAO(daoIntegrantes, daoGenero);
        }

        public List<VOResenia> ReseniaListado(SqlConnection connection)
        {
            List<Resenia> resenias = daoResenia.ObtenerTodasLasResenias(connection);
            List<VOResenia> vOResenias = new List<VOResenia>();
            foreach(Resenia r in resenias)
            {
                vOResenias.Add(ReseniaMapper.ReseniaToVOResenia(r));
            }
            return vOResenias;
        }

        public bool ReseniaAlta(VOResenia resenia, SqlConnection connection)
        {
            if (this.reseniaIdsChecker(resenia, connection))
            {
                if(daoResenia.InsertarResenia(ReseniaMapper.VOReseniaToResenia(resenia), connection) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ReseniaBaja(int Id, SqlConnection connection)
        {
            if(!daoResenia.PerteneceResenia(Id, connection))
            {
                return false;
            }
            return daoResenia.BorrarResenia(Id, connection);
        }

        public bool ReseniaModificacion(VOResenia resenia, SqlConnection connection)
        {
            if (this.reseniaIdsChecker(resenia, connection))
            {
                return daoResenia.ModificarResenia(ReseniaMapper.VOReseniaToResenia(resenia), connection);
            }
            return false;
        }

        public VOResenia ReseniaObtener(int Id, SqlConnection connection)
        {
            Resenia resenia = daoResenia.ObtenerResenia(Id, connection);
            if(resenia != null)
            {
                return ReseniaMapper.ReseniaToVOResenia(resenia);
            }
            return null;
        }

        private bool reseniaIdsChecker(VOResenia resenia, SqlConnection connection)
        {
            if (resenia.UsuarioId == 0)
            {
                return false;
            }
            else
            {
                if(!daoUsuario.PerteneceUsuario(resenia.UsuarioId, connection))
                {
                    return false;
                }
            }

            if (resenia.BandaId != 0)
            {
                if (!daoBanda.PerteneceBanda(resenia.BandaId, connection) || resenia.CancionId != 0)
                {
                    return false;
                }
            }
            if (resenia.CancionId != 0)
            {
                if (!daoCancion.PerteneceCancion(resenia.CancionId, connection))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
