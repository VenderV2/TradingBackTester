using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skender.Stock.Indicators;

namespace TradingBackTester.Models
{
    public class HistoricalBar : IQuote
    {
        protected string ticker;
        protected DateTime date;
        protected decimal open;
        protected decimal high;
        protected decimal low;
        protected decimal close;
        protected decimal volume;
        public string Ticker 
        {
            get { return ticker; }
            set { ticker = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public decimal Open
        {
            get { return open; }
            set { open = value; }
        }


        public decimal High
        {
            get { return high; }
            set { high = value; }
        }

        public decimal Low
        {
            get { return low; }
            set { low = value; }
        }

        public decimal Close
        {
            get { return close; }
            set { close = value; }
        }

        public decimal Volume
        {
            get { return volume; }
            set { volume = value; }
        }
    }
}
