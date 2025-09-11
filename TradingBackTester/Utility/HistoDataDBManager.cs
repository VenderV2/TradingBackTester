using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using TradingBackTester.messages;
using TradingBackTester.Models;

namespace TradingBackTester.Utility
{
    public class HistoDataDBManager
    {
        public static bool DataReady { get; private set; } = false;
        private static List<HistoricalBar> histoData = new List<HistoricalBar>();
        static string connectionString = "Data Source=DESKTOP-UOQM7Q6;Initial Catalog=TradingHistoricalData;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static void QueryDBAsync(string queryString)
        {
            //List<HistoricalBar> histoData = new List<HistoricalBar>();
            histoData.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0]);
                            var bar = FromSql(reader);
                            histoData.Add(bar);
                        }
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
        public static List<HistoricalBar> QueryDB(string queryString)
        {
            List<HistoricalBar> hd = new List<HistoricalBar>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(queryString, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0]);
                            var bar = FromSql(reader);
                            hd.Add(bar);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            Console.WriteLine(hd.Count);
            return hd;
        }
        public static List<HistoricalBar> GetData()
        {
            return histoData;
        }

        static HistoricalBar FromSql(SqlDataReader reader)
        {
            HistoricalBar bar = new HistoricalBar();
            bar.Ticker = (string)reader[0];
            bar.Date = (DateTime)reader[1];
            bar.Open = (decimal)reader[2];
            bar.High = (decimal)reader[3];
            bar.Low = (decimal)reader[4];
            bar.Close = (decimal)reader[5];
            bar.Volume = (decimal)reader[6];
            return bar;
        }
    }
}
