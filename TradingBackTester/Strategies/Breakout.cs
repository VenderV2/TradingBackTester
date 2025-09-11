using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBackTester.Models;

namespace TradingBackTester.Strategies
{
    class Breakout : StrategyBaseClass
    {
        public override void Run(Account account, HistoricalBar currentBar, List<HistoricalBar> previousBars, Contract contract)
        {
            Account = account;
            CurrentBar = currentBar;
            PreviousBars = new List<HistoricalBar>();
            PreviousBars = previousBars;
            Contract = contract;
            //decimal stopMulti = 0.2m;
            //decimal tpMulti = 0.6m;
        }
    }
}
