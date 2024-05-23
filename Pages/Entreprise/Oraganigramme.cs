using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projet_TransConnect_TANG
{
    public partial class Organigramme : Form
    {
        private Entreprise transconnect;
        private TextWriter consoleWriter;
        public Organigramme(Entreprise TransConnect)
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

        private void button1_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Invalidate();
            consoleWriter = Console.Out;
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            this.transconnect.AfficherOrganigramme();
            label1.Text = stringWriter.ToString();
            Console.SetOut(consoleWriter);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
