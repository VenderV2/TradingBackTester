using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBackTester.messages;
using TradingBackTester.Models;
using IBApi;

namespace TradingBackTester.Strategies
{
    abstract class StrategyBaseClass
    {
        public Account Account { get; set; }
        //public HistoricalDataMessage CurrentBar { get; set; }
        //public List<HistoricalDataMessage> PreviousBars { get; set; } = new List<HistoricalDataMessage>();
        public HistoricalBar CurrentBar { get; set; }
        public List<HistoricalBar> PreviousBars { get; set; } = new List<HistoricalBar>();
        public Contract Contract { get; set; }

        public abstract void Run(Account account, HistoricalBar currentBar, List<HistoricalBar> previousBars, Contract contract);
    }
}
