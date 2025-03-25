using System.Collections.Generic;
using System.Linq;

namespace Magazin 
{

    public class Magazin
    {
        public List<Telefon> telefoane;

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

        public List<Telefon> GetTelefoane()
        {
            return telefoane;
        }

        public List<Telefon> CautaDupaPret(decimal pret)
        {
            return telefoane.Where(t => t.Pret == pret).ToList();
        }
    }

}
