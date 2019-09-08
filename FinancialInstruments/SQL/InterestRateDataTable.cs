using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialInstruments.FinancialProducts;

namespace FinancialInstruments.SQL
{
    public static class InterestRateDataTable
    {
        public static DataTable CreateDataTableObject(SortedDictionary<string, SortedDictionary<DateTime, double?>> instrumentObservations)
        {
            DateTime rowTimeStamp = DateTime.Now;
            DataTable dataTable = InitiateTable();

            foreach (string productId in instrumentObservations.Keys)
            {
                foreach (DateTime date in instrumentObservations[productId].Keys)
                {
                    DataRow row = dataTable.NewRow();
                    row["Product"] = productId;
                    row["Date"] = date;
                    row["Rate"] = instrumentObservations[productId][date] ?? (object)DBNull.Value;
                    row["RowTimeStamp"] = rowTimeStamp;

                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        public static DataTable InitiateTable()
        {
            // Create a new DataTable titled 'Names.'
            DataTable namesTable = new DataTable(Settings.InterestRateTableName);

            DataColumn productColumn = new DataColumn();
            productColumn.DataType = Type.GetType("System.String");
            productColumn.ColumnName = "Product";
            namesTable.Columns.Add(productColumn);

            DataColumn dateColumn = new DataColumn();
            dateColumn.DataType = Type.GetType("System.DateTime");
            dateColumn.ColumnName = "Date";
            namesTable.Columns.Add(dateColumn);

            DataColumn rateColumn = new DataColumn();
            rateColumn.DataType = Type.GetType("System.Double");
            rateColumn.ColumnName = "Rate";
            namesTable.Columns.Add(rateColumn);

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

        public static void SetColumnMappings(SqlBulkCopy copy)
        {
            copy.ColumnMappings.Add("Product", "Product");
            copy.ColumnMappings.Add("Date", "Date");
            copy.ColumnMappings.Add("Rate", "Rate");
            copy.ColumnMappings.Add("RowTimeStamp", "RowTimeStamp");
        }
    }
}
