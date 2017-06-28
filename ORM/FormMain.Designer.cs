namespace ORM
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pridatKotelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.vytvoritZakazkuButton = new System.Windows.Forms.Button();
            this.pridatKotel1 = new ORM.PridatKotel();
            this.vytvoreniZakazky1 = new ORM.VytvoreniZakazky();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pridatKotelButton
            // 
            this.pridatKotelButton.Location = new System.Drawing.Point(116, 12);
            this.pridatKotelButton.Name = "pridatKotelButton";
            this.pridatKotelButton.Size = new System.Drawing.Size(98, 28);
            this.pridatKotelButton.TabIndex = 0;
            this.pridatKotelButton.Text = "Přidat kotel";
            this.pridatKotelButton.UseVisualStyleBackColor = true;
            this.pridatKotelButton.Click += new System.EventHandler(this.pridatKotelButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.vytvoreniZakazky1);
            this.panel1.Controls.Add(this.pridatKotel1);
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 526);
            this.panel1.TabIndex = 1;
            // 
            // vytvoritZakazkuButton
            // 
            this.vytvoritZakazkuButton.Location = new System.Drawing.Point(12, 12);
            this.vytvoritZakazkuButton.Name = "vytvoritZakazkuButton";
            this.vytvoritZakazkuButton.Size = new System.Drawing.Size(98, 28);
            this.vytvoritZakazkuButton.TabIndex = 2;
            this.vytvoritZakazkuButton.Text = "Vytvořit zakázku";
            this.vytvoritZakazkuButton.UseVisualStyleBackColor = true;
            this.vytvoritZakazkuButton.Click += new System.EventHandler(this.vytvoritZakazkuButton_Click);
            // 
            // pridatKotel1
            // 
            this.pridatKotel1.Location = new System.Drawing.Point(3, 3);
            this.pridatKotel1.Name = "pridatKotel1";
            this.pridatKotel1.Size = new System.Drawing.Size(624, 517);
            this.pridatKotel1.TabIndex = 0;
            this.pridatKotel1.Visible = false;
            // 
            // vytvoreniZakazky1
            // 
            this.vytvoreniZakazky1.Location = new System.Drawing.Point(3, 0);
            this.vytvoreniZakazky1.Name = "vytvoreniZakazky1";
            this.vytvoreniZakazky1.Size = new System.Drawing.Size(624, 517);
            this.vytvoreniZakazky1.TabIndex = 1;
            this.vytvoreniZakazky1.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 573);
            this.Controls.Add(this.vytvoritZakazkuButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pridatKotelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Správce zakázek montážní firmy ekologických kotlů";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pridatKotelButton;
        private System.Windows.Forms.Panel panel1;
        private PridatKotel pridatKotel1;
        private System.Windows.Forms.Button vytvoritZakazkuButton;
        private VytvoreniZakazky vytvoreniZakazky1;
    }
}