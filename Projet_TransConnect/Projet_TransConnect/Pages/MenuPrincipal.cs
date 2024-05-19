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
        private Form menu;
        private Form organigramme;
        private Form testunitaire;

        public MenuPrincipal()
        {
            InitializeComponent();
            splitContainer1.IsSplitterFixed = false;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            menu = new Menu();
            InitializeChildForm(menu);

            organigramme = new Organigramme();
            InitializeChildForm(organigramme);

            testunitaire = new TestUnitaire();
            InitializeChildForm(testunitaire);
        }

        private void InitializeChildForm(Form childForm)
        {
            childForm.Dock = DockStyle.Fill;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.TopLevel = false;
        }

        private void menu_Click(object sender, EventArgs e)
        {
            ShowForm(menu);
        }

        private void organigramme_Click(object sender, EventArgs e)
        {
            ShowForm(organigramme);
        }

        private void testunitaire_Click(object sender, EventArgs e)
        {
            ShowForm(testunitaire);
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
