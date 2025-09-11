using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBackTester.messages;
using TradingBackTester.Models;

namespace TradingBackTester.Utility
{
    public class StreamReadHistoData
    {
        public static bool DataReady { get; private set; } = false;
        private static List<HistoricalBar> histoData = new List<HistoricalBar>();

        public static void Read(string filePath)
        {
            //List<HistoricalBar> histoData = new List<HistoricalBar>();
            histoData.Clear();
            try
            {
                // Create a StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    // Read line by line
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        HistoricalBar data = FromCsv(line);
                        histoData.Add(data);
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            DataReady = true;
            Console.WriteLine(histoData.Count);
            //return histoData;
        }
        public static List<HistoricalBar> GetData()
        {
            return histoData;
        }

        static HistoricalBar FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            HistoricalBar dailyValues = new HistoricalBar();
            dailyValues.Date = Convert.ToDateTime(values[0]);
            dailyValues.Open = Convert.ToDecimal(values[1]);
            dailyValues.High = Convert.ToDecimal(values[2]);
            dailyValues.Low = Convert.ToDecimal(values[3]);
            dailyValues.Close = Convert.ToDecimal(values[4]);
            dailyValues.Volume = Convert.ToDecimal(values[5]);
            return dailyValues;
        }
    }
}
