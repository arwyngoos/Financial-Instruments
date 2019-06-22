using System;
using System.Collections.Generic;
using System.Text;

namespace SqlConnection
{

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Linq;

    public class DataFetcher
    {
        public string CreateConnectionString(string dataBaseName, string server)
        {
            string sqlConnectionCommand = "server=" + server + ";" + // Network address
                                    "Trusted_connection=true;" + // Secured by Windows Authentication or SSPI
                                    "database=" + dataBaseName + ";" + // Select 'database' associated with teh connection server
                                    "connection timeout = 30;" + // Connection time-out
                                    "Integrated Security=SSPI";             //

            return sqlConnectionCommand;
        }

        internal void ReadInData(string server, string dataBase, string filePath)
        {
            string connectionString = CreateConnectionString(dataBase, server);
            string updateDataBaseQuery = CreateUpdateDataBaseQuery(filePath);

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(updateDataBaseQuery, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                }
            }

        }

        private string CreateUpdateDataBaseQuery(string path)
        {
            string query = $"BULK Insert dbo.stoxx50" +
            $" FROM '{path}'" +
            " WITH(" +
                "FIRSTROW = 2," +
                "FieldTerminator = ';'," +
                "RowTerminator =" + @"'\n'" +
            ")";
            return query;
        }

        public string CreateQuery(string column, string table)
        {
            return $"SELECT [{column}] FROM[SQL_Intermediate].{table}";
        }

        public DataTable GetData(string server, string dataBase, string table, string column)
        {
            string connectionString = CreateConnectionString(dataBase, server);
            string query = CreateQuery(column, table);

            return LoadDataTable(connectionString, query);
        }
        public DataTable LoadDataTable(string connectionString, string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(dataReader);
                }
            }

            return dataTable;
        }

        public List<T> CreateListFromDataTable<T>(DataTable dataTable, string columnName)
        {
            List<T> outputList = new List<T>();
            try
            {
                
                foreach (DataRow row in dataTable.Rows)
                {

                    outputList.Add((T)row[columnName]);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed: error in dataTable. {e}");
            }

            return outputList;
        }
    }
}
