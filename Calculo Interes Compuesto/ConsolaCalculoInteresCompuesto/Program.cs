using System;

namespace ConsolaCalculoInteresCompuesto
{
    class Program
    {
        static void Main(string[] args)
        {
            double capital = 10000;

            double tasa_de_interes_diaria = 0.0074;

            int n;

            for (n = 1; n < 730; n++)
            {
                capital = capital + (capital * tasa_de_interes_diaria);
            }


            Console.WriteLine(capital.ToString());

            Console.ReadKey();
        }
    }
}
