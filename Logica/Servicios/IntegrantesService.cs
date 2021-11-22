using System;
using System.Data.SqlClient;
using Persistencia.DAO;
using Persistencia.Entidades;
using Logica.VO;
using System.Collections.Generic;
using Logica.Mapper;

namespace Logica.Servicios
{
    public class IntegrantesService
    {
        private PersonasDAO daoPersonas;
        private BandaDAO daoBanda;
        private GeneroMusicalDAO daoGeneroMusical;
        private IntegranteDAO daoIntegrantes;
        public IntegrantesService()
        {
            daoGeneroMusical = new GeneroMusicalDAO();
            daoPersonas = new PersonasDAO();
            daoBanda = new BandaDAO(daoGeneroMusical);
            daoIntegrantes = new IntegranteDAO(daoPersonas, daoBanda);
        }

        public List<VOIntegrante> IntegrantesListado(SqlConnection connection)
        {
            List<VOIntegrante> response = new List<VOIntegrante>();
            foreach(Integrante integrante in daoIntegrantes.ObtenerTodosLosIntegrantes(connection))
            {
                response.Add(IntegranteMapper.IntegranteToVOIntegrante(integrante));
            }
            return response;
        }

        public bool IntegranteAlta(VOIntegrante integrante, SqlConnection connection)
        {
            if(integrante == null)
            {
                return false;
            }
            Persona persona = daoPersonas.ObtenerPersona(integrante.Persona.Nombre, integrante.Persona.Apellido, connection);
            if (persona == null)
            {
                int personaNueva = daoPersonas.InsertarPersona(new Persona(integrante.Persona.Nombre, integrante.Persona.Apellido), connection);
                persona = daoPersonas.ObtenerPersona(personaNueva, connection);
            }
            Banda banda = null;
            if (integrante.BandaId != 0)
            {
                banda = daoBanda.ObtenerBanda(integrante.BandaId, connection);
                if (banda == null)
                {
                    Console.WriteLine("No se puede encontrar la Banda a la que pertenece el integrante");
                    return false;
                }
                bool yaExiste = false;
                daoIntegrantes.ObtenerIntegrantesBanda(integrante.BandaId, connection).ForEach(i =>
                {
                    if (i.Persona.Id == integrante.Persona.Id)
                    {
                        yaExiste = true;
                    }
                });
                if (yaExiste) return yaExiste;
            }

            int nuevoIntegranteId = daoIntegrantes.InsertarIntegrante(IntegranteMapper.VOIntegranteToIntegrante(integrante, persona, banda), connection);

            return nuevoIntegranteId != 0;
        }

        public bool IntegranteBaja(int Id, SqlConnection connection)
        {
            if (Id == 0)
            {
                return false;
            }
            return daoIntegrantes.BorrarIntegrante(Id, connection);
        }

        public bool IntegranteModificacion(VOIntegrante integrante, SqlConnection connection)
        {
            if (integrante == null)
            {
                return false;
            }
            Integrante integranteBase = daoIntegrantes.ObtenerIntegrante(integrante.Id, connection);
            if (integranteBase == null)
            {
                Console.WriteLine("No se puede encontrar el integrante a modificar");
                return false;
            }

            Banda banda = null;
            if (integrante.BandaId != 0)
            {
                banda = daoBanda.ObtenerBanda(integrante.BandaId, connection);
                if (banda == null)
                {
                    Console.WriteLine("No se puede encontrar la Banda a la que pertenece el integrante");
                    return false;
                }
            } 

            Persona persona = daoPersonas.ObtenerPersona(integrante.Persona.Id, connection);
            if (persona == null)
            {
                int personaNueva = daoPersonas.InsertarPersona(new Persona(integrante.Persona.Nombre, integrante.Persona.Apellido), connection);
                persona = daoPersonas.ObtenerPersona(personaNueva, connection);
            }

            return daoIntegrantes.ModificarIntegrante(IntegranteMapper.VOIntegranteToIntegrante(integrante, persona, banda), connection);

        }

        public VOIntegrante IntegranteObtener(int Id, SqlConnection connection)
        {
            if (Id == 0)
            {
                return null;
            }
            Integrante integrante = daoIntegrantes.ObtenerIntegrante(Id, connection);
            if(integrante != null)
            {
                return IntegranteMapper.IntegranteToVOIntegrante(integrante);
            }
            return null;
        }
    }
}
