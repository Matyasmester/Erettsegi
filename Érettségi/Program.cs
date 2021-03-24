using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Erettsegi
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Feladatlap: https://dload-oktatas.educatio.hu/erettsegi/feladatok_2016tavasz_emelt/e_inf_16maj_fl.pdf 
             * Forras: https://www.oktatas.hu/kozneveles/erettsegi/feladatsorok/emelt_szint_2016tavasz/emelt_7nap
             */

            List<string> lines = File.ReadAllLines(@"C:\Cs Projects\Érettségi\srces\4_Otszaz\penztar.txt").ToList(); 

            int len = lines.Count;

            int nPayments = 0;

            int firstPayment = 0;

            List<List<string>> payments = new List<List<string>>();

            int temp = 0;

            for(int i = 0; i < len; i++)
            {
                if (lines[i].Equals("F"))
                {
                    if (nPayments == 0) firstPayment = i;
                    nPayments++;

                    List<string> payment = new List<string>();

                    for (int j = temp; j < i; j++)
                    {
                        payment.Add(lines[j]);
                    }
                    temp = i;

                    payments.Add(payment);
                }
            }

            int nItemsInFirstCart = firstPayment;

            Console.WriteLine($"2. feladat | Összesen ennyiszer fizettek a pénztárnál: {nPayments}");

            Console.WriteLine($"3. feladat | Az első vásárló kosarában levő árucikkek száma: {nItemsInFirstCart}");

            Console.WriteLine($"4. feladat | Egy vásárlás sorszáma: ");

            int serialNum = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.WriteLine($"4. feladat | Egy árucikk: ");

            string item = Console.ReadLine();

            Console.WriteLine($"4. feladat | Egy darabszám: ");

            int num = Convert.ToInt32(Console.ReadLine());

            int firstPurchaseOf = -1;

            int lastPurchaseOf = 0;

            foreach (List<string> payment in payments)
            {
                if (payment.Contains(item) && firstPurchaseOf == -1)
                {
                    firstPurchaseOf = payments.IndexOf(payment);
                }
                else if(payment.Contains(item)) lastPurchaseOf = payments.IndexOf(payment); 
            }

            int nPurchasesOfItem = lines.Count(x => x.Equals(item));

            Console.WriteLine($"5. a) feladat | Első vásárlás sorszáma: {firstPurchaseOf + 1}");

            Console.WriteLine($"5. a) feladat | Utolsó vásárlás sorszáma: {lastPurchaseOf + 1}");

            Console.WriteLine($"5. b) feladat | Ennyiszer vásárolták: {nPurchasesOfItem}");

            Console.WriteLine($"6. b) feladat | {num} darab esetén fizetendő: {ertek(num)}");

            Console.WriteLine($"7. feladat ");

            payments[serialNum].RemoveAll(x => x.Equals("F"));

            foreach (string boughtItem in payments[serialNum])
            {
                List<string> current = payments[serialNum];
                int nItems = current.Count(x => x.Equals(boughtItem));

                Console.WriteLine($"{nItems} {boughtItem}");
            }

            string path = @"C:\Cs Projects\Érettségi\srces\4_Otszaz\osszeg.txt";

            foreach (List<string> payment in payments)
            {
                int total = 0;
                string toWrite = (payments.IndexOf(payment) + 1).ToString() + ":";

                foreach (string bought in payment)
                { 
                    int nItems = payment.Count(x => x.Equals(bought));

                    total += ertek(nItems);
                }

                toWrite += total.ToString() + "\n";
                File.AppendAllText(path, toWrite);
            }

            Console.Read();
        }

        private static int ertek(int db)
        {
            int basePrice = 500;

            int downgrade = 50;

            int totalprice = 0;

            if (db > 3)
            {
                totalprice += basePrice + (basePrice - downgrade) + (basePrice - 2 * downgrade);
                basePrice -= 2 * downgrade;
                totalprice += (db - 3) * basePrice;
            }
            else if (db == 3)
            {
                totalprice += basePrice + (basePrice - downgrade) + (basePrice - 2 * downgrade);
            }
            else if (db == 2)
            {
                totalprice += basePrice + (basePrice - downgrade);
            }
            else
            {
                totalprice += basePrice;
            }

            return totalprice;
        }
    }
}
