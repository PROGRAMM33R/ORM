using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ORM.DB.dao_sql
{
    class EvidenceZakaznika
    {
        public static String TABLE_NAME = "Zakaznik";

        /*
         * Evidence zákazníků:
         * 1.1 Vytvoření zákazníka
         * 1.2 Úprava zákazníka
         * 1.3 Odstranění zákazníka
         * 1.4 Detail zákazníka
         * 1.5 Seznam zákazníků
         */
        public static String SQL_SELECT = "select z.idZakaznik, z.jmeno, z.prijmeni, b.mesto, b.ulice, b.cisloPopisne, b.psc from Zakaznik as z, Bydliste as b where z.Bydliste_idBydliste = b.idBydliste;";                                                                                                                                            
        public static String SQL_SELECT_DETAIL = "select z.idZakaznik, z.jmeno, z.prijmeni, b.mesto, b.cisloPopisne, b.psc, k.tel, k.tel2, k.email from Zakaznik as z, Bydliste as b, Kontakt as k where z.idZakaznik = @idZakaznik and z.Bydliste_idBydliste = b.idBydliste and k.idKontakt = z.Kontakt_idKontakt;";                                                                         //1.4
        public static String SQL_UPDATE_KONTAKT = "update Kontakt set tel = @tel, tel2 = @tel2, email = @email where idKontakt = @idKontakt;";
        public static String SQL_UPDATE_BYDLISTE = "update Bydliste set mesto = @mesto, ulice = @ulice, cisloPopisne = @cisloPopisne, psc = @psc where idBydliste = @idBydliste;";
        public static String SQL_UPDATE = "update Zakaznik set jmeno = @jmeno, prijmeni = @prijmeni where idZakaznik = @idZakaznik;";
        public static String SQL_INSERT_KONTAKT = "insert into Kontakt values (@tel, @tel2, @email);";
        public static String SQL_INSERT_BYDLISTE = "insert into Bydliste values (@mesto, @ulice, @cisloPopisne, @psc);";
        public static String SQL_INSERT = "insert into Zakaznik values (@Kontakt_idKontakt, @Bydliste_idBydliste, @jmeno, @prijmeni);";
        public static String SQL_DELETE_ID = "delete from Zakaznik where idZakaznik = @idZakaznik";                                                                                                                                                   


        public static Collection<Zakaznik> Select(Database pDb = null)
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

            Collection<Zakaznik> zakaznici = CtiZakaznika(reader, true);
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zakaznici;
        }
        public static Zakaznik SelectDetail(int idZakaznik, Database pDb = null)
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
            command.Parameters.AddWithValue("@idZakaznik", idZakaznik);
            SqlDataReader reader = db.Select(command);

            Collection<Zakaznik> zakaznici = CtiZakaznika(reader, false);
            Zakaznik zakaznik = null;
            if (zakaznici.Count == 1)
            {
                zakaznik = zakaznici[0];
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return zakaznik;
        }

        public static int Update(Zakaznik zakaznik, Database pDb = null)
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

            int returnCommand1, returnCommand2, returnCommand3;

            SqlCommand command1 = db.CreateCommand(SQL_UPDATE_KONTAKT);
            command1.Parameters.AddWithValue("@tel", zakaznik.tel);
            command1.Parameters.AddWithValue("@tel2", zakaznik.tel2);
            command1.Parameters.AddWithValue("@email", zakaznik.email);
            command1.Parameters.AddWithValue("@idKontakt", zakaznik.idKontakt);

            returnCommand1 = db.ExecuteNonQuery(command1);

            // mesto = @mesto, ulice = @ulice, cisloPopisne = @cisloPopisne, psc = @psc where idBydliste = @idBydliste;"
            SqlCommand command2 = db.CreateCommand(SQL_UPDATE_BYDLISTE);
            command2.Parameters.AddWithValue("@mesto", zakaznik.mesto);
            command2.Parameters.AddWithValue("@ulice", zakaznik.ulice);
            command2.Parameters.AddWithValue("@cisloPopisne", zakaznik.cisloPopisne);
            command2.Parameters.AddWithValue("@psc", zakaznik.psc);
            command2.Parameters.AddWithValue("@idBydliste", zakaznik.idBydliste);

            returnCommand2 = db.ExecuteNonQuery(command2);

            // jmeno = @jmeno, prijmeni = @prijmeni where idZakaznik = @idZakaznik
            SqlCommand command3 = db.CreateCommand(SQL_UPDATE);
            command3.Parameters.AddWithValue("@jmeno", zakaznik.mesto);
            command3.Parameters.AddWithValue("@prijmeni", zakaznik.ulice);
            command3.Parameters.AddWithValue("@idZakaznik", zakaznik.idZakaznik);

            returnCommand3 = db.ExecuteNonQuery(command3);

            if (pDb == null)
            {
                db.Close();
            }
            return returnCommand1 & returnCommand2 & returnCommand3;
        }

        public static int Insert(Zakaznik zakaznik, Database pDb = null)
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

            int newIdKontakt, newIdBydliste, newIdZakaznik, returnCommand1, returnCommand2, returnCommand3;
            SqlDataReader reader;

            // (@tel, @tel2, @email)
            SqlCommand command1 = db.CreateCommand(SQL_INSERT_KONTAKT);
            command1.Parameters.AddWithValue("@tel", zakaznik.tel);
            command1.Parameters.AddWithValue("@tel2", zakaznik.tel2);
            command1.Parameters.AddWithValue("@email", zakaznik.email);

            returnCommand1 = db.ExecuteNonQuery(command1);

            SqlCommand commandKontaktId = db.CreateCommand("select idKontakt from Kontakt where tel = @tel and tel2 = @tel2 and email = @email");
            commandKontaktId.Parameters.AddWithValue("@tel", zakaznik.tel);
            commandKontaktId.Parameters.AddWithValue("@tel2", zakaznik.tel2);
            commandKontaktId.Parameters.AddWithValue("@email", zakaznik.email);
            reader = db.Select(commandKontaktId);
            reader.Read();
            newIdKontakt = reader.GetInt32(0);
            reader.Close();
            zakaznik.idKontakt = newIdKontakt;

            // (@mesto, @ulice, @cisloPopisne, @psc)
            SqlCommand command2 = db.CreateCommand(SQL_INSERT_BYDLISTE);
            command2.Parameters.AddWithValue("@mesto", zakaznik.mesto);
            command2.Parameters.AddWithValue("@ulice", zakaznik.ulice);
            command2.Parameters.AddWithValue("@cisloPopisne", zakaznik.cisloPopisne);
            command2.Parameters.AddWithValue("@psc", zakaznik.psc);

            returnCommand2 = db.ExecuteNonQuery(command2);

            SqlCommand commandBydlisteId = db.CreateCommand("select idBydliste from Bydliste where cisloPopisne = @cisloPopisne and ulice = @ulice and mesto = @mesto and psc = @psc");
            commandBydlisteId.Parameters.AddWithValue("@cisloPopisne", zakaznik.cisloPopisne);
            commandBydlisteId.Parameters.AddWithValue("@ulice", zakaznik.ulice);
            commandBydlisteId.Parameters.AddWithValue("@mesto", zakaznik.mesto);
            commandBydlisteId.Parameters.AddWithValue("@psc", zakaznik.psc);
            reader = db.Select(commandBydlisteId);
            reader.Read();
            newIdBydliste = reader.GetInt32(0);
            reader.Close();
            zakaznik.idBydliste = newIdBydliste;

            // (@Kontakt_idKontakt, @Bydliste_idBydliste, @jmeno, @prijmeni)
            SqlCommand command3 = db.CreateCommand(SQL_INSERT);
            command3.Parameters.AddWithValue("@Kontakt_idKontakt", newIdKontakt);
            command3.Parameters.AddWithValue("@Bydliste_idBydliste", newIdBydliste);
            command3.Parameters.AddWithValue("@jmeno", zakaznik.jmeno);
            command3.Parameters.AddWithValue("@prijmeni", zakaznik.prijmeni);

            returnCommand3 = db.ExecuteNonQuery(command3);

            SqlCommand commandZakaznikId = db.CreateCommand("select idZakaznik from Bydliste where Kontakt_idKontakt = @Kontakt_idKontakt and Bydliste_idBydliste = @Bydliste_idBydliste and jmeno = @jmeno and prijmeni = @prijmeni");
            commandZakaznikId.Parameters.AddWithValue("@Kontakt_idKontakt", zakaznik.Kontakt_idKontakt);
            commandZakaznikId.Parameters.AddWithValue("@Bydliste_idBydliste", zakaznik.Bydliste_idBydliste);
            commandZakaznikId.Parameters.AddWithValue("@jmeno", zakaznik.jmeno);
            commandZakaznikId.Parameters.AddWithValue("@prijmeni", zakaznik.prijmeni);
            reader = db.Select(commandBydlisteId);
            reader.Read();
            newIdZakaznik = reader.GetInt32(0);
            reader.Close();
            zakaznik.Kontakt_idKontakt = newIdKontakt;
            zakaznik.Bydliste_idBydliste = newIdBydliste;
            zakaznik.idZakaznik = newIdZakaznik;

            if (pDb == null)
            {
                db.Close();
            }
            return returnCommand1 & returnCommand2 & returnCommand3;
        }

        public static int Delete(int idZakaznik, Database pDb = null)
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

            command.Parameters.AddWithValue("@idZakaznik", idZakaznik);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static Collection<Zakaznik> CtiZakaznika(SqlDataReader reader, bool select)
        {
            Collection<Zakaznik> zakaznici = new Collection<Zakaznik>();
            // z.idZakaznik, z.jmeno, z.prijmeni, b.mesto, b.cisloPopisne, b.psc
            // z.idZakaznik, z.jmeno, z.prijmeni, b.mesto, b.cisloPopisne, b.psc, k.tel, k.tel2, k.email
            while (reader.Read())
            {
                int i = -1;
                Zakaznik zakaznik = new Zakaznik();

                if (select) // jednoduchy vypis
                {
                    zakaznik.idZakaznik = reader.GetInt32(++i);
                    zakaznik.jmeno = reader.GetString(++i);
                    zakaznik.prijmeni = reader.GetString(++i);
                    zakaznik.mesto = reader.GetString(++i);
                    zakaznik.ulice = reader.GetString(++i);
                    zakaznik.cisloPopisne = reader.GetInt32(++i);
                    zakaznik.psc = reader.GetInt32(++i);
                }
                else // vypis s detailem
                {
                    zakaznik.idZakaznik = reader.GetInt32(++i);
                    zakaznik.jmeno = reader.GetString(++i);
                    zakaznik.prijmeni = reader.GetString(++i);
                    zakaznik.mesto = reader.GetString(++i);
                    zakaznik.cisloPopisne = reader.GetInt32(++i);
                    zakaznik.psc = reader.GetInt32(++i);
                    zakaznik.tel = reader.GetInt32(++i);
                    zakaznik.tel2 = reader.GetInt32(++i);
                    zakaznik.email = reader.GetString(++i);
                }
                // if (!reader.IsDBNull(++i))

                zakaznici.Add(zakaznik);
            }
            return zakaznici;
        }
    }
}
