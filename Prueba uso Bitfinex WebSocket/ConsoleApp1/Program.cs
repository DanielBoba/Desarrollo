using DataLayer;
using DataLayer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace ConsoleApp1
{

  public class Program
    {
        const string quote = "\"";
        public static void Main(string[] args)
        {
            // Create a new instance of the WebSocket class.
            //
            // The WebSocket class inherits the System.IDisposable interface, so you can
            // use the using statement. And the WebSocket connection will be closed with
            // close status 1001 (going away) when the control leaves the using block.
            //
            // If you would like to connect to the server with the secure connection,
            // you should create a new instance with a wss scheme WebSocket URL.


            using (var nf = new Notifier())
         

            using (var ws = new WebSocket("wss://api.bitfinex.com/ws/2"))
            {
                DBCryptoExchangesEntities1 context = new DBCryptoExchangesEntities1();
                BitfinexTradesProvider bitfinexTradesProvider = new BitfinexTradesProvider(context);                
                // Set the WebSocket events.

                // ws.OnOpen += (sender, e) => ws.Send("ping");

                ws.OnMessage += (sender, e) =>
                {
                    // BitfinexTrades bitfinexTrade = new BitfinexTrades();
                    bitfinexTradesProvider.InsertarTrade(e.Data);
                        //                        context.BitfinexTrades.Add
                    Console.WriteLine("Server say: " + e.Data);

                    /*
                    nf.Notify(
                      new NotificationMessage
                      {
                          Summary = "WebSocket Message",
                          Body = !e.IsPing ? e.Data : "Received a ping.",
                          Icon = "notification-message-im"
                      }
                    );
                    */

                };

                ws.OnError += (sender, e) =>
                {

                    Console.WriteLine("Server say: " + e.Message);
                    nf.Notify(
                      new NotificationMessage
                      {
                          Summary = "WebSocket Error",
                          Body = e.Message,
                          Icon = "notification-message-im"
                      }
                    );
                };

                ws.OnClose += (sender, e) =>
                    nf.Notify(
                      new NotificationMessage
                      {
                          Summary = String.Format("WebSocket Close ({0})", e.Code),
                          Body = e.Reason,
                          Icon = "notification-message-im"
                      }
                    );
#if DEBUG
                // To change the logging level.
                ws.Log.Level = LogLevel.Trace;

                // To change the wait time for the response to the Ping or Close.
                //ws.WaitTime = TimeSpan.FromSeconds (10);

                // To emit a WebSocket.OnMessage event when receives a ping.
                //ws.EmitOnPing = true;
#endif
                // To enable the Per-message Compression extension.
                //ws.Compression = CompressionMethod.Deflate;

                // To validate the server certificate.
                /*
                ws.SslConfiguration.ServerCertificateValidationCallback =
                  (sender, certificate, chain, sslPolicyErrors) => {
                    ws.Log.Debug (
                      String.Format (
                        "Certificate:\n- Issuer: {0}\n- Subject: {1}",
                        certificate.Issuer,
                        certificate.Subject
                      )
                    );
                    return true; // If the server certificate is valid.
                  };
                 */

                // To send the credentials for the HTTP Authentication (Basic/Digest).
                //ws.SetCredentials ("nobita", "password", false);

                // To send the Origin header.
                //ws.Origin = "http://localhost:4649";

                // To send the cookies.
                //ws.SetCookie (new Cookie ("name", "nobita"));
                //ws.SetCookie (new Cookie ("roles", "\"idiot, gunfighter\""));

                // To connect through the HTTP Proxy server.
                //ws.SetProxy ("http://localhost:3128", "nobita", "password");

                // To enable the redirection.
                //ws.EnableRedirection = true;

                // Connect to the server.
                //Action<bool> completed = { true };
                ws.Connect();
                Thread.Sleep(1000);
                string mensaje = DataLayer.Model.BitfinexTrades.DevuelveMensajeSuscribe();
                ws.Send(mensaje);
                Thread.Sleep(1000);
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                {
                   // Thread.Sleep(1000);
                }

            }

        }            
        
    }
}