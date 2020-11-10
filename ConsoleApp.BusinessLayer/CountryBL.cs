using ConsoleApp.DataLayer;
using ConsoleApp.Domain;
using System.Collections.Generic;

namespace ConsoleApp.BusinessLayer
{
    public class CountryBL
    {
        public string ErrorMessage { get; set; }

        public Country GetCountry(int id)
        {
            CountryDAL dbCountry = new CountryDAL();
            return dbCountry.GetCountry(id);
        }

        public List<Country> GetCountries()
        {
            CountryDAL dbCountry = new CountryDAL();
            return dbCountry.GetCountries();
        }


        public bool InsertCountry(Country data)
        {
            int nRows = 0;
            CountryDAL dbCountry = new CountryDAL();
            //nRows = dbCountry.InsertCountry(data);
            nRows = dbCountry.InsertCountry(data);
            if (nRows != -1)
                return true;
            else
            {
                ErrorMessage = dbCountry.ErrorMessage;
                return false;
            }
        }

    }
}
