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
    public partial class Menu : Form
    {
        private Entreprise transconnect;
        public Menu(Entreprise TransConnect)
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
        private void Page_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
