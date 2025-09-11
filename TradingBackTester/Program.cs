using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingBackTester.messages;
using TradingBackTester.Utility;


namespace TradingBackTester
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


            //  CONNECTION STRING FOR DEV DB          Server=localhost;Database=master;Trusted_Connection=True;




    //        BULK INSERT[dbo].[ES_full_1min_continuous_absolute_adjusted]
    //        FROM 'D:\Futures Historical Data - firstratedata\futures_full_1min_contin_adj_absolute\ES_full_1min_continuous_absolute_adjusted.txt'
    //WITH
    //(
    // BATCHSIZE = 1,
    // ROWS_PER_BATCH = 1,
    //FIRSTROW = 2,
    //FIELDTERMINATOR = ',', --CSV field delimiter
    //ROWTERMINATOR = '\n', --Use to shift the control to next row
    //TABLOCK
    //)
        }
    }
}
