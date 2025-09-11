using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBackTester.Models
{
    public class InstrumentHistoricalData
    {
        public List<HistoricalBar> min_1 { get; set; }
        public List<HistoricalBar> min_5 { get; set; }
        public List<HistoricalBar> hour_1 { get; set; }
        public List<HistoricalBar> hour_4 { get; set; }
        public List<HistoricalBar> day_1 { get; set; }
    }
}
