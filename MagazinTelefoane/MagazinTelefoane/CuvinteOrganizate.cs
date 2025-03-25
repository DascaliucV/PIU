using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MagazinTelefoane
{
    public class CuvinteOrganizate
    {
        public static List<string>[] OrganizeazaCuvintePeLitere(string caleFisier)
        {
           
            string[] cuvinte = CitesteCuvinteDinFisier(caleFisier);

           
            List<string>[] cuvintePeLitera = new List<string>[26]; 

            
            for (int i = 0; i < 26; i++)
            {
                cuvintePeLitera[i] = new List<string>();
            }

            // Organizăm cuvintele pe litere
            foreach (var cuvant in cuvinte)
            {
                if (!string.IsNullOrWhiteSpace(cuvant))
                {
                    char primaLitera = char.ToLower(cuvant[0]);
                    if (primaLitera >= 'a' && primaLitera <= 'z')
                    {
                        cuvintePeLitera[primaLitera - 'a'].Add(cuvant); 
                    }
                }
            }

            return cuvintePeLitera;
        }

        
        private static string[] CitesteCuvinteDinFisier(string caleFisier)
        {
            try
            {
                return File.ReadAllLines(caleFisier);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la citirea fișierului: {ex.Message}");
                return new string[] { };
            }
        }

        
        public static void AfiseazaCuvinte(List<string>[] cuvintePeLitera)
        {
            for (int i = 0; i < 26; i++)
            {
                if (cuvintePeLitera[i].Count > 0)
                {
                    Console.WriteLine($"Cuvinte care încep cu '{(char)('a' + i)}':");
                    foreach (var cuvant in cuvintePeLitera[i])
                    {
                        Console.WriteLine(cuvant);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
