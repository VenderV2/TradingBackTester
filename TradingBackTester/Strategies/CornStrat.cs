using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBackTester.Models;

namespace TradingBackTester.Strategies
{
    internal class CornStrat : StrategyBaseClass
    {
        public override void Run(Account account, HistoricalBar currentBar, List<HistoricalBar> previousBars, Contract contract)
        {
            Account = account;
            CurrentBar = currentBar;
            PreviousBars = new List<HistoricalBar>();
            PreviousBars = previousBars;
            Contract = contract;

            if (account.OpenPositions.Count == 0)
            {
                if (currentBar.Date.Month == 1)
                {
                    Position position = new Position(1, currentBar, currentBar.Open, 100, contract, currentBar.Date, "BUY", account, 0, currentBar.Close * 10000);
                }
            }
            if (account.OpenPositions.Count > 0)
            {
                if (currentBar.Date.Month == 6)
                {
                    foreach (var pos in account.OpenPositions.ToList())
                    {
                        pos.ClosePosition();
                    }
                }
            }
        }
    }
}
