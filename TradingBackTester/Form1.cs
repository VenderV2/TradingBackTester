using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TradingBackTester;
using System.Threading;
using IBApi;
using TradingBackTester.Models;
using TradingBackTester.Strategies;
using Newtonsoft.Json;
using TradingBackTester.messages;
using TradingBackTester.Utility;

namespace TradingBackTester
{
    public partial class Form1 : Form
    {
        public bool IsConnected { get; set; } = false;
        private string host = "";
        private int port = 4002;
        private string connectOptions = "+PACEAPI";
        private int clientId = 1;


        private IBClient ibClient;
        private HistoricalDataManager historicalDataManager;

        public Form1()
        {
            InitializeComponent();

            ibClient = new IBClient();
            historicalDataManager = new HistoricalDataManager(ibClient, historicalChart);



            ibClient.HistoricalData += historicalDataManager.AddToHistoricalDataList;

            Connect();

            //new Thread(() => { StreamReadHistoData.Read("D:\\Futures Historical Data - firstratedata\\futures_full_1min_contin_adj_absolute\\_dataTest.txt"); }) { IsBackground = true }.Start();
            //new Thread(() => { HistoDataDBManager.QueryDB("SELECT * FROM [TradingHistoricalData].[dbo].[1day_futures_adj_absolute] where Ticker = 'GC'"); }) { IsBackground = true }.Start();
        }
        private void Connect()
        {
            if (host == null || host.Equals(""))
                host = "127.0.0.1";
            try
            {
                ibClient.ClientSocket.SetConnectOptions(connectOptions);

                ibClient.ClientSocket.eConnect(host, port, clientId);

                var reader = new EReader(ibClient.ClientSocket, ibClient.Signal);

                reader.Start();

                new Thread(() => { while (ibClient.ClientSocket.IsConnected()) { ibClient.Signal.waitForSignal(); reader.processMsgs(); } }) { IsBackground = true }.Start();
            }
            catch (Exception e)
            {
                //HandleErrorMessage(new ErrorMessage(-1, -1, "Please check your connection attributes.", ""));
                Console.WriteLine(e);
            }
        }

        private void reqHistoricalData_btn_Click(object sender, EventArgs e)
        {
            Contract contract = ContractSamples.Apple();
            string endTime = "20250508-14:34:06";
            string duration = "7 D";
            string barSize = "1 min";
            string whatToShow = "TRADES";
            int outsideRTH = 0;
            historicalDataManager.AddRequest(contract, endTime, duration, barSize, whatToShow, outsideRTH, 1, false);
        }

        private void backTestData_Click(object sender, EventArgs e)
        {
            var data = HistoDataDBManager.QueryDB($"SELECT * FROM [TradingHistoricalData].[dbo].[1day_futures_adj_absolute] where Ticker = '{textBox1.Text}'");
            var benchmarkData = HistoDataDBManager.QueryDB($"SELECT * FROM [TradingHistoricalData].[dbo].[1day_futures_adj_absolute] where Ticker = '{textBox2.Text}'");
            //if (HistoDataDBManager.DataReady)
            //{
            //int size = 10000;
            //var data = HistoDataDBManager.GetData();
            //List<HistoricalBar> bars = new List<HistoricalBar>();
            //var r = new Random();
            //var startingIndex = r.Next(data.Count - 1 - size);
            //startingIndex = 0;
            //for (var i = 0; i < size; i++)
            //{
            //    bars.Add(data[startingIndex + i]);
            //}

            BackTest backtest = new BackTest(data, benchmarkData, historicalDataManager.Contract, typeof(MeanReversion), historicalChart);
                backtest.Start();

                Console.WriteLine("=================================================================================");

                    Console.WriteLine(JsonConvert.SerializeObject(backtest.Account));
            //}
        }
    }
}
