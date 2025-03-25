using System.Collections.Generic;
using System.Linq;

namespace Modele 
{

    public class Magazin
    {
        public List<Telefon> telefoane { get; set; }

        public Magazin()
        {
            telefoane = new List<Telefon>();
        }

        public void AdaugaTelefon(Telefon telefon)
        {
            if (telefon != null)
            {
                telefoane.Add(telefon);
            }
        }

        public List<Telefon> CautaDupaPret(decimal pret)
        {
            return telefoane.Where(t => t.Pret == pret).ToList();
        }
    }

}
