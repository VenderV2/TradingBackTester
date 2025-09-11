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
    class BuyOverDuration : StrategyBaseClass
    {
        int i = 1;

        public override void Run(Account account, HistoricalBar currentBar, List<HistoricalBar> previousBars, Contract contract)
        {
            if (i == 1)
            {
                //i = 0;
                Position position = new Position(1, currentBar, currentBar.Close, 1, contract, currentBar.Date, "BUY", account, 0, currentBar.Close * 10000);
                //account.OpenPositions.Add(position);
                //return "BUY";
            }
            else if (i == 140)
            {
                //account.OpenPositions[0].ClosePosition();
                //Position position = new Position(2, currentBar, 2, contract, currentBar.Date, "SELL", account);
                //account.OpenPositions.Add(position);
                //return "SELL";
            }
            i++;
        }
    }
}
