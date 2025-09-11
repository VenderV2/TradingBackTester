using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using TradingBackTester.messages;
using TradingBackTester.Models;
using TradingBackTester.Strategies;
using Newtonsoft.Json;
using System.Drawing;

namespace TradingBackTester
{
    class BackTest
    {
        private Control uiControl { get; set; }
        public Account Account { get; private set; }
        //public List<HistoricalDataMessage> HistoricalData { get; private set; } = new List<HistoricalDataMessage>();
        public List<HistoricalBar> HistoricalData { get; private set; } = new List<HistoricalBar>();
        public InstrumentHistoricalData InstrumentData { get; private set; }
        public List<HistoricalBar> BenchmarkData { get; private set; } = new List<HistoricalBar>();
        private StrategyBaseClass Strategy;
        private Contract Contract;
        private List<Account> AccountSnapshots = new List<Account>();

        public BackTest(List<HistoricalBar> historicalData , List<HistoricalBar> benchmarkData, Contract contract, Type strategyType, Chart chartControl)
        {
            Account = new Account();
            HistoricalData = historicalData;
            BenchmarkData = benchmarkData;
            Contract = contract;
            uiControl = chartControl;

            Clear();

            Strategy = (StrategyBaseClass)Activator.CreateInstance(strategyType);
        }

        public void Start()
        {
            for (var i = 0; i < HistoricalData.Count; i++)
            {
                var p = new HistoricalBar[i];
                Array.Copy(HistoricalData.ToArray(), 0, p, 0, i);
                List<HistoricalBar> prevBars = p.ToList();
                prevBars.Reverse();

                UpdatePositions(HistoricalData[i]);
                Strategy.Run(Account, HistoricalData[i], prevBars, Contract);

                SnapshotEquityEoD(HistoricalData[i].Date);
            }

            PaintEquityCurve();
        }

        private void UpdatePositions(HistoricalBar bar)
        {
            foreach (var position in Account.OpenPositions.ToList())
            {
                position.CurrentBar = bar;
                //pass current price to all Indicators
            }
        }
        private void SnapshotEquityEoD(DateTime date)
        {
            Account.Date = date;
            //string json = JsonConvert.SerializeObject(Account);
            //Account acc = JsonConvert.DeserializeObject<Account>(json);
            AccountSnapshots.Add((Account)Account.Clone());
        }
        private void PaintEquityCurve()
        {
            if (uiControl != null)
            {
                Chart equityCurveChart = (Chart)uiControl;
                equityCurveChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
                equityCurveChart.ChartAreas[0].AxisX.LineColor = Color.Black;
                //equityCurveChart.ChartAreas[0].AxisY.Crossing = (double)Account.InitialBalance;
                equityCurveChart.ChartAreas[0].AxisY.Crossing = 0;
                equityCurveChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                equityCurveChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                equityCurveChart.ChartAreas[0].AxisX.ScaleBreakStyle.Enabled = true;
                equityCurveChart.ChartAreas[0].AxisX.ScaleBreakStyle.CollapsibleSpaceThreshold = 10;
                equityCurveChart.ChartAreas[0].AxisX.ScaleBreakStyle.BreakLineStyle = BreakLineStyle.Straight;

                for (int i = 0; i < AccountSnapshots.Count; i++)
                {
                    equityCurveChart.Series[0].Points.AddXY(AccountSnapshots[i].Date, (AccountSnapshots[i].UnrealisedPnL + AccountSnapshots[i].RealisedPnL)); // There is something wrong with using accountBalance, it shows phantom results between closing a trade and opening the next.
                }
            }
        }
        private void Clear()
        {
            if (uiControl != null)
            {
                Chart historicalChart = (Chart)uiControl;
                historicalChart.Series[0].Points.Clear();
            }
        }
    }
}
