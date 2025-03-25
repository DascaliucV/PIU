using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public enum Culoare
    {
        Rosu = 1,
        Albastru,
        Verde,
        Galben,
        Portocaliu,
        Negru,
        Alb,
        Gri
    }

    public enum Memorie
    {
        _32GB = 1,
        _64GB,
        _128GB,
        _256GB,
        _512GB
    }

    public enum FormulaAdresare
    {
        Domnul = 1,
        Doamna,
        Domnisoara
    }

    [Flags]
    public enum Nationalitate
    {
        Romana = 1,
        Engleza = 2,
        Franceza = 4,
        Germana = 8,
        Spaniola = 16,
        Italiana = 32
    }
}
