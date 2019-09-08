using FinancialInstruments.FinancialProducts;
using System;
using System.Collections.Generic;

namespace FinancialInstruments.Processes
{
    public static class DataObject
    {
        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> StockObservationsFromExcel;

        public static SortedDictionary<string, SortedDictionary<DateTime, double?>> InterestRateObservationsFromExcel;

        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadStockDataFromSqlOutput;

        public static SortedDictionary<string, SortedDictionary<DateTime, double?>> ReadInterestRateDataFromSqlOutput;

        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadJsonProcessOutput;

        public static SortedDictionary<string, Stock> Stocks;
    }
}
