using System;
using System.Collections.Generic;
using System.Linq;
using Modele;

namespace MagazinTelefoane
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Numarul de persoane introduse: ");

            int numarPersoane;
            if (!int.TryParse(Console.ReadLine(), out numarPersoane) || numarPersoane <= 0)
            {
                Console.WriteLine("\nNumar invalid. Iesire din program.\n");
                return;
            }

            Persoana[] persoane = new Persoana[numarPersoane];

            for (int i = 0; i < numarPersoane; i++)
            {
                Console.WriteLine($"\nPersoana {i + 1}:");
                Console.Write("Introduceti numele persoanei: ");
                string nume = Console.ReadLine();
                Console.Write("Introduceti prenumele persoanei: ");
                string prenume = Console.ReadLine();
                Console.Write("Intoduceti formula de adresare:\n" +
                    "1. Domnul,\r\n" +
                    "2. Doamna,\r\n" +
                    "3. Domnisoara\r\n\n");

                int formulaAdresare;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out formulaAdresare) && formulaAdresare >= 1 && formulaAdresare <= 3)
                        break;
                    Console.WriteLine("Formula de adresare invalida. Introduceti un numar intre 1 si 3.\n");
                }

                Console.Write("Introduceti nationalitatea:\n" +
                    "1. Romana,\r\n" +
                    "2. Engleza,\r\n" +
                    "4. Franceza,\r\n" +
                    "8. Germana,\r\n" +
                    "16. Spaniola,\r\n" +
                    "32. Italiana\r\n\n");

                int nationalitate;
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out nationalitate) && nationalitate >= 1 && nationalitate <= 63)
                        break;
                    Console.WriteLine("Nationalitate invalida. Introduceti un numar intre 1 si 63.\n");
                }

                persoane[i] = new Persoana(nume, prenume, (FormulaAdresare)(formulaAdresare), (Nationalitate)(nationalitate));
            }

            Console.WriteLine("\nPersoanele introduse sunt:");
            for (int i = 0; i < numarPersoane; i++)
            {
                Console.WriteLine($"{i + 1}. {persoane[i].AfisareDetaliiComplete()}");
            }

            Console.WriteLine("\n\nIntroduceti numele persoanei pentru cautare:");
            string numeCautat = Console.ReadLine();
            bool persoanaGasita = false;
            for (int i = 0; i < numarPersoane; i++)
            {
                if (persoane[i].NumeComplet.Contains(numeCautat))
                {
                    Console.WriteLine($"Persoana cautata este: {persoane[i].AfisareDetaliiComplete()}\n");
                    persoanaGasita = true;
                }
            }

            if (!persoanaGasita)
            {
                Console.WriteLine("Persoana nu a fost gasita.\n");
            }

            Magazin magazin = new Magazin();
            
            Console.Write("Cate telefoane doriti sa introduceti ?\n");
            if (!int.TryParse(Console.ReadLine(), out int numarTelefoane) || numarTelefoane <= 0)
            {
                Console.WriteLine("\nNumar invalid. Iesire din program.\n");
                return;
            }

            for (int i = 0; i < numarTelefoane; i++)
            {
                Console.WriteLine($"\nTelefonul {i + 1}:");
                Console.Write("Introduceti numele telefonului:\n");
                string nume = Console.ReadLine();

                int culoare;
                while (true)
                {
                    Console.Write("Introduceti culoarea telefonului:\n" +
                        "1. Rosu,\r\n" +
                        "2. Albastru,\r\n" +
                        "3. Verde,\r\n" +
                        "4. Galben,\r\n" +
                        "5. Portocaliu,\r\n" +
                        "6. Negru,\r\n" +
                        "7. Alb,\r\n" +
                        "8. Gri\r\n\n");
                    if (int.TryParse(Console.ReadLine(), out culoare) && culoare >= 1 && culoare <= 8)
                        break;
                    Console.WriteLine("Culoare invalida. Introduceti un numar intre 1 si 8.\n");
                }

                int memorie;
                while (true)
                {
                    Console.Write("Introduceti memoria telefonului:\n" +
                        "1. 32GB,\r\n" +
                        "2. 64GB,\r\n" +
                        "3. 128GB,\r\n" +
                        "4. 256GB,\r\n" +
                        "5. 512GB\r\n\n");
                    if (int.TryParse(Console.ReadLine(), out memorie) && memorie >= 1 && memorie <= 5)
                        break;
                    Console.WriteLine("Memorie invalida. Introduceti un numar intre 1 si 5.\n");
                }

                decimal pret;
                while (true)
                {
                    Console.Write("\nIntroduceti pretul telefonului: \n");
                    if (decimal.TryParse(Console.ReadLine(), out pret) && pret > 0)
                        break;
                    Console.WriteLine("\nPret invalid. Introduceti un numar pozitiv.\n");
                }

                magazin.AdaugaTelefon(new Telefon(nume, pret, (Culoare)(culoare), (Memorie)(memorie)));
            }

            Console.WriteLine("\nLista telefoanelor disponibile:");
            AfiseazaTelefoane(magazin.telefoane);

            Console.Write("\nIntroduceti pretul pentru cautare: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal cautaPret) && cautaPret > 0)
            {
                var telefoaneGasite = magazin.CautaDupaPret(cautaPret);
                if (telefoaneGasite.Any())
                {
                    Console.WriteLine("\nTelefoane gasite după pretul introdus:");
                    AfiseazaTelefoane(telefoaneGasite);
                }
                else
                {
                    Console.WriteLine("\nNu au fost găsite telefoane cu acest pret.\n");
                }
            }
            else
            {
                Console.WriteLine("\nPret invalid pentru cautare.\n");
            }
        }

        static void AfiseazaTelefoane(List<Telefon> telefoane)
        {
            if (telefoane == null || !telefoane.Any())
            {
                Console.WriteLine("Nu există telefoane disponibile.");
                return;
            }

            foreach (var telefon in telefoane)
            {
                Console.WriteLine(telefon.AfisareDetaliiComplete());
            }
        }
    }
}
