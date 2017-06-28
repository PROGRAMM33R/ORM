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
    class EvidenceZarizeni
    {
        public static String TABLE_NAME = "Zarizeni";

        /*
         * Evidence zařízení:
         * 3.1 Vytvoření záznamu o novém zařízení
         * 3.2 Úprava informací o existujícím zařízení
         * 3.3 Odstranění zařízení
         * 3.4 Detail zařízení (technické specifikace)
         * 3.5 Celkový seznam zařízení
         * 3.6 Výpis všech možných stavů zařízení
         * 3.7 Výpis všech typů paliv
         */
        public static String SQL_INSERT = "INSERT INTO Zarizeni (SpecifikaceZarizeni_idSpecifikaceZarizeni, nazev, vyrobce, zaruka, datumSpusteni, StavZarizeni_idStavZarizeni) VALUES (@SpecifikaceZarizeni_idSpecifikaceZarizeni, @nazev, @vyrobce, @zaruka, @datumSpusteni, @StavZarizeni_idStavZarizeni);";
        public static String SQL_INSERT_SPECIFIKACE = "insert into SpecifikaceZarizeni values(@evidencniCislo, @typ, @vykon, @emisniTrida, @posledniKontrola, @typPaliva);";
        public static String SQL_UPDATE = "UPDATE Zarizeni SET SpecifikaceZarizeni_idSpecifikaceZarizeni = @SpecifikaceZarizeni_idSpecifikaceZarizeni, nazev = @nazev, vyrobce = @vyrobce, zaruka = @zaruka, datumSpusteni = @datumSpusteni, StavZarizeni_idStavZarizeni = @StavZarizeni_idStavZarizeni WHERE idZarizeni = @idZarizeni";
        public static String SQL_DELETE = "delete from Zarizeni where idZarizeni = @idZarizeni";
        public static String SQL_SELECT_DETAIL = "select sp.vyrobniCislo, sp.model, sp.vykon, sp.emisniTrida, sp.posledniKontrola from Zarizeni as z, SpecifikaceZarizeni as sp where z.SpecifikaceZarizeni_idSpecifikaceZarizeni = sp.idSpecifikaceZarizeni and z.idZarizeni = @idZarizeni";
        public static String SQL_SELECT = "SELECT idZarizeni, SpecifikaceZarizeni_idSpecifikaceZarizeni, nazev, vyrobce, zaruka, datumSpusteni, StavZarizeni_idStavZarizeni FROM Zarizeni";
        public static String SQL_SELECT_POSLEDNI_ID_SPECIFIKACE = "SELECT TOP 1 idSpecifikaceZarizeni FROM SpecifikaceZarizeni ORDER BY idSpecifikaceZarizeni DESC;";
        public static String SQL_SELECT_STAVY = "select * from StavZarizeni";
        public static String SQL_SELECT_PALIVA = "select * from TypPaliva";
        
        public static int Insert(Zarizeni zarizeni, Database pDb = null)
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

            // (@pID_zakazka integer, @SpecifikaceZarizeni_idSpecifikaceZarizeni integer, @nazev varchar(200), @vyrobce varchar(45), @zaruka integer, @datumSpusteni date, @StavZarizeni_idStavZarizeni integer)
            SqlCommand command = new SqlCommand();
            command = db.CreateCommand("pridatZarizeni", CommandType.StoredProcedure);

            command.Parameters.AddWithValue("@pID_zakazka", zarizeni.idZarizeni);
            command.Parameters.AddWithValue("@SpecifikaceZarizeni_idSpecifikaceZarizeni", zarizeni.SpecifikaceZarizeni_idSpecifikaceZarizeni);
            command.Parameters.AddWithValue("@nazev", zarizeni.nazev);
            command.Parameters.AddWithValue("@vyrobce", zarizeni.vyrobce);
            command.Parameters.AddWithValue("@zaruka", zarizeni.zaruka);
            command.Parameters.AddWithValue("@datumSpusteni", zarizeni.datumSpusteni);
            command.Parameters.AddWithValue("@StavZarizeni_idStavZarizeni", zarizeni.StavZarizeni_idStavZarizeni);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }
            return ret;
        }

        public static int InsertSpecifikace(SpecifikaceZarizeni zarizeni, Database pDb = null)
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

            // "insert into SpecifikaceZarizeni values(@evidencniCislo, @typ, @vykon, @emisniTrida, @posledniKontrola, @typPaliva);"
            SqlCommand command = new SqlCommand();
            command = db.CreateCommand(SQL_INSERT_SPECIFIKACE);

            command.Parameters.AddWithValue("@evidencniCislo", zarizeni.vyrobniCislo);
            command.Parameters.AddWithValue("@typ", zarizeni.model);
            command.Parameters.AddWithValue("@vykon", zarizeni.vykon);
            command.Parameters.AddWithValue("@emisniTrida", zarizeni.emisniTrida);
            command.Parameters.AddWithValue("@posledniKontrola", zarizeni.posledniKontrola);
            command.Parameters.AddWithValue("@typPaliva", zarizeni.TypPaliva_idTypPaliva);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }
            return ret;
        }

        public static int posledniIdSpecifikace(Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_POSLEDNI_ID_SPECIFIKACE);
            SqlDataReader reader = db.Select(command);

            int id = 0;
            if (reader.Read())
                id = reader.GetInt32(0);

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return id;
        }

        ////(@SpecifikaceZarizeni_idSpecifikaceZarizeni, @nazev, @vyrobce, @zaruka, @datumSpusteni, @StavZarizeni_idStavZarizeni)
        public static int Update(Zarizeni zarizeni, Database pDb = null)
        {
            Database db;
            db = new Database();
            db.Connect();

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@SpecifikaceZarizeni_idSpecifikaceZarizeni", zarizeni.SpecifikaceZarizeni_idSpecifikaceZarizeni);
            command.Parameters.AddWithValue("@nazev", zarizeni.nazev);
            command.Parameters.AddWithValue("@vyrobce", zarizeni.vyrobce);
            command.Parameters.AddWithValue("@zaruka", zarizeni.zaruka);
            command.Parameters.AddWithValue("@datumSpusteni", zarizeni.datumSpusteni);
            command.Parameters.AddWithValue("@StavZarizeni_idStavZarizeni", zarizeni.StavZarizeni_idStavZarizeni);
            command.Parameters.AddWithValue("@idZarizeni", zarizeni.idZarizeni);

            int ret = db.ExecuteNonQuery(command);
            db.Close();
            return ret;
        }

        public static SpecifikaceZarizeni SelectDetail(int idZarizeni, Database pDb = null)
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
            command.Parameters.AddWithValue("@idZarizeni", idZarizeni);
            SqlDataReader reader = db.Select(command);

            //sp.vyrobniCislo, sp.model, sp.vykon, sp.emisniTrida, sp.posledniKontrola
            Collection<SpecifikaceZarizeni> zarizeni = new Collection<SpecifikaceZarizeni>();

            while (reader.Read())
            {
                int i = -1;
                SpecifikaceZarizeni select = new SpecifikaceZarizeni();
                select.vyrobniCislo = reader.GetInt32(++i);
                select.model = reader.GetString(++i);
                select.vykon = reader.GetInt32(++i);
                select.emisniTrida = reader.GetString(++i);
                select.posledniKontrola = reader.GetDateTime(++i);
               
                zarizeni.Add(select);
            }


            SpecifikaceZarizeni zarizeniReturn = null;
            if (zarizeni.Count == 1)
            {
                zarizeniReturn = zarizeni[0];
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zarizeniReturn;
        }

        public static Collection<Zarizeni> Select(Database pDb = null)
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

            //idZarizeni, SpecifikaceZarizeni_idSpecifikaceZarizeni, nazev, vyrobce, zaruka, datumSpusteni, StavZarizeni_idStavZarizeni
            Collection<Zarizeni> zarizeni = new Collection<Zarizeni>();

            while (reader.Read())
            {
                int i = -1;
                Zarizeni select = new Zarizeni();
                select.idZarizeni = reader.GetInt32(++i);
                select.SpecifikaceZarizeni_idSpecifikaceZarizeni = reader.GetInt32(++i);
                select.nazev = reader.GetString(++i);
                select.vyrobce = reader.GetString(++i);
                select.zaruka = reader.GetInt32(++i);
                select.datumSpusteni = reader.GetDateTime(++i);
                select.StavZarizeni_idStavZarizeni = reader.GetInt32(++i);
                zarizeni.Add(select);
            }


            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zarizeni;
        }

        public static Collection<TypPaliva> SelectPaliva(Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_PALIVA);
            SqlDataReader reader = db.Select(command);

            //idZarizeni, SpecifikaceZarizeni_idSpecifikaceZarizeni, nazev, vyrobce, zaruka, datumSpusteni, StavZarizeni_idStavZarizeni
            Collection<TypPaliva> typPaliva = new Collection<TypPaliva>();

            while (reader.Read())
            {
                int i = -1;
                TypPaliva select = new TypPaliva();
                select.idTypPaliva = reader.GetInt32(++i);
                select.nazev = reader.GetString(++i);
                typPaliva.Add(select);
            }


            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return typPaliva;
        }
    

        public static Collection<StavZarizeni> SelectStavy(Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_STAVY);
            SqlDataReader reader = db.Select(command);

            //idZarizeni, SpecifikaceZarizeni_idSpecifikaceZarizeni, nazev, vyrobce, zaruka, datumSpusteni, StavZarizeni_idStavZarizeni
            Collection<StavZarizeni> zarizeni = new Collection<StavZarizeni>();

            while (reader.Read())
            {
                int i = -1;
                StavZarizeni select = new StavZarizeni();
                select.idStavZarizeni = reader.GetInt32(++i);
                select.nazev = reader.GetString(++i);
                zarizeni.Add(select);
            }


            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zarizeni;
        }
    }
}