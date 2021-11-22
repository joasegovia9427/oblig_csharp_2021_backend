using System;
using System.Collections.Generic;

namespace Logica.VO
{
    public class VOBanda
    {
        public VOBanda()
        {
        }

        public VOBanda(int id, string nombre, int anioCreacion, int anioSeparacion, string genero, List<VOIntegrante> integrantes, string error)
        {
            Id = id;
            Nombre = nombre;
            AnioCreacion = anioCreacion;
            AnioSeparacion = anioSeparacion;
            Genero = genero;
            Integrantes = integrantes;
            Error = error;
        }
        public VOBanda(string nombre, int anioCreacion, int anioSeparacion, string genero, List<VOIntegrante> integrantes, string error)
        {
            Nombre = nombre;
            AnioCreacion = anioCreacion;
            AnioSeparacion = anioSeparacion;
            Genero = genero;
            Integrantes = integrantes;
            Error = error;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int AnioCreacion { get; set; }
        public int AnioSeparacion { get; set; }
        public string Genero { get; set; }
        public List<VOIntegrante> Integrantes { get; set; }
        public string Error { get; set; }

    }
}
