using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBackTester.messages;
using TradingBackTester.Models;

namespace TradingBackTester.Strategies
{
    class _50_50 : StrategyBaseClass
    {
        Random random = new Random();

        public override void Run(Account account, HistoricalBar currentBar, List<HistoricalBar> previousBars, Contract contract)
        {
            Account = account;
            CurrentBar = currentBar;
            //PreviousBars = new List<HistoricalDataMessage>();
            //PreviousBars = previousBars;
            Contract = contract;

            int r = random.Next(1, 3);
            if (r == 1)
            {
                Position position = new Position(1, currentBar, currentBar.Close, 1, Contract, CurrentBar.Date, "BUY", Account, currentBar.Close - (currentBar.Close * 0.1m), currentBar.Close + (currentBar.Close * 0.1m));
                //Account.OpenPositions.Add(position);
            }
            else
            {
                Position position = new Position(1, currentBar, currentBar.Close, 1, Contract, CurrentBar.Date, "SELL", Account, currentBar.Close + (currentBar.Close * 0.1m), currentBar.Close - (currentBar.Close * 0.1m));
                //Account.OpenPositions.Add(position);
            }
        }
    }
}
