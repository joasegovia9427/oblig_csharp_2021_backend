using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Logica.VO;
using Persistencia.DAO;
using Persistencia.Entidades;
using Logica.Mapper;
using System.Linq;

namespace Logica.Servicios
{
    public class BandasService
    {
        private GeneroMusicalDAO daoGeneroMusical;

        private PersonasDAO daoPersona;

        private BandaDAO daoBanda;

        private IntegranteDAO daoIntegrante;

        public BandasService()
        {
            daoGeneroMusical = new GeneroMusicalDAO();
            daoPersona = new PersonasDAO();
            daoBanda = new BandaDAO(daoGeneroMusical);
            daoIntegrante = new IntegranteDAO(daoPersona, daoBanda);
        }

        public VOBanda obtenerBanda(int Id, SqlConnection connection)
        {
            VOBanda response = new VOBanda();
            Banda banda = daoBanda.ObtenerBanda(Id, connection);
            if(banda != null)
            {
                List<VOIntegrante> integrantesBanda = this.obtenerIntegrantesBanda(daoIntegrante.ObtenerIntegrantesBanda(Id, connection), connection);
                response.Id = banda.Id;
                response.Nombre = banda.Nombre;
                response.AnioCreacion = banda.AnioCreacion;
                response.AnioSeparacion = banda.AnioSeparacion;
                response.Genero = banda.GeneroMusical.Nombre;
                response.Integrantes = integrantesBanda;
            }

            return response;
        }

        public List<VOBanda> ObtenerTodasLasBandas(SqlConnection connection)
        {
            List<VOBanda> bandas = new List<VOBanda>();
            List<Banda> bandasBase = daoBanda.ObtenerTodasLasBandas(connection);
            foreach (Banda b in bandasBase)
            {
                bandas.Add(new VOBanda(
                    b.Id,
                    b.Nombre,
                    b.AnioCreacion,
                    b.AnioSeparacion,
                    b.GeneroMusical.Nombre,
                    this.obtenerIntegrantesBanda(daoIntegrante.ObtenerIntegrantesBanda(b.Id, connection), connection),
                    null
                    )
                );
            }

            return bandas;
        }

        public bool AltaDeBanda(VOBanda banda, SqlConnection connection)
        {
            if(banda.Id != 0 && daoBanda.PerteneceBanda(banda.Id, connection))
            {
                Console.WriteLine("La banda ID: " + banda.Id + " ya existe en el sistema, no se puede insertar.");
                return false;
            }
            GeneroMusical genero = daoGeneroMusical.ObtenerGeneroMusical(banda.Genero, connection);
            if(genero == null)
            {
                int generoNuevo = daoGeneroMusical.InsertarGeneroMusical(banda.Genero, connection);
                genero = daoGeneroMusical.ObtenerGeneroMusical(generoNuevo, connection);
            }

            Banda bandaBase = BandaMapper.VOBandaToBanda(banda, genero);
            bandaBase.Id = daoBanda.InsertarBanda(bandaBase, connection);

            foreach (VOIntegrante integrante in banda.Integrantes)
            {
                Persona persona = daoPersona.ObtenerPersona(integrante.Persona.Id, connection);

                if (persona == null)
                {
                    int nuevaPersonaId = daoPersona.InsertarPersona(new Persona(integrante.Persona.Nombre, integrante.Persona.Apellido), connection);
                    persona = daoPersona.ObtenerPersona(nuevaPersonaId, connection);
                }

                int integranteId = daoIntegrante.InsertarIntegrante(IntegranteMapper.VOIntegranteToIntegrante(integrante, persona, bandaBase), connection);
                if (integranteId == 0)
                {
                    daoPersona.BorrarPersona(persona.Id, connection);
                    daoBanda.BorrarBanda(bandaBase.Id, connection);
                    Console.WriteLine("Error - AltaDeBanda : No se pudieron guardar los integrantes de la banda");
                    return false;
                }
            }
            return true;
        }

        public bool BajaDeBanda(int Id, SqlConnection connection)
        {
            Banda bandaBase = daoBanda.ObtenerBanda(Id, connection);

            if (bandaBase != null)
            {
                List<Integrante> integrantes = daoIntegrante.ObtenerIntegrantesBanda(Id, connection);
                if(integrantes.Count != 0)
                {
                    foreach(Integrante i in integrantes)
                    {
                        if (!daoIntegrante.BorrarIntegrante(i.Id, connection))
                        {
                            Console.WriteLine("No se puede eliminar el integrante NRO:" + i.Id + " de la base.");
                            return false;
                        }
                    }
                }
                return daoBanda.BorrarBanda(bandaBase.Id, connection);
            }
            Console.WriteLine("Error al eliminar banda ID: " + Id + ".");
            return false;
        }

        public bool ModificarBanda(VOBanda banda, SqlConnection connection)
        {
            if (!daoBanda.PerteneceBanda(banda.Id, connection))
            {
                Console.WriteLine("La banda ID: " + banda.Id + " no existe en el sistema, no se puede modificar.");
                return false;
            }

            Banda bandaBase = daoBanda.ObtenerBanda(banda.Id, connection);
            bandaBase.Nombre = (banda.Nombre == null) ? bandaBase.Nombre : banda.Nombre;
            bandaBase.GeneroMusical = (banda.Genero == null)? bandaBase.GeneroMusical : daoGeneroMusical.ObtenerGeneroMusical(banda.Genero, connection);
            bandaBase.AnioCreacion = (banda.AnioCreacion == 0)? bandaBase.AnioCreacion : banda.AnioCreacion;
            bandaBase.AnioSeparacion = (banda.AnioSeparacion == 0)? bandaBase.AnioSeparacion : banda.AnioSeparacion;
            if(!this.actualizaIntegrantesBanda(
                daoIntegrante.ObtenerIntegrantesBanda(banda.Id, connection).ToDictionary(
                    i => i.Persona.Nombre + " " + i.Persona.Apellido, i => i), 
                banda.Integrantes,
                bandaBase,
                connection
                ))
            {
                Console.WriteLine("ERROR - modificarBanda - No se pudo modificar los integrantes de la banda");
                return false;
            }
            if (!daoBanda.ModificarBanda(bandaBase, connection))
            {
                Console.WriteLine("ERROR - modificarBanda - No se pudo modificar los datos de la banda");
                return false;
            }

            return true;
            
        }

        public List<VOIntegrante> BandaIntegrantes(int Id, SqlConnection connection)
        {
            List<VOIntegrante> vOIntegrantes = new List<VOIntegrante>();
            List<Integrante> integrantes = daoIntegrante.ObtenerIntegrantesBanda(Id, connection);
            if (integrantes != null && integrantes.Count != 0)
            {
                integrantes.ForEach(i => vOIntegrantes.Add(IntegranteMapper.IntegranteToVOIntegrante(i)));
            }
            return vOIntegrantes;
        }

        public bool IntegranteAltaEnBanda(int BandaId, int IntegranteId, SqlConnection connection)
        {
            Integrante integrante = daoIntegrante.ObtenerIntegrante(IntegranteId, connection);
            if(integrante == null)
            {
                Console.WriteLine("El integrante ingresado no existe en el sistema.");
                return false;
            }

            Banda banda = daoBanda.ObtenerBanda(BandaId, connection);
            if(banda == null)
            {
                Console.WriteLine("La banda ingresada no existe en el sistema.");
                return false;
            }
            integrante.Banda = banda;

            return daoIntegrante.ModificarIntegrante(integrante, connection);
        }

        public bool IntegranteBajaEnBanda(int IntegranteId, System.Data.SqlClient.SqlConnection connection)
        {
            Integrante integrante = daoIntegrante.ObtenerIntegrante(IntegranteId, connection);
            if (integrante == null)
            {
                Console.WriteLine("El integrante ingresado no existe en el sistema.");
                return false;
            }
            integrante.Banda = null;
            return daoIntegrante.ModificarIntegrante(integrante, connection);
        }

        private bool actualizaIntegrantesBanda(Dictionary<string, Integrante> integrantesBase, List<VOIntegrante> nuevosIntegrantes, Banda banda, SqlConnection connection)
        {
            foreach(VOIntegrante i in nuevosIntegrantes)
            {
                string key = i.Persona.Nombre + " " + i.Persona.Apellido;
                if (integrantesBase.ContainsKey(key))
                {
                    Integrante integrante = integrantesBase[key];
                    integrante.FechaNacimiento = i.FechaNacimiento;
                    integrante.Foto = i.Foto;
                    if (!daoIntegrante.ModificarIntegrante(integrante, connection))
                    {
                        Console.WriteLine("No se pudo modificar un integrante en la Banda ID: " + banda.Id);
                        return false;
                    }
                    integrantesBase.Remove(key);
                }
                else
                {
                    Persona persona = daoPersona.ObtenerPersona(i.Persona.Id, connection);
                    Integrante nuevoIntegrante = new Integrante(
                        i.FechaNacimiento,
                        i.Foto,
                        (persona == null) ? new Persona(i.Persona.Nombre, i.Persona.Apellido) : persona,
                        banda);
                    
                    if(daoIntegrante.InsertarIntegrante(nuevoIntegrante, connection) == 0)
                    {
                        Console.WriteLine("No se pudo ingresar el nuevo integrante a la Banda ID: " + banda.Id);
                        return false;
                    }
                }
            }

            if(integrantesBase.Count != 0)
            {
                foreach(KeyValuePair<string, Integrante> entry in integrantesBase)
                {
                    if(!daoIntegrante.BorrarIntegrante(entry.Value.Id, connection))
                    {
                        Console.WriteLine("No se puede eliminar el integrante NRO:" + entry.Value.Id + " de la base.");
                        return false;
                    }
                }
            }
            return true;
        }

        public List<VOIntegrante> obtenerIntegrantesBanda(List<Integrante> integrantes, SqlConnection connection)
        {
            List<VOIntegrante> integrantesBanda = new List<VOIntegrante>();
            if (integrantes.Count != 0)
            {
                foreach (Integrante i in integrantes)
                {
                    integrantesBanda.Add(
                        new VOIntegrante(i.Id, i.FechaNacimiento, i.Foto,
                            new VOPersona(i.Persona.Nombre, i.Persona.Apellido, null),
                            i.Banda.Id,
                            null
                            )
                        );
                }
            }
            return integrantesBanda;
        }
    }
}
