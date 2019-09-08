using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FinancialInstruments.FinancialProducts;

namespace FinancialInstruments.SQL
{
    public static class SqlWriter
    {
        internal static void WriteStockDataToDatabase(SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> instrumentObservations)
        {
            DataTable dataTable = StockDataTable.CreateDataTableObject(instrumentObservations);
            WriteTableToDataBase(dataTable);
        }

        internal static void WriteInterestRateDataToDatabase(SortedDictionary<string, SortedDictionary<DateTime, double?>> instrumentObservations)
        {
            DataTable dataTable = InterestRateDataTable.CreateDataTableObject(instrumentObservations);
            WriteTableToDataBase(dataTable);
        }

        private static void WriteTableToDataBase(DataTable dataTable)
        {
            using (SqlConnection sqlConnection = new SqlConnection(SqlBaseClass._connectionString))
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

        private static void SetColumnMappings(SqlBulkCopy copy)
        {
            if (copy.DestinationTableName == Settings.StockTableName)
            {
                StockDataTable.SetColumnMappings(copy);
                return;
            }

            if (copy.DestinationTableName == Settings.InterestRateTableName)
            {
                InterestRateDataTable.SetColumnMappings(copy);
                return;
            }

            throw new Exception($"Failed: the table name {copy.DestinationTableName} is currently not supported");
        }
    }
}
