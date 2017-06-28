using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.DB
{
    class Zakaznik
    {
        public int idZakaznik { get; set; }
        public int Kontakt_idKontakt { get; set; }
        public int Bydliste_idBydliste { get; set; }
        public string jmeno { get; set; }
        public string prijmeni { get; set; }

        public int idBydliste { get; set; }
        public string mesto { get; set; }
        public string ulice { get; set; }
        public int cisloPopisne { get; set; }
        public int psc { get; set; }

        public int idKontakt { get; set; }
        public int tel { get; set; }
        public int tel2 { get; set; }
        public string email { get; set; }
    }
}
