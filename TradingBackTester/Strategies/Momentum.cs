using IBApi;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBackTester.messages;
using TradingBackTester.Models;

namespace TradingBackTester.Strategies
{
    class Momentum : StrategyBaseClass
    {
        public override void Run(Account account, HistoricalBar currentBar, List<HistoricalBar> previousBars, Contract contract)
        {
            Account = account;
            CurrentBar = currentBar;
            PreviousBars = new List<HistoricalBar>();
            PreviousBars = previousBars;
            Contract = contract;
            decimal stopMulti = 0.2m;
            decimal tpMulti = 0.6m;
            List<RsiResult> results = previousBars.GetRsi(14).ToList();
            results.Reverse();

            if (previousBars.Count >= 3)
            {
                if (previousBars[0].Close > previousBars[0].Open && previousBars[1].Close > previousBars[1].Open && previousBars[2].Close > previousBars[2].Open && results.ElementAt(0).Rsi < 35)
                {
                    //if (previousBars[0].Volume > previousBars[1].Volume && previousBars[1].Volume > previousBars[2].Volume)
                    //{
                        Position _ = new Position(1, currentBar, currentBar.Open, 1, Contract, CurrentBar.Date, "BUY", Account, currentBar.Open - (currentBar.Open * stopMulti), currentBar.Open + (currentBar.Open * tpMulti));
                    //}
                }
                else if (previousBars[0].Close < previousBars[0].Open && previousBars[1].Close < previousBars[1].Open && previousBars[2].Close < previousBars[2].Open && results.ElementAt(0).Rsi > 65)
                {
                    //if (previousBars[0].Volume > previousBars[1].Volume && previousBars[1].Volume > previousBars[2].Volume)
                    //{
                        Position _ = new Position(1, currentBar, currentBar.Open, 1, Contract, CurrentBar.Date, "SELL", Account, currentBar.Open + (currentBar.Open * stopMulti), currentBar.Open - (currentBar.Open * tpMulti));
                    //}
                }
            }
        }
    }
}
