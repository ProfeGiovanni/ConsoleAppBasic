using ConsoleApp.BusinessLayer;
using ConsoleApp.Domain;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CountryBL queryToCountries = new CountryBL();
            Country country = new Country();

            Console.WriteLine("Consulta a la base de datos (paises)");
            Console.Write("Id: ");
            // Realizar la consulta a BD
            int idCountry = Convert.ToInt32(Console.ReadLine());
            country = queryToCountries.GetCountry(idCountry);

            if (country != null)
            {
                //mostrar el resultado:
                Console.WriteLine("El país tiene los siguientes datos: ");
                Console.WriteLine("Nombre: {0} - {1}", country.NameEn, country.ISO3);
            }
            else
                Console.WriteLine("No hay datos");

            Console.ReadLine();

        }
    }
}
