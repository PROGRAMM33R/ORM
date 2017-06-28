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
    class EvidenceServisu
    {
        /*
         * Evidence servisů:
         * 4.1 Vytvoření záznamu servisu
         * 4.2 Úprava informací servisu
         * 4.3 Odstranění záznamu o servisu
         * 4.4 Výpis všech servisů u daného zařízení
         */
        public static String TABLE_NAME = "Servis";

        public static String SQL_UPDATE = "update Servis set Zarizeni_idZarizeni = @Zarizeni_idZarizeni, Zakaznik_idZakaznik = @Zakaznik_idZakaznik, datum = @datum, popis = @popis, dokonceno = @dokonceno, zaplaceno = @zaplaceno, idStavZarizeni = @idStavZarizeni where idServis = @idServis;";
        public static String SQL_DELETE = "delete from Servis where idServis = @idServis";
        public static String SQL_SELECT_ID = "SELECT idServis, datum, popis FROM Servis WHERE idServis = @idServis;";
        public static String SQL_SELECT_DETAIL = "SELECT idServis, Zarizeni_idZarizeni, Zakaznik_idZakaznik, datum, popis, dokonceno, zaplaceno FROM Servis WHERE idServis = @idServis;";

        public static void Insert(Servis servis, int idZarizeni, Database pDb = null)
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

            // (@vstupIdZarizeni integer, @Zarizeni_idZarizeni integer, @Zakaznik_idZakaznik integer, @datum date, @popis text)
            SqlCommand command = new SqlCommand();
            command = db.CreateCommand("pridatServis", CommandType.StoredProcedure);

            command.Parameters.AddWithValue("@vstupIdZarizeni", idZarizeni);
            command.Parameters.AddWithValue("@Zarizeni_idZarizeni", servis.Zarizeni_idZarizeni);
            command.Parameters.AddWithValue("@Zakaznik_idZakaznik", servis.Zakaznik_idZakaznik);
            command.Parameters.AddWithValue("@datum", servis.datum);
            command.Parameters.AddWithValue("@popis", servis.popis);
            
            if (pDb == null)
            {
                db.Close();
            }
        }

        public static int Update(Servis servis, Database pDb = null)
        {
            Database db;
            db = new Database();
            db.Connect();

            // Zarizeni_idZarizeni = @Zarizeni_idZarizeni, Zakaznik_idZakaznik = @Zakaznik_idZakaznik, datum = @datum, popis = @popis, dokonceno = @dokonceno, zaplaceno = @zaplaceno, idStavZarizeni = @idStavZarizeni
            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@idServis", servis.idServis);
            command.Parameters.AddWithValue("@Zarizeni_idZarizeni", servis.Zarizeni_idZarizeni);
            command.Parameters.AddWithValue("@Zakaznik_idZakaznik", servis.Zakaznik_idZakaznik);
            command.Parameters.AddWithValue("@datum", servis.datum);
            command.Parameters.AddWithValue("@popis", servis.popis);
            command.Parameters.AddWithValue("@dokonceno", servis.dokonceno);
            command.Parameters.AddWithValue("@zaplaceno", servis.zaplaceno);
            command.Parameters.AddWithValue("@idStavZarizeni", servis.idStavZarizeni);

            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }
        public static Collection<Servis> SelectByID(int pId_izarizeni, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@idServis", pId_izarizeni);
            SqlDataReader reader = db.Select(command);

            Collection<Servis> servisy = new Collection<Servis>();
            // idServis, datum, popis FROM Servis WHERE idServis = @idServis;
            while (reader.Read())
            {
                int i = -1;
                Servis servis = new Servis();
                servis.idServis = reader.GetInt32(++i);
                servis.datum = reader.GetDateTime(++i);
                servis.popis = reader.GetString(++i);
                servisy.Add(servis);
            }
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return servisy;
        }
        public static Servis SelectDetail(int pId_servis, Database pDb = null)
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
            command.Parameters.AddWithValue("@idServis", pId_servis);
            SqlDataReader reader = db.Select(command);

            Collection<Servis> servisy = new Collection<Servis>();

            //idServis, Zarizeni_idZarizeni, Zakaznik_idZakaznik, datum, popis, dokonceno, zaplaceno FROM Servis WHERE idServis = @idServis;
            while (reader.Read())
            {
                int i = -1;
                Servis select = new Servis();
                select.idServis = reader.GetInt32(++i);
                select.Zarizeni_idZarizeni = reader.GetInt32(++i);
                select.Zakaznik_idZakaznik = reader.GetInt32(++i);
                select.datum = reader.GetDateTime(++i);
                select.popis = reader.GetString(++i);
                select.dokonceno = reader.GetBoolean(++i);
                select.zaplaceno = reader.GetBoolean(++i);
                servisy.Add(select);
            }


            Servis servisReturn = null;
            if (servisy.Count == 1)
            {
                servisReturn = servisy[0];
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return servisReturn;
        }
    }
}
