using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Providers
{
    public class BitfinexTradesProvider
    {
        List<BitfinexTrades> Trades ;
        DBCryptoExchangesEntities1 context;

        public BitfinexTradesProvider (DBCryptoExchangesEntities1 context)
        {
            this.context = context;
        }

        private bool MensajeEsTrade (string mensaje)
        {
            return mensaje.Contains("te\"");
        }
        public BitfinexTradesProvider  ()
        {
            Trades = new List<BitfinexTrades> ();
        }

        public void InsertarTrade (string mensaje)
        {
            if (MensajeEsTrade (mensaje))
            {
                char[] array = mensaje.ToCharArray();
                int n;
                char abre_llave = '[';
                char coma = ',';
                char cierraLlave = ']';
                int count_abre_llaves = 0;
                int numero_campo = 0;
                StringBuilder valor = new StringBuilder();
                bool leyendo = false;
                BitfinexTrades bitfinexTrade = new BitfinexTrades();
                var context = new DBCryptoExchangesEntities1();
                for (n = 0; n < array.Length; n++)
                {
                    if (array[n] == abre_llave)
                    {
                        count_abre_llaves++;
                        if (count_abre_llaves == 2)
                            leyendo = true;
                    }
                    else if ((leyendo) && (array[n] != coma) && (array[n] != cierraLlave))
                    {
                        valor.Append(array[n]);
                    }
                    else if ((leyendo) && ((array[n] == coma) || (array[n] == cierraLlave)))
                    {
                        leyendo = (array[n] != cierraLlave);

                        switch (numero_campo)
                        {
                            case 0:
                            {
                                bitfinexTrade.BitfinexId = Int32.Parse(valor.ToString());
                                break;            
                            };
                            case 1:
                            {
                                bitfinexTrade.MTS = Int64.Parse(valor.ToString());
                                break;
                            };
                            case 2:
                            {
                                bitfinexTrade.Amount = Double.Parse (valor.ToString());
                                break;
                            };
                            case 3:
                            {
                                bitfinexTrade.Price = Double.Parse(valor.ToString());
                                break;
                            };

                        }
                        numero_campo++;
                        valor.Clear();
                    }

                }

             //   using (var transaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Chaos))
               // {
                    var trade = context.Set<BitfinexTrades>();
                    context.BitfinexTrades.Add(bitfinexTrade);
                    context.SaveChanges();
               // }

            }
        }
    }
}
