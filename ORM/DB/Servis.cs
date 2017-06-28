using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.DB
{
    public class Servis
    {
	    public int idServis { get; set;}
        public int Zarizeni_idZarizeni { get; set; }
        public int Zakaznik_idZakaznik { get; set; }
	    public DateTime datum { get; set;}
        public string popis { get; set; }
        public bool dokonceno { get; set; }
        public bool zaplaceno { get; set; }
        public int idStavZarizeni { get; set; }
    }
}
