using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_TransConnect_TANG
{
    public partial class MenuPrincipal : Form
    {
        private Form entreprise;
        private Form client;
        private Form organigramme;
        private Form testunitaire;

        Entreprise transconnect;

        public MenuPrincipal(Entreprise TransConnect)
        {
            this.transconnect = TransConnect;
            InitializeComponent();
            splitContainer1.IsSplitterFixed = false;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
        }
        public Entreprise TransConnect
        { 
            set { this.transconnect = value; } 
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            entreprise = new MenuEntreprise(this.transconnect);
            InitializeChildForm(entreprise);

            client = new MenuClient(this.transconnect);
            InitializeChildForm(client);

            organigramme = new Organigramme(this.transconnect);
            InitializeChildForm(organigramme);

            testunitaire = new TestUnitaire(this.transconnect);
            InitializeChildForm(testunitaire);
        }

        private void InitializeChildForm(Form childForm)
        {
            childForm.Dock = DockStyle.Fill;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.TopLevel = false;
        }

        private void entreprise_Click(object sender, EventArgs e)
        {
            ShowForm(entreprise);
        }

        private void organigramme_Click(object sender, EventArgs e)
        {
            ShowForm(organigramme);
        }

        private void testunitaire_Click(object sender, EventArgs e)
        {
            ShowForm(testunitaire);
        }
        private void client_Click(object sender, EventArgs e)
        {
            ShowForm(client);
        }

        private void ShowForm(Form form)
        {
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(form);
            form.Show();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
