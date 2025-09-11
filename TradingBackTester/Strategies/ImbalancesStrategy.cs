using IBApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TradingBackTester.messages;
using TradingBackTester.Models;

namespace TradingBackTester.Strategies
{
    class ImbalancesStrategy : StrategyBaseClass
    {
        List<FVGImbalance> Imbalances = new List<FVGImbalance>();
        //Account Account { get; set; }
        //HistoricalDataMessage CurrentBar { get; set; }
        //List<HistoricalDataMessage> PreviousBars { get; set; } = new List<HistoricalDataMessage>();
        //Contract Contract { get; set; }

        public ImbalancesStrategy()
        {
             
        }

        public override void Run(Account account, HistoricalBar currentBar, List<HistoricalBar> previousBars, Contract contract)
        {
            Account = account;
            CurrentBar = currentBar;
            PreviousBars = new List<HistoricalBar>();
            PreviousBars = previousBars;
            Contract = contract;

            CheckForPriceInZone();
            RemoveFilledGaps();
            CheckForNewGap();
        }
        private void CheckForPriceInZone()
        {
            FVGImbalance fvg = Imbalances.Find(x => x.UpperPrice > CurrentBar.Close && x.LowerPrice < CurrentBar.Close);
            if (fvg == null)
            {
                fvg = Imbalances.Find(x => x.UpperPrice > CurrentBar.Low && x.LowerPrice < CurrentBar.Low);
            }
            if (fvg == null)
            {
                fvg = Imbalances.Find(x => x.UpperPrice > CurrentBar.High && x.LowerPrice < CurrentBar.High);
            }
            if (fvg == null)
            {
                fvg = Imbalances.Find(x => Enumerable.Range((int)(CurrentBar.Open * 100), (int)CurrentBar.Close * 100).Contains((int)x.LowerPrice*100) && Enumerable.Range((int)(CurrentBar.Open * 100), (int)CurrentBar.Close * 100).Contains((int)x.UpperPrice * 100));
            }
            

            if (fvg != null)
            {
                if (fvg.Side == "LONG")
                {
                    Position position = new Position(1, CurrentBar, fvg.UpperPrice, 1, Contract, CurrentBar.Date, "BUY", Account, fvg.LowerPrice * 0.99m, fvg.UpperPrice * 1.01m);
                    //Account.OpenPositions.Add(position);
                    Imbalances.Remove(fvg);
                }
                else if (fvg.Side == "SHORT")
                {
                    Position position = new Position(1, CurrentBar, fvg.LowerPrice, 1, Contract, CurrentBar.Date, "SELL", Account, fvg.UpperPrice * 0.99m, fvg.LowerPrice * 1.01m);
                    //Account.OpenPositions.Add(position);
                    Imbalances.Remove(fvg);
                }
            }
        }
        private void RemoveFilledGaps()
        {
            foreach (var gap in Imbalances.ToList())
            {
                if (gap.Side == "LONG" && CurrentBar.Close < gap.LowerPrice)
                {
                    Imbalances.Remove(gap);
                }
                else if (gap.Side == "SHORT" && CurrentBar.Close > gap.UpperPrice)
                {
                    Imbalances.Remove(gap);
                }
            }
        }
        private void CheckForNewGap()
        {
            decimal gapSize = 0.1m;

            if (PreviousBars.Count >= 3)
            {
                var diff = ((CurrentBar.Close - PreviousBars[1].Close) / (CurrentBar.Close)) * 100;
                if (diff <= -gapSize)
                {
                    var fvg = new FVGImbalance(PreviousBars[1].Close, CurrentBar.Close, "SHORT");
                    Imbalances.Add(fvg);
                }
                else if (diff >= gapSize)
                {
                    var fvg = new FVGImbalance(PreviousBars[1].Close, CurrentBar.Close, "LONG");
                    Imbalances.Add(fvg);
                }
            }
        }
    }
}
