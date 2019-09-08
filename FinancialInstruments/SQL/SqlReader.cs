using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialInstruments.SQL
{
    public class SqlReader 
    {
        internal static DataTable ReadStockDataFromDataBase()
        {
            string query = CreateQuery(Settings.StockTableName);
            return LoadDataTable(query);
        }

        internal static DataTable ReadInterestRateDataFromDataBase()
        {
            string query = CreateQuery(Settings.InterestRateTableName);
            return LoadDataTable(query);
        }

        public static DataTable LoadDataTable(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(SqlBaseClass._connectionString))
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


        public static string CreateQuery(string tableName)
        {
            return $"SELECT * FROM[{Settings.DataBase}].[dbo].[{tableName}] " +
                   $"where RowTimeStamp = (select max(RowTimeStamp) from [{Settings.DataBase}].[dbo].[{tableName}])";
        }
    }
}
