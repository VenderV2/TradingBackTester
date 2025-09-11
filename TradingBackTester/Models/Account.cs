using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBackTester.Models
{
    public class Account : ICloneable
    {
        public DateTime Date { get; set; }
        public decimal InitialBalance { get; } = 100000.00m;
        public decimal LiquidBalance { get; set; } // Excludes unrealised PnL and margin requirments
        public decimal BuyingPower { get; private set; } // Includes margin requirments
        public decimal MarginRequirments { get; private set; }
        public decimal AccountBalance { get { return LiquidBalance + UnrealisedPnL - CommisionFees; } }
        public decimal RealisedPnL { get; private set; }
        public decimal UnrealisedPnL {  get; private set; }
        public decimal TotalPnLPercentage { get; private set; }
        [JsonIgnore]
        public List<Position> OpenPositions { get; set; } = new List<Position>();
        public int NumberOfOpenPositions { get { return OpenPositions.Count; } }
        public int TotalNumberOfTrades { get; set; }
        public int ShortsTaken { get; set; }
        public int LongsTaken { get; set; }
        public int TotalStopsHit { get; set; }
        public int TotalTakeProfitsHit { get; set; }
        public decimal CommisionFees { get; set; }
        public int InsufficientFundsReturns { get; set; }
        public decimal WinPercentage { get 
            { 
                if ((decimal)(TotalStopsHit + TotalTakeProfitsHit) == 0) { return 0.00m; }
                decimal wr = ((decimal)TotalTakeProfitsHit / (decimal)(TotalStopsHit + TotalTakeProfitsHit)) * 100m;
                return wr;
            } 
        }

        //private static readonly Account instance = new Account();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        //static Account()
        //{
        //}

        public Account()
        {
            LiquidBalance = InitialBalance;
            BuyingPower = LiquidBalance;
        }

        //public static Account Instance
        //{
        //    get
        //    {
        //        return instance;
        //    }
        //}
        public void UpdateAccountValues()
        {
            decimal unrealisedPnL = 0;
            decimal marginReq = 0;
            foreach (var position in OpenPositions)
            {
                unrealisedPnL += position.PnLTotal;
                marginReq += position.EntryPrice;
            }

            MarginRequirments = marginReq;
            BuyingPower = LiquidBalance - MarginRequirments;
            RealisedPnL = LiquidBalance - InitialBalance;
            UnrealisedPnL = unrealisedPnL;
            TotalPnLPercentage = ((AccountBalance - InitialBalance) / InitialBalance) * 100;
        }
        public void DestroyPositionInstance(Position positionRef)
        {
            //var pos = OpenPositions.Find(r => r == positionRef);
            var index = OpenPositions.IndexOf(positionRef);
            OpenPositions.RemoveAt(index);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
