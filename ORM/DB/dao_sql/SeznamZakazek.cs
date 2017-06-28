using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ORM.DB.dao_sql
{
    class SeznamZakazek
    {
        public static String SQL_SELECT_1 = "select idZakazka, Zakaznik_idZakaznik, Zarizeni_idZarizeni, nazev, smlouva, splatnost, dokonceno, zaplaceno, poznamka from Zakazka WHERE dokonceno = 0";
        public static String SQL_SELECT_2 = "select idZakazka, Zakaznik_idZakaznik, Zarizeni_idZarizeni, nazev, smlouva, splatnost, dokonceno, zaplaceno, poznamka from Zakazka WHERE zaplaceno = 0";
        public static String SQL_SELECT_3 = "select z.idZakaznik, z.jmeno, z.prijmeni, b.mesto, b.cisloPopisne, b.psc, k.tel, k.tel2, k.email from Zakaznik as z, Bydliste as b, Kontakt as k, Zakazka as zak where z.Bydliste_idBydliste = b.idBydliste and k.idKontakt = z.Kontakt_idKontakt and zak.Zakaznik_idZakaznik = z.idZakaznik and zak.zaplaceno = 0;";
        public static String SQL_SELECT_4 = "select zak.idZakazka, zak.Zakaznik_idZakaznik, zak.Zarizeni_idZarizeni, zak.nazev, zak.smlouva, zak.splatnost, zak.dokonceno, zak.zaplaceno, zak.poznamka from Zakazka as zak, Zakaznik as z WHERE z.idZakaznik = zak.Zakaznik_idZakaznik and z.idZakaznik = @idZakaznik";
        public static String SQL_SELECT_5 = "select zar.idZarizeni, zar.nazev, zar.vyrobce, zar.zaruka, zar.datumSpusteni from Zarizeni as zar, Zakazka as zak where zak.Zarizeni_idZarizeni = zar.idZarizeni and zak.idZakazka = @idZakazka";

        public static Collection<Zakazka> SelectNedokonceneZakazky(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_1);
            SqlDataReader reader = db.Select(command);

            Collection<Zakazka> zakazky = new Collection<Zakazka>();
            // idZakazka, nazev, smlouva from Zakazka
            while (reader.Read())
            {
                int i = -1;
                Zakazka select = new Zakazka();
                select.idZakazka = reader.GetInt32(++i);
                select.Zakaznik_idZakaznik = reader.GetInt32(++i);
                select.Zarizeni_idZarizeni = reader.GetInt32(++i);
                select.nazev = reader.GetString(++i);
                select.smlouva = reader.GetString(++i);
                select.splatnost = reader.GetDateTime(++i);
                select.dokonceno = reader.GetBoolean(++i);
                select.zaplaceno = reader.GetBoolean(++i);
                select.poznamka = reader.GetString(++i);
                zakazky.Add(select);
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zakazky;
        }

        public static Collection<Zakazka> SelectNezaplaceneZakazky(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_2);
            SqlDataReader reader = db.Select(command);

            Collection<Zakazka> zakazky = new Collection<Zakazka>();
            // idZakazka, nazev, smlouva from Zakazka
            while (reader.Read())
            {
                int i = -1;
                Zakazka select = new Zakazka();
                select.idZakazka = reader.GetInt32(++i);
                select.Zakaznik_idZakaznik = reader.GetInt32(++i);
                select.Zarizeni_idZarizeni = reader.GetInt32(++i);
                select.nazev = reader.GetString(++i);
                select.smlouva = reader.GetString(++i);
                select.splatnost = reader.GetDateTime(++i);
                select.dokonceno = reader.GetBoolean(++i);
                select.zaplaceno = reader.GetBoolean(++i);
                select.poznamka = reader.GetString(++i);
                zakazky.Add(select);
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zakazky;
        }

        public static Collection<Zakaznik> ZakazniciKteriNezaplatili(Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_3);
            SqlDataReader reader = db.Select(command);

            Collection<Zakaznik> zakazky = new Collection<Zakaznik>();
            // z.idZakaznik, z.jmeno, z.prijmeni, b.mesto, b.cisloPopisne, b.psc, k.tel, k.tel2, k.email
            while (reader.Read())
            {
                int i = -1;
                Zakaznik select = new Zakaznik();
                select.idZakaznik = reader.GetInt32(++i);
                select.jmeno = reader.GetString(++i);
                select.prijmeni = reader.GetString(++i);
                select.mesto = reader.GetString(++i);
                select.cisloPopisne = reader.GetInt32(++i);
                select.psc = reader.GetInt32(++i);
                select.tel = reader.GetInt32(++i);
                select.tel2 = reader.GetInt32(++i);
                select.email = reader.GetString(++i);
                zakazky.Add(select);
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zakazky;
        }

        public static Collection<Zakazka> ZakazkyZakaznika(int idZakaznik, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_4);
            command.Parameters.AddWithValue("@idZakaznik", idZakaznik);
            SqlDataReader reader = db.Select(command);

            Collection<Zakazka> zakazky = new Collection<Zakazka>();
            // zak.idZakazka, zak.Zakaznik_idZakaznik, zak.Zarizeni_idZarizeni, zak.nazev, zak.smlouva, zak.splatnost, zak.dokonceno, zak.zaplaceno, zak.poznamka
            while (reader.Read())
            {
                int i = -1;
                Zakazka select = new Zakazka();
                select.idZakazka = reader.GetInt32(++i);
                select.Zakaznik_idZakaznik = reader.GetInt32(++i);
                select.Zarizeni_idZarizeni = reader.GetInt32(++i);
                select.nazev = reader.GetString(++i);
                select.smlouva = reader.GetString(++i);
                select.splatnost = reader.GetDateTime(++i);
                select.dokonceno = reader.GetBoolean(++i);
                select.zaplaceno = reader.GetBoolean(++i);
                select.poznamka = reader.GetString(++i);
                zakazky.Add(select);
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zakazky;
        }

        public static Collection<Zarizeni> ZarizeniUZakazky(int idZakazka, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_5);
            command.Parameters.AddWithValue("@idZakazka", idZakazka);
            SqlDataReader reader = db.Select(command);

            Collection<Zarizeni> zakazky = new Collection<Zarizeni>();
            // zar.idZarizeni, zar.nazev, zar.vyrobce, zar.zaruka, zar.datumSpusteni
            while (reader.Read())
            {
                int i = -1;
                Zarizeni select = new Zarizeni();
                select.idZarizeni = reader.GetInt32(++i);
                select.nazev = reader.GetString(++i);
                select.vyrobce = reader.GetString(++i);
                select.zaruka = reader.GetInt32(++i);
                select.datumSpusteni = reader.GetDateTime(++i);
                zakazky.Add(select);
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zakazky;
        }

    }
}
