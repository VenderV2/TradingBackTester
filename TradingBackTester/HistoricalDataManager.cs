using TradingBackTester.messages;
using TradingBackTester.UI_Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using IBApi;

namespace TradingBackTester
{
    class HistoricalDataManager : DataManager
    {
        public const int HISTORICAL_ID_BASE = 30000000;
        public List<HistoricalDataMessage> historicalData { get; private set; } = new List<HistoricalDataMessage>();
        public Contract Contract { get; private set; }

        public HistoricalDataManager(IBClient client, Chart chart) : base(client, chart)
        {
            
        }
        public void AddRequest(Contract contract, string endDateTime, string durationString, string barSizeSetting, string whatToShow, int useRTH, int dateFormat, bool keepUpToDate)
        {
            Clear();
            ibClient.ClientSocket.reqHistoricalData(currentTicker + HISTORICAL_ID_BASE, contract, endDateTime, durationString, barSizeSetting, whatToShow, useRTH, 1, keepUpToDate, new List<TagValue>());
            Contract = contract;
        }

        public override void Clear()
        {
            //barCounter = -1;
            Chart historicalChart = (Chart)uiControl;
            historicalChart.Series[0].Points.Clear();
            //gridView.Rows.Clear();
            historicalData = new List<HistoricalDataMessage>();
            Contract = null;
        }

        public override void NotifyError(int requestId)
        {
            throw new NotImplementedException();
        }
        public void AddToHistoricalDataList(HistoricalDataMessage message)
        {
            historicalData.Add(message);
        }
    }
}
