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
    public partial class VytvoreniZakazky : UserControl
    {
        FormMain mainForm;
        Collection<Zakaznik> zakaznici;
        Collection<Zarizeni> zarizeni;
        
        public VytvoreniZakazky()
        {
            InitializeComponent();
            this.Hide();
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

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
            zakaznici = EvidenceZakaznika.Select(db);
            zarizeni = EvidenceZarizeni.Select(db);
            db.Close();

            nazevTextBox.Text = "";
            smlouvaTextBox.Text = "";

            comboBox1.DisplayMember = "Prijmeni";
            comboBox1.DataSource = zakaznici;
            comboBox2.DisplayMember = "nazev";
            comboBox2.DataSource = zarizeni;

            dateTimePicker1.Value = DateTime.Now;

            this.Show();
        }

        private void zavritButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void vytvoritButton_Click(object sender, EventArgs e)
        {
            if (nazevTextBox.Text == "" || smlouvaTextBox.Text == "")
            {
                MessageBox.Show("Nejsou vyplněna všechna textová pole.", "Upozornění");
            }
            else
            {
                bool dokonceno = false, zaplaceno = false;

                if (dokoncenoRadioButton1.Checked){
                    dokonceno = true;
                } else if (dokoncenoRadioButton2.Checked){
                    dokonceno = false;
                } else dokonceno = false;

                if (zaplacenoRadioButton1.Checked){
                    zaplaceno = true;
                } else if (zaplacenoRadioButton2.Checked){
                    zaplaceno = false;
                } else zaplaceno = false;

                Zakazka zakazkaProVlozeni = new Zakazka();
                zakazkaProVlozeni.nazev = nazevTextBox.Text;
                zakazkaProVlozeni.smlouva = smlouvaTextBox.Text;
                zakazkaProVlozeni.splatnost = dateTimePicker1.Value;
                zakazkaProVlozeni.dokonceno = dokonceno;
                zakazkaProVlozeni.zaplaceno = zaplaceno;
                zakazkaProVlozeni.poznamka = richTextBox1.Text;

                Zakaznik model = comboBox1.SelectedItem as Zakaznik;
                zakazkaProVlozeni.Zakaznik_idZakaznik = model.idZakaznik;

                Zarizeni model2 = comboBox2.SelectedItem as Zarizeni;
                zakazkaProVlozeni.Zarizeni_idZarizeni = model2.idZarizeni;

                Database db = new Database();
                db.Connect();

                int ii = EvidenceZakazek.Insert(zakazkaProVlozeni, db);

                db.Close();

                if (ii != 1)
                {
                    MessageBox.Show("Při zápisu dat do databáze se vyskytla chyba", "Chyba");
                }
                else
                {
                    MessageBox.Show("Zakázka byla úspěšně vytvořena.", "Oznámení");
                    this.Hide();
                }
               
            }
        }
    }
}
