using System;

namespace SqlConnection
{
    using System;
    using System.Data;
    using System.Collections;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    class Program
    { 
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\argoos\Documents\Arwyn\C# Coding\Git Projects\SqlConnection\stoxx50.csv";
            string server = "NLLT108279";
            string dataBase = "ArwynTest";
            string table = "S&P500";
            string column = "Date";


            DataFetcher dataFetcher = new DataFetcher();
            dataFetcher.ReadInData(server, dataBase, filePath);
            DataTable dataTable = dataFetcher.GetData(server, dataBase, table, column);

            List<string> clientIds = dataFetcher.CreateListFromDataTable<string>(dataTable, "client_id");
        }
    }
}
