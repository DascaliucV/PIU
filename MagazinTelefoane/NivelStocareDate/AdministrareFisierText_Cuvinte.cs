using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelStocareDate
{
    public class AdministrareFisierText_Cuvinte
    {

        public List<String> GetCuvinte(String numeFisier)
        {
            List<String> cuvinte = new List<String>();

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    cuvinte.Add(linieFisier);
                }
            }

            return cuvinte;
        }
    }
}
