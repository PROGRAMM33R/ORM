using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ORM.DB;
using System.Collections.ObjectModel;
using ORM.DB.dao_sql;

namespace ORM
{
    public partial class PridatKotel : UserControl
    {
        FormMain mainForm;
        Collection<StavZarizeni> stavy;
        Collection<TypPaliva> paliva;
        public PridatKotel()
        {
            InitializeComponent();
            this.Hide();
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public void Zobrazit(FormMain f)
        {
            mainForm = f;
            Database db = new Database();
            db.Connect();
            stavy = EvidenceZarizeni.SelectStavy(db);
            paliva = EvidenceZarizeni.SelectPaliva(db);
            db.Close();
           
            nazevTextBox.Text = "";
            vyrobceTextBox.Text = "";
            zarukaTextBox.Text = "";
            vyrobniCisloTextBox.Text = "";
            modelTextBox.Text = "";
            vykonTextBox.Text = "";
            emisniTridaTextBox.Text = "";

            comboBox1.DisplayMember = "nazev";
            comboBox1.DataSource = stavy;

            comboBox2.DisplayMember = "nazev";
            comboBox2.DataSource = paliva;

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            this.Show();
        }

        private void zavritButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void vytvoritButton_Click(object sender, EventArgs e)
        {
            if (nazevTextBox.Text == "" || vyrobceTextBox.Text == "" || zarukaTextBox.Text == "" || vyrobniCisloTextBox.Text == "" || modelTextBox.Text == "" || vykonTextBox.Text == "" || emisniTridaTextBox.Text == "")
            {
                MessageBox.Show("Nejsou vyplněna všechna textová pole.", "Upozornění");
            }
            else
            {

                SpecifikaceZarizeni specifikaceProVlozeni = new SpecifikaceZarizeni();
                specifikaceProVlozeni.emisniTrida = emisniTridaTextBox.Text;
                specifikaceProVlozeni.posledniKontrola = dateTimePicker2.Value;
                specifikaceProVlozeni.vykon = Convert.ToInt32(vykonTextBox.Text);
                specifikaceProVlozeni.model = modelTextBox.Text;
                specifikaceProVlozeni.vyrobniCislo = Convert.ToInt32(vyrobniCisloTextBox.Text);
                var idPalivo = Convert.ToInt32(comboBox2.SelectedIndex);
                idPalivo++;
                specifikaceProVlozeni.TypPaliva_idTypPaliva = idPalivo;

                Database db = new Database();
                db.Connect();

                int i = EvidenceZarizeni.InsertSpecifikace(specifikaceProVlozeni, db);
                int idSpecifikace = EvidenceZarizeni.posledniIdSpecifikace(db);

                Zarizeni zarizeniProVlozeni = new Zarizeni();
                zarizeniProVlozeni.idZarizeni = 1;
                zarizeniProVlozeni.SpecifikaceZarizeni_idSpecifikaceZarizeni = idSpecifikace;
                zarizeniProVlozeni.nazev = nazevTextBox.Text;
                zarizeniProVlozeni.vyrobce = vyrobceTextBox.Text;
                zarizeniProVlozeni.zaruka = Convert.ToInt32(zarukaTextBox.Text);
                zarizeniProVlozeni.datumSpusteni = dateTimePicker1.Value;
                var id = Convert.ToInt32(comboBox1.SelectedIndex);
                id++;
                zarizeniProVlozeni.StavZarizeni_idStavZarizeni = id;

                int ii = EvidenceZarizeni.Insert(zarizeniProVlozeni, db);

                db.Close();

                if (i != 1 || ii != 1)
                {
                    MessageBox.Show("Při zápisu dat do databáze se vyskytla chyba", "Chyba");
                }
                else
                {
                    MessageBox.Show("Přidání kotle bylo úspěšné.", "Oznámení");
                    this.Hide();
                }

            }
        }

    }
}
