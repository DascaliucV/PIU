using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class Persoana
    {
        public String Nume { get; set; }
        public String Prenume { get; set; }
        public String NumeComplet { get { return $"{Nume} {Prenume}"; } }
        public FormulaAdresare FORMULA_ADRESARE { get; set; }
        public Nationalitate NATIONALITATE { get; set; }
        public Persoana(String nume, String prenume, FormulaAdresare formulaAdresare, Nationalitate nationalitate)
        {
            Nume = nume;
            Prenume = prenume;
            FORMULA_ADRESARE = formulaAdresare;
            NATIONALITATE = nationalitate;
        }

        public String AfisareDetaliiComplete()
        {
            return $"Nume: {NumeComplet}, Formula de adresare: {FORMULA_ADRESARE}, Nationalitate: {NATIONALITATE}";
        }
    }
}
