using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.DB.dao_sql;
using ORM.DB;
using System.Windows.Forms;

namespace ORM
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            /*Program p = new Program();
            Database db = new Database();
            db.Connect();
            bool loop = true;
            string menuInput;

            //p.testTabulkyZakaznik(db);
            //p.testTabulkyZakazka(db);
            //p.testTabulkyZarizeni(db);
            //p.testTabulkyServis(db);
            //p.testSeznamuZakazek(db);

            while (loop)
            {
                Console.WriteLine("");
                Console.WriteLine("[1] Test tabulky Zakaznik");
                Console.WriteLine("[2] Test tabulky Zakazka");
                Console.WriteLine("[3] Test tabulky Zarizeni");
                Console.WriteLine("[4] Test tabulky Servis");
                Console.WriteLine("[5] Test seznamu zakazek");
                Console.WriteLine("[lib. klavesa] Konec");
                Console.Write("> ");
                menuInput = Console.ReadLine();
                switch (menuInput)
                {
                    case "1":
                        p.testTabulkyZakaznik(db);
                        break;
                    case "2":
                        p.testTabulkyZakazka(db);
                        break;
                    case "3":
                        p.testTabulkyZarizeni(db);
                        break;
                    case "4":
                        p.testTabulkyServis(db);
                        break;
                    case "5":
                        p.testSeznamuZakazek(db);
                        break;
                    default:
                        loop = false;
                        break;
                }
                Console.Write("Stisknete Enter pro pokracovani...");
                ConsoleKeyInfo c = Console.ReadKey();
                while (c.Key != ConsoleKey.Enter) {}
                Console.Clear();
            }

            db.Close();
            Console.Out.WriteLine("\n-------------------------------------------------------------------------------");
            Console.WriteLine("\n\t\t\tSpojení s DB bylo ukončeno!");
            Console.Out.WriteLine("\n-------------------------------------------------------------------------------");
            Console.ReadKey();*/
        }
        
        public void testTabulkyZakaznik(Database db)
        {
            Console.Out.WriteLine("-------------------------------------------------------------------------------");
            Console.Out.WriteLine("Test tabulky Zakaznik:");

            Zakaznik zakaznik = new Zakaznik();
           
            zakaznik.jmeno = "Adam";
            zakaznik.prijmeni = "Lasák";
            zakaznik.tel = 739141091;
            zakaznik.tel2 = 0;
            zakaznik.email = "lasak.ad@gmail.com";
            zakaznik.mesto = "Kobeřice";
            zakaznik.ulice = "Nádražní";
            zakaznik.cisloPopisne = 35;
            zakaznik.psc = 74727;

            EvidenceZakaznika.Insert(zakaznik, db);
            Console.Out.WriteLine("Nový zákazník vložen!\n");

            Collection<Zakaznik> zakaznici = new Collection<Zakaznik>();
            zakaznici = EvidenceZakaznika.Select(db);
            Console.Out.WriteLine("Select z tabulky Zakaznik:");

            foreach (Zakaznik select in zakaznici)
                Console.Out.WriteLine("\t" + select.idZakaznik + " " + select.jmeno + " " + select.prijmeni + " " + select.mesto + " " + select.ulice + " " + select.cisloPopisne + " " + select.psc);

            Console.Out.WriteLine("\nPoslední záznam z tabulky osoba byl upraven! (město, tel. číslo, email)");
            zakaznik.tel = 123456789;
            zakaznik.email = "novy@gmail.cz";
            zakaznik.mesto = "Opava";
            EvidenceZakaznika.Update(zakaznik, db);

            Console.Out.WriteLine("\nDetail posledního záznamu z tabulky osoba:");
            Zakaznik selectDetail = new Zakaznik();
            selectDetail = EvidenceZakaznika.SelectDetail(zakaznik.idZakaznik-1, db);
            Console.Out.WriteLine("\t" + selectDetail.idZakaznik + " " + selectDetail.jmeno + " " + selectDetail.prijmeni + " " + selectDetail.mesto + " " + selectDetail.ulice + " " + selectDetail.cisloPopisne + " " + selectDetail.psc + " " + selectDetail.tel + " " + selectDetail.tel2 + " " + selectDetail.email);

            Console.Out.WriteLine("Poslední záznam z tabulky osoba byl odstraněn!");
            EvidenceZakaznika.Delete(zakaznik.idZakaznik, db);

            Console.Out.WriteLine("-------------------------------------------------------------------------------\n");
            Console.ReadKey();

        }

        public void testTabulkyZakazka(Database db)
        {

            Console.Out.WriteLine("-------------------------------------------------------------------------------");
            Console.Out.WriteLine("Zkouška tabulky Zakazka:");

            Zakazka zakazka = new Zakazka();
            int ID_deleted = 2;

            // nazev, smlouva, splatnost, dokonceno, zaplaceno, poznamka from Zakazka WHERE idZakazka = @idZakazka
            zakazka.idZakazka = 1;
            zakazka.nazev = "Eko kotel - Lasák";
            zakazka.smlouva = "Smlouva o provedení prac. činnosti";
            zakazka.splatnost = new DateTime(2017, 3, 1, 7, 0, 0);
            zakazka.dokonceno = false;
            zakazka.zaplaceno = false;
            zakazka.poznamka = "..";
            
            Collection<Zakazka> zakazky = new Collection<Zakazka>();
            zakazky = EvidenceZakazek.Select(db);
            int count1 = zakazky.Count;
            Console.Out.WriteLine("Select z tabulky zakazka:");

            foreach (Zakazka select in zakazky)
            {
                Console.Out.WriteLine("\t" + select.idZakazka + " | " + select.nazev + " | " + select.smlouva);
            }
            EvidenceZakazek.dokoncitZakazku(zakazka.idZakazka, db);

            Console.Out.WriteLine("\nPoslední záznam z tabulky zakazka byl upraven!");
            EvidenceZakazek.Update(zakazka, db);

            // idZakazka, Zakaznik_idZakaznik, Zarizeni_idZarizeni, nazev, smlouva, splatnost, dokonceno, zaplaceno, poznamka from Zakazka WHERE idZakazka = @idZakazka
            Console.Out.WriteLine("\nDetail posledního záznamu z tabulky zakazka:");
            Zakazka selectDetail = new Zakazka();
            selectDetail = EvidenceZakazek.SelectDetail(ID_deleted, db);
            Console.Out.WriteLine("\t" + selectDetail.idZakazka + " | " + selectDetail.Zakaznik_idZakaznik + " | " + selectDetail.Zarizeni_idZarizeni + " | " + selectDetail.nazev + " | " + selectDetail.smlouva + " | " + selectDetail.splatnost + " | "  + selectDetail.dokonceno + " | " + selectDetail.zaplaceno + " | " + selectDetail.poznamka);

            //Console.Out.WriteLine("Poslední záznam z tabulky zakazka byl odstraněn!");
            //EvidenceZakazek.Delete(ID_deleted, db);
            Console.Out.WriteLine("-------------------------------------------------------------------------------\n");
        }

        public void testTabulkyZarizeni(Database db)
        {

            Console.Out.WriteLine("-------------------------------------------------------------------------------");
            Console.Out.WriteLine("Zkouška tabulky Zarizeni:");

            Zarizeni zarizeni = new Zarizeni();
            zarizeni.idZarizeni = 1;
            zarizeni.SpecifikaceZarizeni_idSpecifikaceZarizeni = 1;
            zarizeni.nazev = "P400/29 xjks";
            zarizeni.vyrobce = "Ja";
            zarizeni.zaruka = 24;
            zarizeni.datumSpusteni = new DateTime(2017, 3, 1, 7, 0, 0);
            zarizeni.StavZarizeni_idStavZarizeni = 1;

            //EvidenceZarizeni.Insert(zarizeni, db);
            Console.Out.WriteLine("Nové zařízení vloženo!\n");

            Collection<Zarizeni> selectZarizeni = new Collection<Zarizeni>();
            selectZarizeni = EvidenceZarizeni.Select(db);

            Console.Out.WriteLine("Select z tabulky zarizeni:");

            // select detail> //sp.vyrobniCislo, sp.model, sp.vykon, sp.emisniTrida, sp.posledniKontrola
            // select normal> //idZarizeni, SpecifikaceZarizeni_idSpecifikaceZarizeni, nazev, vyrobce, zaruka, datumSpusteni, StavZarizeni_idStavZarizeni
            foreach (Zarizeni select in selectZarizeni)
            {
                Console.Out.WriteLine("\t" + select.idZarizeni + " | --- | " + select.SpecifikaceZarizeni_idSpecifikaceZarizeni + " " + select.nazev + " " + select.vyrobce + " " + select.zaruka + " " + select.datumSpusteni);
            }

            Console.Out.WriteLine("\nPoslední záznam z tabulky zarizeni byl upraven! (nazev)");
            zarizeni.nazev = "P400/29";
            zarizeni.vyrobce = "Ja Taky";
            EvidenceZarizeni.Update(zarizeni, db);

            Console.Out.WriteLine("\nDetail posledního záznamu z tabulky zarizeni:");
            SpecifikaceZarizeni selectDetail = new SpecifikaceZarizeni();
            selectDetail = EvidenceZarizeni.SelectDetail(1, db);
            Console.Out.WriteLine("\t" + selectDetail.vyrobniCislo + " | --- | " + selectDetail.model + " | --- | " + selectDetail.vykon + " | --- | " + selectDetail.emisniTrida + " | --- | " + selectDetail.posledniKontrola);
            Console.Out.WriteLine("-------------------------------------------------------------------------------\n");

        }

        public void testTabulkyServis(Database db)
        {

            Console.Out.WriteLine("-------------------------------------------------------------------------------");
            Console.Out.WriteLine("Zkouška tabulky Servis:");

            Servis servis = new Servis();
            int ID_deleted = 2;

            Collection<Servis> servisy = new Collection<Servis>();
            servisy = EvidenceServisu.SelectByID(ID_deleted, db);
            Console.Out.WriteLine("Select z tabulky Servis s ID_izarizeni 1:");

            foreach (Servis select in servisy)
            {
                Console.Out.WriteLine("\t" + select.idServis + " " + select.datum + " " + select.popis);
            }

            servis.Zarizeni_idZarizeni = 1;
            servis.Zakaznik_idZakaznik = 33;
            servis.datum = new DateTime(2017, 3, 1, 7, 0, 0);
            servis.popis = "kotel obohaceny o uran";
            servis.dokonceno = false;
            servis.zaplaceno = false;
            EvidenceServisu.Update(servis, db);
            Console.Out.WriteLine("\nPoslední záznam z tabulky Servis a ID_izarizeni " + ID_deleted + " byl upraven!");

            Console.Out.WriteLine("\nDetail posledního záznamu z tabulky Servis a ID_izarizeni 1:");
            Servis selectDetail = new Servis();
            selectDetail = EvidenceServisu.SelectDetail(ID_deleted, db);    ////Id_servis, Nazev, Datum, Popis, Zarucni
            Console.Out.WriteLine("\t" + selectDetail.idServis + " " + selectDetail.Zarizeni_idZarizeni + " " + selectDetail.Zakaznik_idZakaznik + " " + selectDetail.datum + " " + selectDetail.popis + " " + selectDetail.dokonceno + " " + selectDetail.zaplaceno);
            Console.Out.WriteLine("-------------------------------------------------------------------------------\n");
        }

        public void testSeznamuZakazek(Database db)
        {
            Collection<Zakazka> zakazka_1 = new Collection<Zakazka>();
            zakazka_1 = SeznamZakazek.SelectNedokonceneZakazky(db);
            Console.Out.WriteLine("Nedokoncene zakazky:");
            // idZakazka, Zakaznik_idZakaznik, Zarizeni_idZarizeni, nazev, smlouva, splatnost, dokonceno, zaplaceno, poznamka
            foreach (Zakazka select in zakazka_1)
                Console.Out.WriteLine("\t" + select.idZakazka + " " + select.Zakaznik_idZakaznik + " " + select.Zarizeni_idZarizeni + " " + select.nazev + " " + select.smlouva + " " + select.splatnost + " " + select.dokonceno + " " + select.zaplaceno + " " + select.poznamka);

            Collection<Zakazka> zakazka_2 = new Collection<Zakazka>();
            zakazka_2 = SeznamZakazek.SelectNezaplaceneZakazky(db);
            Console.Out.WriteLine("");
            Console.Out.WriteLine("Nezaplacene zakazky:");
            // idZakazka, Zakaznik_idZakaznik, Zarizeni_idZarizeni, nazev, smlouva, splatnost, dokonceno, zaplaceno, poznamka
            foreach (Zakazka select in zakazka_2)
                Console.Out.WriteLine("\t" + select.idZakazka + " " + select.Zakaznik_idZakaznik + " " + select.Zarizeni_idZarizeni + " " + select.nazev + " " + select.smlouva + " " + select.splatnost + " " + select.dokonceno + " " + select.zaplaceno + " " + select.poznamka);

            Collection<Zakaznik> zakazka_3 = new Collection<Zakaznik>();
            zakazka_3 = SeznamZakazek.ZakazniciKteriNezaplatili(db);
            Console.Out.WriteLine("");
            Console.Out.WriteLine("Zakaznici kteri nezaplatili:");
            // z.idZakaznik, z.jmeno, z.prijmeni, b.mesto, b.cisloPopisne, b.psc, k.tel, k.tel2, k.email
            foreach (Zakaznik select in zakazka_3)
                Console.Out.WriteLine("\t" + select.idZakaznik + " " + select.jmeno + " " + select.prijmeni + " " + select.mesto + " " + select.cisloPopisne + " " + select.psc + " " + select.tel + " " + select.tel2 + " " + select.email);

            Collection<Zakazka> zakazka_4 = new Collection<Zakazka>();
            zakazka_4 = SeznamZakazek.ZakazkyZakaznika(33, db);
            Console.Out.WriteLine("");
            Console.Out.WriteLine("Vsechny zakazky daneho zakaznika:");
            // idZakazka, Zakaznik_idZakaznik, Zarizeni_idZarizeni, nazev, smlouva, splatnost, dokonceno, zaplaceno, poznamka
            foreach (Zakazka select in zakazka_4)
                Console.Out.WriteLine("\t" + select.idZakazka + " " + select.Zakaznik_idZakaznik + " " + select.Zarizeni_idZarizeni + " " + select.nazev + " " + select.smlouva + " " + select.splatnost + " " + select.dokonceno + " " + select.zaplaceno + " " + select.poznamka);

            Collection<Zarizeni> zakazka_5 = new Collection<Zarizeni>();
            zakazka_5 = SeznamZakazek.ZarizeniUZakazky(1, db);
            Console.Out.WriteLine("");
            Console.Out.WriteLine("Vsechny zarizeni u dane zakazky:");
            // zar.idZarizeni, zar.nazev, zar.vyrobce, zar.zaruka, zar.datumSpusteni
            foreach (Zarizeni select in zakazka_5)
                Console.Out.WriteLine("\t" + select.idZarizeni + " " + select.nazev + " " + select.vyrobce + " " + select.zaruka + " " + select.datumSpusteni);
        }
       
    }
}
