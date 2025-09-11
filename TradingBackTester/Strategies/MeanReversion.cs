using IBApi;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBackTester.Models;

namespace TradingBackTester.Strategies
{
    class MeanReversion : StrategyBaseClass
    {
        public override void Run(Account account, HistoricalBar currentBar, List<HistoricalBar> previousBars, Contract contract)
        {
            Account = account;
            CurrentBar = currentBar;
            PreviousBars = new List<HistoricalBar>();
            PreviousBars = previousBars;
            Contract = contract;

            //if (account.OpenPositions.Count > 0)
            //{
            //    return;
            //}
            if (previousBars.Count > 149)
            {
                List<StdDevChannelsResult> results = previousBars.GetStdDevChannels(90).ToList();
                results.Reverse();

                if (previousBars[0].Close <= (decimal)results[0].LowerChannel.Value)
                {
                    Position _ = new Position(1, currentBar, currentBar.Open, 1, Contract, CurrentBar.Date, "BUY", Account, (decimal)currentBar.Open * 0.997m, (decimal)results[0].UpperChannel.Value);
                }
            }
        }
    }
}
