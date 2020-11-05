using ConsoleApp.Domain;
using DBConnection;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;


namespace ConsoleApp.DataLayer
{
    public class CountryDAL
    {
        string DbConnectionString = ConfigurationManager.ConnectionStrings["MySqlSena"].ToString();


        public string ErrorMessage { get; set; }

        public Country GetCountry(int id)
        {
            MySqlManager db = new MySqlManager(DbConnectionString);
            StringBuilder sbQuery = new StringBuilder();
            Country country = new Country();
            try
            {
                sbQuery.Append("select ")
                        .Append("Id, ")
                        .Append("NameEn, ")
                        .Append("NameEs, ")
                        .Append("ISO2, ")
                        .Append("ISO3 ")
                        .Append("from country ")
                        .Append("where Id = ").Append(id);

                if (db.Open() == true)
                {
                    MySqlDataReader reader = db.ExecuteReader(CommandType.Text, sbQuery.ToString());
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            country = new Country
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                NameEn = reader["NameEn"].ToString(),
                                NameEs = reader["NameEs"].ToString(),
                                ISO2 = reader["ISO2"].ToString(),
                                ISO3 = reader["ISO3"].ToString()
                            };
                        }
                    }
                    else
                        country = null;
                    reader.Close();
                    db.Close();
                }
                else
                {
                    // Error at Connect to SQL-Server Server
                    ErrorMessage = db.ErrorMessage;
                    return null;
                }
                return country;
            }
            catch
            {
                ErrorMessage = db.ErrorMessage;
                return null;
            }

        }

    }
}
