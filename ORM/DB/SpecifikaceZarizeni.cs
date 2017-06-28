using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.DB
{
    class SpecifikaceZarizeni
    {
        public int idSpecifikaceZarizeni { get; set; }
        public int vyrobniCislo { get; set; }
        public string model { get; set; }
        public int vykon { get; set; }
        public string emisniTrida { get; set; }
        public DateTime posledniKontrola { get; set; }
        public int TypPaliva_idTypPaliva { get; set; }
    }
}
