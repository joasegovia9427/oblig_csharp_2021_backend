using System;
using Logica;
using Persistencia;
using Persistencia.DAO;
using Persistencia.Entidades;
using System.Collections.Generic;
using Logica.EntryPoint;
using Logica.VO;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Facade fachada = Facade.Instance;
            List<VOGeneroMusical> vOGeneroMusicals = fachada.GeneroMusicalListado();
            List<VOCancion> vOCancions = fachada.CancionListado();
            Console.ReadLine();
        }

        private static void cargaGenerosMusicales()
        {
            Facade fachada = Facade.Instance;
            Console.WriteLine("Insertando Generos musicales");
            fachada.GeneroMusicalAlta("Rock");
            fachada.GeneroMusicalAlta("Salsa");
            fachada.GeneroMusicalAlta("Cumbia");
            fachada.GeneroMusicalAlta("Blues");
            fachada.GeneroMusicalAlta("Merengue");

            List<VOGeneroMusical> generos = fachada.GeneroMusicalListado();
            foreach (VOGeneroMusical genero in generos)
            {
                Console.WriteLine(genero.genero);
            }
            Console.ReadLine();
        }

        private static void cargaBandas()
        {
            Facade fachada = Facade.Instance;

            VOPersona persona1 = new VOPersona("Cristian", "Cary", null);
            VOIntegrante integrante1 = new VOIntegrante(DateTime.Now, "", persona1, 0, null);
            List<VOIntegrante> integrantes1 = new List<VOIntegrante>();
            integrantes1.Add(integrante1);
            VOBanda banda1 = new VOBanda("Triple Nelson", 2001, 0, "Rock", integrantes1, null);
       
            VOPersona persona2 = new VOPersona("John", "Lennon", null);
            VOIntegrante integrante2 = new VOIntegrante(DateTime.Now, "", persona2, 0, null);
            List<VOIntegrante> integrantes2 = new List<VOIntegrante>();
            integrantes2.Add(integrante2);
            VOBanda banda2 = new VOBanda("Joko", 2002, 0, "Rock", integrantes2, null);
            
            VOPersona persona3 = new VOPersona("Pipo", "Pescador", null);
            VOIntegrante integrante3 = new VOIntegrante(DateTime.Now, "", persona3, 0, null);
            List<VOIntegrante> integrantes3 = new List<VOIntegrante>();
            integrantes3.Add(integrante3);
            VOBanda banda3 = new VOBanda("Mouse", 2003, 2010, "Salsa", integrantes3, null);
            
            VOPersona persona4 = new VOPersona("Viejo", "Medina", null);
            VOIntegrante integrante4 = new VOIntegrante(DateTime.Now, "", persona4, 0, null);
            VOIntegrante integrante5 = new VOIntegrante(DateTime.Now, "", persona1, 0, null);
            List<VOIntegrante> integrantes4 = new List<VOIntegrante>();
            integrantes4.Add(integrante4);
            integrantes4.Add(integrante5);
            VOBanda banda4 = new VOBanda("Viejos canticos", 2004, 2020, "Folklore", integrantes4, null);
            
            Console.WriteLine("Insertando Bandas");

            fachada.altaDeBanda(banda1);
            fachada.altaDeBanda(banda2);
            fachada.altaDeBanda(banda3);
            fachada.altaDeBanda(banda4);

            Console.ReadLine();
        }

        private static void CargaCanciones()
        {
            Facade fachada = Facade.Instance;
            VOCancion cancion1 = new VOCancion("The office", 1.8f, 2010, "", 1, "rock", null);
            cancion1.IntegranteId = 1;
            VOCancion cancion2 = new VOCancion("What Else", 5.0f, 2000, "", 2, "salsa", null);
            cancion2.IntegranteId = 2;
            VOCancion cancion3 = new VOCancion("Groaning", 3.3f, 1990, "", 3, "rock", null);
            cancion3.IntegranteId = 3;
            VOCancion cancion4 = new VOCancion("Cheerful", 5.3f, 2012, "", 4, "salsa", null);
            cancion4.IntegranteId = 4;
            VOCancion cancion5 = new VOCancion("Starring", 2.3f, 2018, "", 5, "rock", null);
            cancion5.IntegranteId = 5;
            VOCancion cancion6 = new VOCancion("Conversations", 1.3f, 2012, "", 6, "salsa", null);
            cancion6.IntegranteId = 6;
            VOCancion cancion7 = new VOCancion("Number two", 1.5f, 2021, "", 9, "rock", null);
            cancion7.IntegranteId = 9;


            Console.WriteLine("Insertando Canciones");
            fachada.CancionAlta(cancion1);
            fachada.CancionAlta(cancion2);
            fachada.CancionAlta(cancion3);
            fachada.CancionAlta(cancion4);
            fachada.CancionAlta(cancion5);
            fachada.CancionAlta(cancion6);
            fachada.CancionAlta(cancion7);
            Console.ReadLine();
        }

        private static void CargaAlbumes()
        {
            Facade fachada = Facade.Instance;
            List<VOCancion> listaCanciones1 = new List<VOCancion>();
            listaCanciones1.Add(fachada.CancionObtener(17));
            listaCanciones1.Add(fachada.CancionObtener(21));
            VOAlbum album1 = new VOAlbum("Charlas de oficina", 2013, "Rock", fachada.obtenerBanda(4), listaCanciones1, null);

            List<VOCancion> listaCanciones2 = new List<VOCancion>();
            listaCanciones2.Add(fachada.CancionObtener(9));
            listaCanciones2.Add(fachada.CancionObtener(18));
            VOAlbum album2 = new VOAlbum("Mouse con chocolate", 2015, "Salsa", fachada.obtenerBanda(6), listaCanciones2, null);

            List<VOCancion> listaCanciones3 = new List<VOCancion>();
            listaCanciones3.Add(fachada.CancionObtener(20));
            listaCanciones3.Add(fachada.CancionObtener(22));
            VOAlbum album3 = new VOAlbum("Todo bien", 2020, "Rock", fachada.obtenerBanda(5), listaCanciones3, null);


            Console.WriteLine("Insertando albumes");
            fachada.AlbumAlta(album1);
            fachada.AlbumAlta(album2);
            fachada.AlbumAlta(album3);

            Console.ReadLine();
        }

        private static void CargaIntegrantesSinBanda()
        {
            Facade fachada = Facade.Instance;
            VOPersona persona1 = new VOPersona("Roger", "Water", null);
            VOIntegrante integrante1 = new VOIntegrante(DateTime.Now, "", persona1, 0, null);
            VOPersona persona2 = new VOPersona("David", "Gilmour", null);
            VOIntegrante integrante2 = new VOIntegrante(DateTime.Now, "", persona2, 0, null);

            fachada.IntegranteAlta(integrante1);
            fachada.IntegranteAlta(integrante2);

            Console.ReadLine();
        }
    }
}
