using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.DB
{
    public class Zakazka
    {
	    public int idZakazka { get; set;}
        public int Zakaznik_idZakaznik { get; set; }
        public int Zarizeni_idZarizeni { get; set; }
	    public string nazev { get; set;}
	    public string smlouva { get; set;}
        public DateTime splatnost { get; set; }
	    public bool dokonceno { get; set;}
	    public bool zaplaceno { get; set;}
        public string poznamka { get; set; }
        public int idStavZarizeni { get; set; }
    }
}
