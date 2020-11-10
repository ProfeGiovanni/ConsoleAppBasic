using ConsoleApp.DataLayer;
using ConsoleApp.Domain;
using System.Collections.Generic;

namespace ConsoleApp.BusinessLayer
{
    public class MusicianBL
    {
        public string ErrorMessage { get; set; }


        public bool InsertMusician(Musician data)
        {
            int nRows = 0;
            MusicianDAL dbMusician = new MusicianDAL();
            nRows = dbMusician.InsertMusician(data);
            if (nRows != -1)
                return true;
            else
            {
                ErrorMessage = dbMusician.ErrorMessage;
                return false;
            }
        }
    }
}
