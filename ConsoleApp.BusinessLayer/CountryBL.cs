using ConsoleApp.DataLayer;
using ConsoleApp.Domain;

namespace ConsoleApp.BusinessLayer
{
    public class CountryBL
    {

        public Country GetCountry(int id)
        {
            CountryDAL dbCountry = new CountryDAL();
            return dbCountry.GetCountry(id);
        }

    }
}
