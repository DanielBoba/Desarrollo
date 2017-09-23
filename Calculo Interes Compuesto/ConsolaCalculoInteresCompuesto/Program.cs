using System;

namespace ConsolaCalculoInteresCompuesto
{
    class Program
    {
        static void Main(string[] args)
        {
            double capital = 1;

            double tasa_de_interes_diaria = 0.007;

            int n;

            for (n = 1; n < 365 ; n++)
            {
                capital = capital + (capital * tasa_de_interes_diaria);
            }


            Console.WriteLine(capital.ToString());

            Console.ReadKey();
        }
    }
}
