using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Linq;
using FinancialInstruments.FinancialProducts;

namespace FinancialInstruments.SQL
{
    public class SqlConnector
    {
        private string connectionString;


        public SqlConnector()
        {
            connectionString = "server=" + Settings.DataBaseEngine + ";" + // Network address
                        "Trusted_connection=true;" + // Secured by Windows Authentication or SSPI
                        "database=" + Settings.DataBase + ";" + // Select 'database' associated with teh connection server
                        "connection timeout = 30;" + // Connection time-out
                        "Integrated Security=SSPI";             //

            
        }

        internal void WriteToDatabase(SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> instrumentObservations)
        {
            DataTable dataTable = CreateDataTableObject(instrumentObservations);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString);

            bulkCopy.DestinationTableName = dataTable.TableName;
            try
            {
                bulkCopy.WriteToServer(dataTable);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private DataTable CreateDataTableObject(SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> instrumentObservations)
        {
            DateTime rowTimeStamp = DateTime.Now;
            DataTable dataTable = MakeNamesTable();

            foreach (string productId in instrumentObservations.Keys)
            {
                foreach (DateTime date in instrumentObservations[productId].Keys)
                {
                    DataRow row = dataTable.NewRow();
                    row["Product"] = productId;
                    row["Date"] = date;
                    row["Open"] = instrumentObservations[productId][date].Open;
                    row["High"] = instrumentObservations[productId][date].High;
                    row["Low"] = instrumentObservations[productId][date].Low;
                    row["Close"] = instrumentObservations[productId][date].Close;
                    row["Volume"] = instrumentObservations[productId][date].Volume;
                    row["RowTimeStamp"] = rowTimeStamp;

                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
        

        private DataTable MakeNamesTable()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable(Settings.TableName);

            DataColumn productColumn = new DataColumn();
            productColumn.DataType = System.Type.GetType("System.String");
            productColumn.ColumnName = "Product";
            namesTable.Columns.Add(productColumn);

            DataColumn dateColumn = new DataColumn();
            dateColumn.DataType = System.Type.GetType("System.DateTime");
            dateColumn.ColumnName = "Date";
            namesTable.Columns.Add(dateColumn);

            DataColumn openColumn = new DataColumn();
            openColumn.DataType = System.Type.GetType("System.Double");
            openColumn.ColumnName = "Open";
            namesTable.Columns.Add(openColumn);

            DataColumn highColumn = new DataColumn();
            highColumn.DataType = System.Type.GetType("System.Double");
            highColumn.ColumnName = "High";
            namesTable.Columns.Add(highColumn);

            DataColumn lowColumn = new DataColumn();
            lowColumn.DataType = System.Type.GetType("System.Double");
            lowColumn.ColumnName = "Low";
            namesTable.Columns.Add(lowColumn);

            DataColumn closeColumn = new DataColumn();
            closeColumn.DataType = System.Type.GetType("System.Double");
            closeColumn.ColumnName = "Close";
            namesTable.Columns.Add(closeColumn);

            DataColumn volumeColumn = new DataColumn();
            volumeColumn.DataType = System.Type.GetType("System.Int32");
            volumeColumn.ColumnName = "Volume";
            namesTable.Columns.Add(volumeColumn);

            DataColumn timestampColumn = new DataColumn();
            timestampColumn.DataType = System.Type.GetType("System.DateTime");
            timestampColumn.ColumnName = "RowTimeStamp";
            namesTable.Columns.Add(timestampColumn);

            // Create an array for DataColumn objects.
            DataColumn[] keys = new DataColumn[3];
            keys[0] = productColumn;
            keys[1] = dateColumn;
            keys[2] = timestampColumn;
            namesTable.PrimaryKey = keys;

            // Return the new DataTable.
            return namesTable;
        }

        internal DataTable ReadFromDataBase()
        {
            string query = CreateQuery();
            return LoadDataTable(query);
        }

        public string CreateQuery()
        {
            return $"SELECT {"*"} FROM[{Settings.DataBase}].[dbo].[{Settings.TableName}] " +
                   $"where RowTimeStamp = (select max(RowTimeStamp) from [{Settings.DataBase}].[dbo].[{Settings.TableName}])" ;
        }

        public DataTable LoadDataTable(string query)
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

    }
}
