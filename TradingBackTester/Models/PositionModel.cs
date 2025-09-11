using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;
using Newtonsoft.Json;
using TradingBackTester.messages;

namespace TradingBackTester.Models
{
    public class Position
    {
        public int Id { get; set; }
        [JsonIgnore]
        private Account _account;
        [JsonIgnore]
        public Account Account { get { return _account; } private set { if (_account == null) { _account = value; } } }
        public decimal EntryPrice { get; set; }
        //public HistoricalDataMessage EntryBar { get; set; }
        public HistoricalBar EntryBar { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalValue { get; set; }
        public Contract Contract { get; set; }
        public decimal PnLPercentage { get; set; }
        public decimal PnLTotal { get; set; }
        public DateTime EntryDate { get; set; }
        public string Type { get; set; }
        public decimal StopLoss { get; set; } = 0.00m;
        public decimal TakeProfit { get; set; } = 0.00m;


        public decimal CurrentPrice {  get; private set; }
        //private HistoricalDataMessage currentBar;
        //public HistoricalDataMessage CurrentBar { get { return currentBar; } set { currentBar = value; UpdateValues(); } }
        private HistoricalBar currentBar;
        public HistoricalBar CurrentBar { get { return currentBar; } set { currentBar = value; UpdateValues(); } }
        public string Status { get; private set; }

        public Position(int id, HistoricalBar entryBar, decimal entryPrice, int quantity, Contract contract, DateTime entryDateString, string orderType, Account account, decimal stopLimit, decimal takeProfit)
        {

            if (account.BuyingPower < entryPrice)
            {
                //Console.WriteLine("--------> Insufficient funds");
                account.InsufficientFundsReturns++;
                return;
            }
            if (orderType == "BUY")
            {
                account.LongsTaken++;
            }
            else if (orderType == "SELL")
            {
                account.ShortsTaken++;
            }
            Id = id;
            EntryBar = entryBar;
            EntryPrice = entryPrice;
            Quantity = quantity;
            Contract = contract;
            EntryDate = entryDateString;
            Type = orderType;
            StopLoss = stopLimit;
            TakeProfit = takeProfit;
            Account = account;

            TotalCost = Quantity * EntryPrice;
            CurrentPrice = EntryPrice;
            currentBar = EntryBar;

            OpenPosition();
        }
        //public Position(int id, double entryPrice, int quantity, Contract contract, DateTime entryDateString, string orderType, Account account, double stopLimit, double takeProfit)
        //{
        //    if (orderType == "BUY")
        //    {
        //        account.LongsTaken++;
        //    }
        //    else if (orderType == "SELL")
        //    {
        //        account.ShortsTaken++;
        //    }
        //    Id = id;
        //    EntryPrice = entryPrice;
        //    Quantity = quantity;
        //    Contract = contract;
        //    EntryDate = entryDateString;
        //    Type = orderType;
        //    StopLoss = stopLimit;
        //    TakeProfit = takeProfit;
        //    Account = account;

        //    TotalCost = Quantity * EntryPrice;
        //    CurrentPrice = EntryPrice;

        //    account.TotalNumberOfTrades++;
        //    account.CommisionFees += (TotalCost * 0.005);
        //}

        private void UpdateValues()
        {
            CurrentPrice = CurrentBar.Close;
            // If current bar high/close is outisde of SL or TP, set CurrentPrice to the price of the SL or TP that was hit. Else CurrentPrice = bar.close.
            var signal = CheckStops(CurrentPrice, StopLoss, TakeProfit, CurrentBar);
            if (signal == "TPHIT")
            {
                CurrentPrice = TakeProfit;
            }
            else if (signal == "SLHIT")
            {
                CurrentPrice = StopLoss;
            }

            PnLTotal = (CurrentPrice - EntryPrice) * Quantity;
            if (Type == "SELL")
            {
                PnLTotal *= -1;
            }

            PnLPercentage = ((CurrentPrice - EntryPrice) / EntryPrice) * 100;
            TotalValue = Quantity * CurrentPrice;

            //Perform checks on the SL and TP to check if position needs to close itself.
            if (signal != "")
            {
                ClosePosition();
            }

            _account.UpdateAccountValues();
        }
        public void OpenPosition()
        {
            Account.TotalNumberOfTrades++;
            Account.CommisionFees += (TotalCost * 0.005m);
            Account.OpenPositions.Add(this);
            Status = "OPEN";
        }
        public void ClosePosition()
        {
            Account.UpdateAccountValues();

            Account.LiquidBalance += PnLTotal;
            Status = "CLOSED";
            Account.DestroyPositionInstance(this);
        }

        private string CheckStops(decimal price, decimal sl, decimal tp, HistoricalBar bar)
        {
            if (Type == "BUY")
            {
                if (price >= tp || bar.High >= tp)
                {
                    Account.TotalTakeProfitsHit++;
                    return "TPHIT";
                }
                else if (price <= sl || bar.Low <= sl)
                {
                    Account.TotalStopsHit++;
                    return "SLHIT";
                }
            }
            else if (Type == "SELL")
            {
                if (price <= tp || bar.Low <= tp)
                {
                    Account.TotalTakeProfitsHit++;
                    return "TPHIT";
                }
                else if (price >= sl || bar.High >= sl)
                {
                    Account.TotalStopsHit++;
                    return "SLHIT";
                }
            }


            return "";
        }
    }
}
