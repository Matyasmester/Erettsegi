using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Erettsegi
{
    class Program
    {
        /*
         * Feladatlap: https://dload-oktatas.educatio.hu/erettsegi/feladatok_2020osz_emelt/e_inf_20okt_fl.pdf
         * Forras: https://www.oktatas.hu/kozneveles/erettsegi/feladatsorok/emelt_szint_2020osz/emelt_8nap
         */

        static void Main()
        {
            List<string> lines = File.ReadAllLines(@"C:\Cs Projects\Érettségi\srces\4_Sorozatok\lista.txt").ToList();

            int nKnownReleaseDates = 0;

            float seen = lines.Count(x => x.Equals("1"));
            float max = seen + lines.Count(x => x.Equals("0"));

            int totalMins = 0;
            int nDays = 0;
            int nHours = 0;
            int nMins = 0;

            foreach (string line in lines)
            {
                int currI = lines.IndexOf(line);
                string next = lines[currI + 1];
                if (currI % 5 == 0 && !line.Equals("NI"))
                {
                    nKnownReleaseDates++;
                }
                if (next.Equals("1") || next.Equals("0"))
                {
                    totalMins += Convert.ToInt32(line);
                }
            }

            nDays = totalMins / 1440;
            totalMins -= nDays * 1440;

            nHours = totalMins / 60;
            totalMins -= nHours * 60;

            nMins = totalMins;

            float percentSeen = (seen / max) * 100;

            Console.WriteLine($"2. Feladat | Ennyi sorozatnak tudjuk kiadasi datumat: {nKnownReleaseDates}");
            Console.WriteLine($"3. Feladat | Ennyi szazalekat latta a sorozatoknak: {Math.Truncate(percentSeen * 100) / 100}%");
            Console.WriteLine($"4. Feladat | Sorozatnezessel ennyi idot toltott el: {nDays} nap {nHours} óra {nMins} perc.");

            Console.WriteLine("5. Feladat | Egy datum eeee.hh.nn formatumban:");
            string date = Console.ReadLine();

            HashSet<string> pairs = new HashSet<string>();

            int year = Convert.ToInt32(date.Substring(0, 4));

            int month = Convert.ToInt32(date.Substring(5, 2));

            int day = Convert.ToInt32(date.Substring(8, 2));

            foreach (string line in lines)
            {
                int currI = lines.IndexOf(line);
                if (currI % 5 == 0 && !line.Equals("NI"))
                {
                    int currYear = Convert.ToInt32(line.Substring(0, 4));

                    int currMonth = Convert.ToInt32(line.Substring(5, 2));

                    int currDay = Convert.ToInt32(line.Substring(8, 2));

                    if (year >= currYear && day >= currDay && month >= currMonth)
                    {
                        string str = lines[currI + 2] + "   " + lines[currI + 1];
                        pairs.Add(str);
                    }
                }
            }

            foreach (string str in pairs)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine("7. Feladat | Adjon meg egy napot roviditett alakban (h, k, sze, cs, p, szo, v): ");

            string inputDay = Console.ReadLine();

            HashSet<string> names = new HashSet<string>();

            bool showAiredOnDay = false;

            foreach (string line in lines)
            {
                int currI = lines.IndexOf(line);
                if (currI % 5 == 0 && !line.Equals("NI"))
                {
                    int currYear = Convert.ToInt32(line.Substring(0, 4));

                    int currMonth = Convert.ToInt32(line.Substring(5, 2));

                    int currDay = Convert.ToInt32(line.Substring(8, 2));

                    if(hetnapja(currYear, currMonth, currDay) == inputDay)
                    {
                        names.Add(lines[currI + 1]);
                        showAiredOnDay = true;
                    }
                }
            }

            if (showAiredOnDay) {
                foreach (string str in names)
                {
                    Console.WriteLine(str);
                }
            }
            else
            {
                Console.WriteLine("Az adott napon nem kerül adasba sorozat.");
            }

            string hetnapja(int ev, int ho, int nap)
            {
                string[] napok = { "v", "h", "k", "sze", "cs", "p", "szo" };
                int[] honapok = { 0, 3, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4 };

                if (ho < 3) ev -= 1;

                return napok[(ev + ev / 4 - ev / 100 + ev / 400 + honapok[ho - 1] + nap) % 7];
            }

            Console.Read();
        }
    }
}
