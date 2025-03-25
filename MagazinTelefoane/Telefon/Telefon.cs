using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class Telefon
    {
        public string Nume { get; set; }
        public decimal Pret { get; set; }
        public Culoare CULOARE { get; set; }
        public Memorie MEMORIE { get; set; }

        public Telefon(string nume, decimal pret, Culoare culoare, Memorie memorie)
        {
            Nume = nume;
            Pret = pret;
            CULOARE = culoare;
            MEMORIE = memorie;
        }

        public String AfisareDetaliiComplete()
        {
            return $"Nume: {Nume}, Pret: {Pret}, Culoare: {CULOARE}, Memorie: {MEMORIE}";
        }
    }

}
