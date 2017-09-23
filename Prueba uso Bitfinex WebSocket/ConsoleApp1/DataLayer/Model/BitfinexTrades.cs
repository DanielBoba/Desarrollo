using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class BitfinexTrades
    {
        const string quote = "\"";
        public Nullable<Int64> MTS { get; set; }
        public Nullable<int> BitfinexId { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<int> Period { get; set; }

        public static string DevuelveMensajeSuscribe ()
        {
            string mensaje = "{ " +
                     quote + "event" + quote + ":" + quote + "subscribe" + quote + "," +
                     quote + "channel" + quote + ":" + quote + "trades" + quote + "," +
                     quote + "symbol" + quote + ":" + quote + "tBTCUSD" + quote + " }";

            return mensaje ;
        }

        
    }
}
