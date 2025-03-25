using System;
using System.Collections.Generic;
using NivelStocareDate;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            // crearea unei liste de cuvinte goala
            List<String>[] cuvinteSortate = new List<String>[26];

            // Crearea unui obiect de tip AdministrareFisierText_Cuvinte
            AdministrareFisierText_Cuvinte administrareFisier = new AdministrareFisierText_Cuvinte();

            // Citirea cuvintelor din fisier
            List<String> cuvinteFisier = administrareFisier.GetCuvinte("cuvinte.txt");

            // Initializarea listei goale
            for (int i = 0; i < 26; i++)
            {
                // initializarea listei de cuvinte goala pentru litera i
                cuvinteSortate[i] = new List<String>();
            }

            // Sortarea cuvintelor in functie de prima litera
            foreach (String cuvant in cuvinteFisier)
            {
                // luam prima litera din cuvantul parcurs
                Char primaLitera = cuvant[0];
                // verificam daca prima litera este o litera mica
                if (primaLitera >= 'a' && primaLitera <= 'z')
                {
                    // adaugam cuvantul in lista corespunzatoare literei
                    cuvinteSortate[primaLitera - 'a'].Add(cuvant);
                }
                // acest caz pentru litera mare
                else if (primaLitera >= 'A' && primaLitera <= 'Z')
                {
                    // adaugam cuvantul in lista corespunzatoare literei
                    cuvinteSortate[primaLitera - 'A'].Add(cuvant);
                }
            }

            // Afisarea cuvintelor sortate
            for (int i = 0; i < 26; i++)
            {
                Console.Write("Litera: " + (char)(i + 'a') + "/" + (char)(i + 'A') + " <--->  ");
                // luam fiecare cuvant din lista de cuvinte corespunzatoare literei i
                foreach (String cuvant in cuvinteSortate[i])
                {
                    // Afisam cuvantul
                    Console.Write(cuvant + " ");
                }

                // Linie noua
                Console.WriteLine();
            }
        }
    }
}
 