using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace DBConnection
{
    #region DATA VERSION
    /* 
    v 1.3.0 - 2018.02.21
    * Remove unused usings
    * Change Namespace name to a most generic and easy access for new projects

    v 1.2.0 - 2017.05.19
    * Add a string parameter in Constructor, to change connectionstrin externally

    v 1.1.1 - 2016.01.26
    * Add  Commando.Parameters.Clear() method to clean de Parameters data in Commando parameter. Inserted on TableAddInParameters() just before to put datatable parameters in Parameters commando datarows.

    v 1.1.0 - 2015.09.08
    * Add another parameters to interact with GUID datatypes in TableAddInParameters() method. Overload of three parameters.

    v 1.0.0 - 2015.06.07
    * Class Creation to interact with MS SQLSERVER Data Bases, based on BDotNet library provided on BDotNet Workshop on 2015.05.01.
    * TableAddInParameters() method added to make the Data Parameters easier to load to ExecuteNonQuery queries.

    */
    #endregion

    public class MySqlManager
    {

        #region Propiedades de la Clase

        /// <summary>Propiedad que Expone el DataReader en donde se encuentran los datos leidos
        /// </summary>
        public IDataReader DataReader;

        /// <summary>Propiedad que almacena el objeto de Conexion a la BD
        /// </summary>
        public MySqlConnection Conexion { get; set; }

        /// <summary>Nombre de la Base de Datos
        /// </summary>
        public String BaseDatos { get; set; }

        /// <summary>Password de la Base de datos si lo tiene
        /// </summary>
        public String Password { get; set; }

        /// <summary>Propiedad que devuelve un objeto SqlCeCommand
        /// </summary>
        public MySqlCommand Commando { get; set; }

        /// <summary>Ruta de la base de datos si es diferente a la de la Aplicacion
        /// </summary>
        public String ConnectionString { get; set; }

        /// <summary> Datatable con el resultado de la consulta
        /// </summary>
        public DataTable Datos { get; set; }

        /// <summary> Mensaje de error que arroja la clase en el momento de aparecer una SqlException
        /// </summary>
        public string ErrorMessage { get; set; }
         
        #endregion

        #region Constructores

        /// <summary>Constructor vacio de la Clase
        /// </summary>
        public MySqlManager(string connString)
        {
            Commando = new MySqlCommand();
            Conexion = new MySqlConnection();
            Password = String.Empty;
            Conexion.ConnectionString = connString;
        }

        #endregion


        #region Metodos


        /// <summary> Método que abre la conexion a la Base de datos
        /// </summary>
        public bool Open()
        {
            // Si hay una conexion abierta se mantiene
            if (Conexion.State == System.Data.ConnectionState.Open)
                return true;
            // Abre la conexion
            try
            {
                Conexion.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        /// <summary> Método que Cierra la conexión y libera recursos
        /// </summary>
        public void Close()
        {
            try
            {
                Conexion.Close();
                Conexion.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>Crea un Objeto SqlCommand para ser utilizado
        /// </summary>
        public void CreateParameter()
        {
            try
            {
                Commando = Conexion.CreateCommand();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region # # # # # # # # Adición de Parámetros para consultas    # # # # # # # #

        public void ClearParameters()
        {
            Commando.Parameters.Clear();
        }

        public void TableAddInParameters(DataTable dtParameters, object objetoAdicional = null)
        {
            MySqlDbType typeParam;
            Commando.Parameters.Clear();
            try
            {
                foreach (DataRow row in dtParameters.Rows)
                {
                    typeParam = (MySqlDbType)Enum.Parse(typeof(MySqlDbType), row["ParamType"].ToString());
                    if (row["ParamValor"].ToString() == "imagenEnBytes")
                    {
                        AddInParameters(row["ParamNombre"].ToString(), objetoAdicional, typeParam);
                    }
                    else if (row["ParamValor"].ToString() == "GUID")
                    {
                        AddInParameters(row["ParamNombre"].ToString(), objetoAdicional, typeParam);
                    }
                    else
                    {
                        AddInParameters(row["ParamNombre"].ToString(), (object)row["ParamValor"], typeParam);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        /// <summary> Inserción de parámetros en tabla de parámetros para envío de comandos parametrizados
        /// </summary>
        /// <param name="dtParameters">Tabla de parámetros</param>
        /// <param name="objetoAdicional1">condición de GUID, es obligatoria</param>
        /// <param name="objetoAdicional2">Condición de Imagen, es opcional</param>
        public void TableAddInParameters(DataTable dtParameters, object objetoAdicional1, object objetoAdicional2 = null)
        {
            MySqlDbType typeParam;
            Commando.Parameters.Clear();
            foreach (DataRow row in dtParameters.Rows)
            {
                typeParam = (MySqlDbType)Enum.Parse(typeof(MySqlDbType), row["ParamType"].ToString());
                if (row["ParamValor"].ToString() == "imagenEnBytes")
                {
                    AddInParameters(row["ParamNombre"].ToString(), objetoAdicional2, typeParam);
                }
                else if (row["ParamValor"].ToString() == "GUID")
                {
                    AddInParameters(row["ParamNombre"].ToString(), objetoAdicional1, typeParam);
                }
                else
                {
                    AddInParameters(row["ParamNombre"].ToString(), (object)row["ParamValor"], typeParam);
                }
            }
        }

        /// <summary> Inserción de parámetros en tabla de parámetros para envío de comandos parametrizados
        /// </summary>
        /// <param name="dtParameters">Tabla de parámetros</param>
        /// <param name="objetoAdicional1">condición de GUID, es obligatoria</param>
        /// <param name="objetoAdicional2">Condición de Imagen, es opcional</param>
        /// <param name="objetoAdicional3">condición de 2nd GUID, es opcional</param>
        public void TableAddInParameters(DataTable dtParameters, object objetoAdicional1, object objetoAdicional2 = null, object objetoAdicional3 = null)
        {
            MySqlDbType typeParam;
            Commando.Parameters.Clear();
            foreach (DataRow row in dtParameters.Rows)
            {
                typeParam = (MySqlDbType)Enum.Parse(typeof(MySqlDbType), row["ParamType"].ToString());
                if (row["ParamValor"].ToString() == "imagenEnBytes")
                    AddInParameters(row["ParamNombre"].ToString(), objetoAdicional2, typeParam);
                else if (row["ParamValor"].ToString() == "GUID")
                    AddInParameters(row["ParamNombre"].ToString(), objetoAdicional1, typeParam);
                else if (row["ParamValor"].ToString() == "GUID2")
                    AddInParameters(row["ParamNombre"].ToString(), objetoAdicional3, typeParam);
                else
                    AddInParameters(row["ParamNombre"].ToString(), (object)row["ParamValor"], typeParam);
            }
        }

        /// <summary>Metodo que permite crear los parametros de las diferentes consultas a realizar
        /// </summary>
        /// <param name="paramName">Nombre del parametro</param>
        /// <param name="typeParam">Tipo del parametro</param>
        /// <param name="paramValue">Valor del parametro</param>
        public void AddInParameters(String paramName, Object paramValue, MySqlDbType typeParam)
        {
            try
            {
                MySqlParameter objSQLParam = new MySqlParameter();// paramName, paramValue);
                objSQLParam.ParameterName = paramName;
                objSQLParam.MySqlDbType = typeParam;
                objSQLParam.Value = paramValue;
                Commando.Parameters.Add(objSQLParam);
            }
            catch (MySqlException ex)
            {
                ErrorMessage = ex.Message;
                throw;
            }
        }

        public DataTable ConfigTableForParameters(int nFilas)
        {
            DataTable dtParametros = new DataTable();

            // 1. Creación de tabla para envío de parámetros
            // Se realiza este paso para evitar cargar de forma innecesaria los métodos alojados en capa de Acceso a Datos (DAL)
            dtParametros.Columns.Add("ParamNombre", System.Type.GetType("System.String"));
            dtParametros.Columns.Add("ParamValor");
            dtParametros.Columns.Add("ParamType");
            //dtParametros.Columns.Add("ParamSize");
            for (int i = 0; i < nFilas; i++)
                dtParametros.Rows.Add();
            return dtParametros;
        }
        #endregion


        #region Query Constructors

        /// <summary> Builds the Query for an Insert action
        /// </summary>
        /// <param name="tableName">DB Table name</param>
        /// <param name="parametersTable">DataTable with parameters structure (the same to use as parametrized query)</param>
        /// <returns></returns>
        public string InsertQueryBuilder(string tableName, DataTable parametersTable)
        {
            StringBuilder sbQuery = new StringBuilder();
            string tempoString = string.Empty;

            sbQuery.Append("INSERT INTO ").Append(tableName).Append(" (");
            foreach (DataRow row in parametersTable.Rows)
            {
                sbQuery.Append(row["ParamNombre"].ToString().Replace("@", string.Empty)).Append(", ");
            }
            sbQuery.Remove(sbQuery.Length - 2, 2);
            sbQuery.Append(") ");
            sbQuery.Append("VALUES (");
            foreach (DataRow row in parametersTable.Rows)
            {
                sbQuery.Append(row["ParamNombre"]).Append(", ");
            }
            sbQuery.Remove(sbQuery.Length - 2, 2);
            sbQuery.Append(") ");
            return sbQuery.ToString();
        }


        /// <summary>Builds the Query for an Update action
        /// </summary>
        /// <param name="tableName">DB Table name</param>
        /// <param name="parametersTable">DataTable with parameters structure (the same to use as parametrized query)</param>
        /// <param name="indexParamTable">Location of the WHERE parameter</param>
        /// <returns></returns>
        public string UpdateQueryBuilder(string tableName, DataTable parametersTable, int indexParamTable)
        {
            StringBuilder sbQuery = new StringBuilder();
            string tempoString = string.Empty;

            sbQuery.Append("UPDATE ").Append(tableName).Append(" SET ");
            foreach (DataRow row in parametersTable.Rows)
            {
                sbQuery.Append(row["ParamNombre"].ToString().Replace("@", string.Empty)).Append(" = ").Append(row["ParamNombre"]).Append(", ");
            }
            sbQuery.Remove(sbQuery.Length - 2, 2);
            sbQuery.Append(" WHERE ( ").
                Append(parametersTable.Rows[indexParamTable]["ParamNombre"].ToString().Replace("@", string.Empty)).
                Append(" = ").
                Append(parametersTable.Rows[indexParamTable]["ParamNombre"].ToString()).
                Append(")");
            return sbQuery.ToString();
        }


        #endregion

        #region ::::::::::::::::::::: MySQL commands :::::::::::::::::::::

        /// <summary>Ejecuta sentencias SQL en la BAse de datos
        /// </summary>
        /// <param name="tipoComando">CommandType para saber que se hace con la instruccion</param>
        /// <param name="query">La sentencia SQL a Ejecutar</param>
        /// <returns>Un Datareader para ser recorrido</returns>
        public MySqlDataReader ExecuteReader(CommandType tipoComando, String query)
        {
            MySqlDataReader DataReader = null;
            VerificarComando();
            Commando.Connection = Conexion;
            //Commando.CommandTimeout = 240;   // Este valor es en segundos
            Commando.CommandType = tipoComando;
            Commando.CommandText = query;

            try
            {
                DataReader = Commando.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return DataReader;
        }

        /// <summary> Metodo que ejecuta una sentencia y devuelve un DataSet
        /// </summary>
        /// <param name="tipoComando"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(CommandType tipoComando, String query)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter();

            VerificarComando();
            Commando.Connection = Conexion;
            //Commando.CommandTimeout = 240;   // Este valor es en segundos
            Commando.CommandType = tipoComando;
            Commando.CommandText = query;

            try
            {
                da = new MySqlDataAdapter(Commando);
                da.Fill(ds);
            }
            catch (MySqlException ex)
            {
                throw ex;
            }

            return ds;
        }

        /// <summary> Método que ejecuta una sentencia y devuelve un DataTable
        /// </summary>
        /// <param name="query">Sentencia SQl a ejecutar contra la base de datos</param>
        /// <returns>Un Datatable con los datos</returns>
        public DataTable ExecuteDataTable(String query)
        {
            MySqlDataAdapter daTmp = new MySqlDataAdapter();
            try
            {
                DataReader = null;
                VerificarComando();
                Commando.Connection = Conexion;
                //Commando.CommandTimeout = 240;   // Este valor es en segundos
                Commando.CommandType = CommandType.Text;
                Commando.CommandText = query;
                daTmp = new MySqlDataAdapter(Commando);
                Datos = new DataTable();
                daTmp.Fill(Datos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                daTmp.Dispose();
            }
            return Datos;
        }

        /// <summary>Ejecuta Sentencias SQL pero devuelve un solo valor
        /// </summary>
        /// <param name="tipoComando">Tipo de comando a Ejecutar</param>
        /// <param name="query">Instruccion SQL a Ejecutar</param>
        /// <returns>Devuelve una cadena con el resultado d ela consulta SQL</returns>
        public String ExecuteScalar(CommandType tipoComando, String query)
        {
            VerificarComando();
            Commando.Connection = Conexion;
            Commando.CommandType = tipoComando;
            Commando.CommandText = query;
            String stReturn;

            try
            {
                stReturn = Commando.ExecuteScalar().ToString();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return stReturn;
        }

        /// <summary> Ejecuta sentencias de INSERT, DELETE y UPDATE 
        /// </summary>
        /// <param name="tipoComando">Tipo de comando a Ejecutar</param>
        /// <param name="query">Instruccion SQL a Ejecutar</param>
        /// <returns>Un entero con el numero de registros Afectados</returns>
        public Int32 ExecuteNonQuery(CommandType tipoComando, String query)
        {
            VerificarComando();
            Commando.Connection = Conexion;
            Commando.CommandType = tipoComando;
            Commando.CommandText = query;
            Int32 nRowsReturn;

            try
            {
                nRowsReturn = Commando.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                nRowsReturn = -1; //
                ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                nRowsReturn = -1; //
                ErrorMessage = ex.Message;
            }

            return nRowsReturn;
        }

        public Int64 GetScopeIdentity()
        {
            return Convert.ToInt64(ExecuteScalar(CommandType.Text, "SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]"));
        }

        /// <summary> Metodo que Verifica si el Comando no es nulo, si es nulo lo inicializa
        /// </summary>
        private void VerificarComando()
        {
            if (Commando == null)
            {
                CreateParameter();
            }
        }

        #endregion

        #endregion
    }
}
