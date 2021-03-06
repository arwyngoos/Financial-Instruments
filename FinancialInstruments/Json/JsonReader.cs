﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FinancialInstruments.FinancialProducts;
using FinancialInstruments.Utilities;
using Newtonsoft.Json.Linq;

namespace FinancialInstruments.Json
{
    public static class JsonReader
    {
        public static SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> ReadJsonFiles()
        {
            string folder = Settings.StockDataDirectory;

            if (Settings.InputDataType != Enums.InputDataType.Json)
            {
                throw new Exception(
                    $"Failed: the data type is {Settings.InputDataType.ToString()} but the Json reader process is called");
            }

            List<string> fileNames = Utils.GetFileNames(folder, Settings.InputDataType);

            SortedDictionary<string, SortedDictionary<DateTime, StockObservation>> instrumentsObservations =
                new SortedDictionary<string, SortedDictionary<DateTime, StockObservation>>();

            foreach (string fileName in fileNames)
            {
                JObject jObject = JObject.Parse(File.ReadAllText($"{folder}\\{fileName}"));
                instrumentsObservations.Add(fileName.Replace(".json", ""), ParseJObject(jObject));
            }

            return instrumentsObservations;
        }

        private static SortedDictionary<DateTime, StockObservation> ParseJObject(JObject jObject)
        {
            SortedDictionary<DateTime, StockObservation> stockObservation = new SortedDictionary<DateTime, StockObservation>();

            List<JProperty> properties = jObject.Children().Select(x => (JProperty) x).ToList();
            JProperty timeSeriesData = properties.Single(x => x.Name.Contains("Time Series"));
            JToken timeSeriesContent = timeSeriesData.Children().Single();

            foreach (var jToken in timeSeriesContent)
            {
                var dataPoint = (JProperty) jToken;
                stockObservation.Add(DateTime.Parse(dataPoint.Name), ParseStockObservation(dataPoint));
            }

            return stockObservation;
        }

        private static StockObservation ParseStockObservation(JProperty dataPoint)
        {
            List<double> values = new List<double>();

            foreach (JToken field in dataPoint.Value)
            {
                values.Add(field.Values<double>().Single());
            }

            return new StockObservation(DateTime.Parse(dataPoint.Name), values[0], values[1], values[2], values[3], (int)values[4]);
        }
    }
}
