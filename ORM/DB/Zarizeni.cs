using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.DB
{
    public class Zarizeni
    {
	    public int idZarizeni { get; set;}
        public int SpecifikaceZarizeni_idSpecifikaceZarizeni { get; set; }
	    public string nazev { get; set;}
        public string vyrobce { get; set; }
        public int zaruka { get; set; }
        public DateTime datumSpusteni { get; set; }
        public int StavZarizeni_idStavZarizeni { get; set; }
    }
}
