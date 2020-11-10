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
    public class MusicianDAL
    {
        string DbConnectionString = ConfigurationManager.ConnectionStrings["MySqlSena"].ToString();

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }

        public int InsertMusician(Musician data)
        {
            MySqlManager db = new MySqlManager(DbConnectionString);
            int nRows = 0;
            try
            {
                DataTable dtParameters = db.ConfigTableForParameters();
                dtParameters.Rows.Add("@FirstName", data.FirstName, MySqlDbType.VarChar);
                dtParameters.Rows.Add("@LastName", data.LastName, MySqlDbType.VarChar);
                dtParameters.Rows.Add("@BirthDate", data.BirthDate.ToString("yyyy-MM-dd HH:mm:ss"), MySqlDbType.DateTime);
                dtParameters.Rows.Add("@BirthPlace", data.BirthPlace, MySqlDbType.VarChar);

                string query = db.InsertQueryBuilder("soundbeats.musician", dtParameters);

                // Apertura de conexión
                if (db.Open())
                {
                    db.TableAddInParameters(dtParameters);
                    nRows = db.ExecuteNonQuery(CommandType.Text, query);
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
