using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBackTester.Models
{
    public class FVGImbalance
    {
        public decimal UpperPrice { get; set; }
        public decimal LowerPrice { get; set; }
        public string Side { get; set; } = "LONG";//e.g "SUPPLY" or "DEMAND" / "LONG" or "SHORT"

        public FVGImbalance(decimal upperPrice, decimal lowerPrice, string side)
        {
            UpperPrice = upperPrice;
            LowerPrice = lowerPrice;
            Side = side;
        }
    }
}
