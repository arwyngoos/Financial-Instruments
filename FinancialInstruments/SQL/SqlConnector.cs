using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FinancialInstruments.FinancialProducts;

namespace FinancialInstruments.SQL
{
    public class SqlConnector
    {
        private readonly string _connectionString;

        public SqlConnector()
        {
            _connectionString = 
                        "server=" + Settings.DataBaseEngine + ";" + // Network address
                        "Trusted_connection=true;" + // Secured by Windows Authentication or SSPI
                        "database=" + Settings.DataBase + ";" + // Select 'database' associated with teh connection server
                        "connection timeout = 30;" + // Connection time-out
                        "Integrated Security=SSPI";             
        }

        internal void WriteToDatabase(SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> instrumentObservations)
        {
            DataTable dataTable = CreateDataTableObject(instrumentObservations);
            WriteTableToDataBase(dataTable);
        }

        private void WriteTableToDataBase(DataTable dataTable)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(sqlConnection))
                {
                    copy.DestinationTableName = dataTable.TableName;
                    SetColumnMappings(copy);

                    try
                    {
                        copy.WriteToServer(dataTable);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        private void SetColumnMappings(SqlBulkCopy copy)
        {
            copy.ColumnMappings.Add("Product", "Product");
            copy.ColumnMappings.Add("Date", "Date");
            copy.ColumnMappings.Add("Open", "Open");
            copy.ColumnMappings.Add("High", "High");
            copy.ColumnMappings.Add("Low", "Low");
            copy.ColumnMappings.Add("Close", "Close");
            copy.ColumnMappings.Add("Volume", "Volume");
            copy.ColumnMappings.Add("RowTimeStamp", "RowTimeStamp");
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
            productColumn.DataType = Type.GetType("System.String");
            productColumn.ColumnName = "Product";
            namesTable.Columns.Add(productColumn);

            DataColumn dateColumn = new DataColumn();
            dateColumn.DataType = Type.GetType("System.DateTime");
            dateColumn.ColumnName = "Date";
            namesTable.Columns.Add(dateColumn);

            DataColumn openColumn = new DataColumn();
            openColumn.DataType = Type.GetType("System.Double");
            openColumn.ColumnName = "Open";
            namesTable.Columns.Add(openColumn);

            DataColumn highColumn = new DataColumn();
            highColumn.DataType = Type.GetType("System.Double");
            highColumn.ColumnName = "High";
            namesTable.Columns.Add(highColumn);

            DataColumn lowColumn = new DataColumn();
            lowColumn.DataType = Type.GetType("System.Double");
            lowColumn.ColumnName = "Low";
            namesTable.Columns.Add(lowColumn);

            DataColumn closeColumn = new DataColumn();
            closeColumn.DataType = Type.GetType("System.Double");
            closeColumn.ColumnName = "Close";
            namesTable.Columns.Add(closeColumn);

            DataColumn volumeColumn = new DataColumn();
            volumeColumn.DataType = Type.GetType("System.Double");
            volumeColumn.ColumnName = "Volume";
            namesTable.Columns.Add(volumeColumn);

            DataColumn timestampColumn = new DataColumn();
            timestampColumn.DataType = Type.GetType("System.DateTime");
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
            return $"SELECT * FROM[{Settings.DataBase}].[dbo].[{Settings.TableName}] " +
                   $"where RowTimeStamp = (select max(RowTimeStamp) from [{Settings.DataBase}].[dbo].[{Settings.TableName}])" ;
        }

        public DataTable LoadDataTable(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
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
