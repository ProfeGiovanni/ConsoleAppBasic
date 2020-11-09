using ConsoleApp.Domain;
using DBConnection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;


namespace ConsoleApp.DataLayer
{
    public class CountryDAL
    {
        string DbConnectionString = ConfigurationManager.ConnectionStrings["MySqlSena"].ToString();

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> GetCountries()
        {
            MySqlManager db = new MySqlManager(DbConnectionString);
            StringBuilder sbQuery = new StringBuilder();
            List<Country> country = new List<Country>();
            try
            {
                sbQuery.Append("select ")
                        .Append("Id, ")
                        .Append("NameEn, ")
                        .Append("NameEs, ")
                        .Append("ISO2, ")
                        .Append("ISO3 ")
                        .Append("from country ");

                if (db.Open() == true)
                {
                    MySqlDataReader reader = db.ExecuteReader(CommandType.Text, sbQuery.ToString());
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            country.Add(new Country
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                NameEn = reader["NameEn"].ToString(),
                                NameEs = reader["NameEs"].ToString(),
                                ISO2 = reader["ISO2"].ToString(),
                                ISO3 = reader["ISO3"].ToString()
                            });
                        }
                    }
                    else
                        country = null;
                    reader.Close();
                    db.Close();
                }
                else
                {
                    // Error at Connect to MySQL
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

        public int InsertCountry(Country data)
        {
            MySqlManager db = new MySqlManager(DbConnectionString);
            StringBuilder sbQuery = new StringBuilder();
            int nRows = 0;
            try
            {
                // INSERT INTO soundbeats.country(NameEn, NameEs, ISO2, ISO3)
                // VALUES('Greece', 'Grecia', 'GR', 'GRC')
                sbQuery.Append("INSERT INTO soundbeats.country(NameEn, NameEs, ISO2, ISO3) ")
                    .Append("VALUES(")
                    .Append("'").Append(data.NameEn).Append("', ")
                    .Append("'").Append(data.NameEs).Append("', ")
                    .Append("'").Append(data.ISO2).Append("', ")
                    .Append("'").Append(data.ISO3).Append("') ");
                // Apertura de conexión
                if (db.Open())
                {
                    nRows = db.ExecuteNonQuery(CommandType.Text, sbQuery.ToString());
                    if (nRows == -1)
                        ErrorMessage = db.ErrorMessage;
                    db.Close();
                }
                else
                {
                    // Error at Connect to SQL-Server Server
                    ErrorMessage = db.ErrorMessage;
                    return -1;
                }
            }
            catch
            {
                ErrorMessage = db.ErrorMessage;
                return -1;
            }
            return nRows;
        }


        public int InsertCountryParametrics(Country data)
        {
            MySqlManager db = new MySqlManager(DbConnectionString);
            StringBuilder sbQuery = new StringBuilder();
            int nRows = 0;
            try
            {
                // INSERT INTO soundbeats.country(NameEn, NameEs, ISO2, ISO3)
                // VALUES('Greece', 'Grecia', 'GR', 'GRC')
                sbQuery.Append("INSERT INTO soundbeats.country(NameEn, NameEs, ISO2, ISO3) ")
                    .Append("VALUES(")
                    .Append("'").Append(data.NameEn).Append("', ")
                    .Append("'").Append(data.NameEs).Append("', ")
                    .Append("'").Append(data.ISO2).Append("', ")
                    .Append("'").Append(data.ISO3).Append("') ");
                // Apertura de conexión
                if (db.Open())
                {
                    nRows = db.ExecuteNonQuery(CommandType.Text, sbQuery.ToString());
                    if (nRows == -1)
                        ErrorMessage = db.ErrorMessage;
                    db.Close();
                }
                else
                {
                    // Error at Connect to SQL-Server Server
                    ErrorMessage = db.ErrorMessage;
                    return -1;
                }
            }
            catch
            {
                ErrorMessage = db.ErrorMessage;
                return -1;
            }
            return nRows;
        }
    }
}
