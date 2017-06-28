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
    class EvidenceZakazek
    {
        public static String TABLE_NAME = "Zakazka";

        /*
         * Evidence zakázek:
         * 2.1 Vytvoření zakázky
         * 2.2 Úprava zakázky
         * 2.3 Odstranění zakázky
         * 2.5 Detail zakázky
         * 2.6 Seznam zakázek
         */
        public static String SQL_UPDATE = "update Zakazka set nazev = @nazev, smlouva = @smlouva, splatnost = @splatnost, dokonceno = @dokonceno, zaplaceno = @zaplaceno, poznamka = @poznamka where idZakazka = @idZakazka";
        public static String SQL_DELETE_ID = "delete from Zakazka where idZakazka = @idZakazka";
        public static String SQL_SELECT_DETAIL = "select idZakazka, Zakaznik_idZakaznik, Zarizeni_idZarizeni, nazev, smlouva, splatnost, dokonceno, zaplaceno, poznamka from Zakazka WHERE idZakazka = @idZakazka;";
        public static String SQL_SELECT = "select idZakazka, nazev, smlouva from Zakazka";

        public static int Insert(Zakazka zakakazka, Database pDb = null)
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

            // (@Zakaznik_idZakaznik integer, @Zarizeni_idZarizeni integer, @nazev VARCHAR(200) , @smlouva VARCHAR(45), @splatnost date, @poznamka text)
            SqlCommand command = new SqlCommand();
            command = db.CreateCommand("pridatZakazku", CommandType.StoredProcedure);

            command.Parameters.AddWithValue("@Zakaznik_idZakaznik", zakakazka.Zakaznik_idZakaznik);
            command.Parameters.AddWithValue("@Zarizeni_idZarizeni", zakakazka.Zarizeni_idZarizeni);
            command.Parameters.AddWithValue("@nazev", zakakazka.nazev);
            command.Parameters.AddWithValue("@smlouva", zakakazka.smlouva);
            command.Parameters.AddWithValue("@splatnost", zakakazka.splatnost);
            command.Parameters.AddWithValue("@poznamka", zakakazka.poznamka);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }
            return ret;
        }

        public static void dokoncitZakazku(int idZakakazka, Database pDb = null)
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

            //// (@pID_zakazka INTEGER)
            SqlCommand command = new SqlCommand();
            command = db.CreateCommand("dokoncitZakazku", CommandType.StoredProcedure);

            command.Parameters.AddWithValue("@@pID_zakazka", idZakakazka);

            if (pDb == null)
            {
                db.Close();
            }
        }

        public static int Update(Zakazka zakakazka, Database pDb = null)
        {
            Database db;
            db = new Database();
            db.Connect();

            // nazev = @nazev, smlouva = @smlouva, splatnost = @splatnost, dokonceno = @dokonceno, zaplaceno = @zaplaceno, poznamka = @poznamka where idZakazka = @idZakazka
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@nazev", zakakazka.nazev);
            command.Parameters.AddWithValue("@smlouva", zakakazka.smlouva == null ? DBNull.Value : (object)zakakazka.smlouva);
            command.Parameters.AddWithValue("@splatnost", zakakazka.splatnost);
            command.Parameters.AddWithValue("@dokonceno", zakakazka.dokonceno);
            command.Parameters.AddWithValue("@zaplaceno", zakakazka.zaplaceno);
            command.Parameters.AddWithValue("@poznamka", zakakazka.poznamka);
            command.Parameters.AddWithValue("@idZakazka", zakakazka.idZakazka);
            
            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public static int Delete(int idZakazka, Database pDb = null)
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
            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);

            command.Parameters.AddWithValue("@idZakazka", idZakazka);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static Zakazka SelectDetail(int idZakazka, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_DETAIL);
            command.Parameters.AddWithValue("@idZakazka", idZakazka);
            SqlDataReader reader = db.Select(command);

            Collection<Zakazka> zakazky = new Collection<Zakazka>();

            // idZakazka, Zakaznik_idZakaznik, Zarizeni_idZarizeni, nazev, smlouva, splatnost, dokonceno, zaplaceno, poznamka from Zakazka WHERE idZakazka = @idZakazka

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


            Zakazka zakazkyReturn = null;
            if (zakazky.Count == 1)
            {
                zakazkyReturn = zakazky[0];
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zakazkyReturn;
        }

        public static Collection<Zakazka> Select(Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);

            Collection<Zakazka> zakazky = new Collection<Zakazka>();
            // idZakazka, nazev, smlouva from Zakazka
            while (reader.Read())
            {
                int i = -1;
                Zakazka select = new Zakazka();
                select.idZakazka = reader.GetInt32(++i);
                select.nazev = reader.GetString(++i);
                select.smlouva = reader.GetString(++i);

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
